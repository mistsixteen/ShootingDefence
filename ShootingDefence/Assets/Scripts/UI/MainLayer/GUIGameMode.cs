using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIGameMode : MonoBehaviour
{
    [SerializeField]
    private Text waveText, gunTypeText, bulletText;
    
    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Texture2D normalTexture, reloadTexture;

    [SerializeField]
    private Sprite Red, Black;

    [SerializeField]
    private Transform inventory;

    private Image[] inventorySlot;
    private Image[] inventoryItem;

    private bool isReload;

    // Start is called before the first frame update
    void Start()
    {
        inventorySlot = new Image[10];
        inventoryItem = new Image[10];

        for (int i = 0; i < 10; i++)
        {
            inventorySlot[i] = inventory.Find("Slot_" + (i+1).ToString()).GetComponent<Image>();
            inventoryItem[i] = inventory.Find("Item_" + (i + 1).ToString()).GetComponent<Image>();
        }
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if(AppInstance.GetInstance() != null)
            AppInstance.GetInstance().ModelManager.ModelUser.onChanged += OnUpdateUserdata;
        StartCoroutine(UpdateCursorRoutine());
        OnUpdateUserdata();
    }

    private void OnDisable()
    {
        AppInstance.GetInstance().ModelManager.ModelUser.onChanged -= OnUpdateUserdata;
        StopAllCoroutines();
    }
    /*
    IEnumerator UpdateUIRoutine()
    {
        while(true)
        {
            //WaveText
            waveText.text = "Wave 001";


            //GunUI
            gunTypeText.text = GunInventory.GetInstance().GetCurrentItem().gunName;
            bulletText.text = "" + GunInventory.GetInstance().GetCurrentItem().bulletLeft + " / " + GunInventory.GetInstance().GetCurrentItem().totalBulletLeft;

            yield return new WaitForSeconds(0.1f);
        }
    }
    */

    public void SelectItem(int idx)
    {
        for(int i = 0; i < 10; i++)
        {
            inventorySlot[i].sprite = Black;
        }
        inventorySlot[idx].sprite = Red;
    }

    public void OnUpdateUserdata()
    {
        //HpBar
        healthBar.fillAmount = AppInstance.GetInstance().ModelManager.ModelUser.GetHpPercentage();
    }

    IEnumerator UpdateCursorRoutine()
    {
        Vector2 mouseSpot;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (isReload)
            {
                mouseSpot.x = reloadTexture.width / 2;
                mouseSpot.y = reloadTexture.width / 2;

                Cursor.SetCursor(reloadTexture, mouseSpot, CursorMode.Auto);
            }
            else
            {
                mouseSpot.x = normalTexture.width / 2;
                mouseSpot.y = normalTexture.width / 2;

                Cursor.SetCursor(normalTexture, mouseSpot, CursorMode.Auto);
            }
        }
    }
}
