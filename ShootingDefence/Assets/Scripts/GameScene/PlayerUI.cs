using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text waveText, gunTypeText, bulletText;
    public Image healthBar;

    public PlayerCharacter player;

    public Texture2D normalTexture, reloadTexture;

    public bool isReload = false;

    public Sprite Red, Black;

    Transform inventory;

    Image[] inventorySlot;
    Image[] inventoryItem;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateUIRoutine());
        StartCoroutine(UpdateCursorRoutine());
        inventory = gameObject.transform.Find("Inventory");

        inventorySlot = new Image[10];
        inventoryItem = new Image[10];

        

        for (int i = 0; i < 10; i++)
        {
            inventorySlot[i] = inventory.Find("Slot_" + (i+1).ToString()).GetComponent<Image>();
            inventoryItem[i] = inventory.Find("Item_" + (i + 1).ToString()).GetComponent<Image>();

            if (inventorySlot[i] == null)
                Debug.Log("ERRORRRRR");
        }

    }

    IEnumerator UpdateUIRoutine()
    {
        while(true)
        {
            //WaveText
            waveText.text = "Wave 001";

            //HpBar
            healthBar.fillAmount = player.getHpPercentage();
            //GunUI
            gunTypeText.text = GunInventory.GetInstance().GetCurrentItem().gunName;
            bulletText.text = "" + GunInventory.GetInstance().GetCurrentItem().bulletLeft + " / " + GunInventory.GetInstance().GetCurrentItem().totalBulletLeft;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator UpdateCursorRoutine()
    {
        Vector2 mouseSpot;
        while(true)
        {
            yield return new WaitForEndOfFrame();
            if(isReload)
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
    public void SelectItem(int idx)
    {
        for(int i = 0; i < 10; i++)
        {
            inventorySlot[i].sprite = Black;
        }
        inventorySlot[idx].sprite = Red;
    }


}
