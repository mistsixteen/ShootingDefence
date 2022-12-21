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


    // Start is called before the first frame update
    private void Awake()
    {
        inventorySlot = new Image[10];
        inventoryItem = new Image[10];

        for (int i = 0; i < 10; i++)
        {
            inventorySlot[i] = inventory.Find("Slot_" + (i + 1).ToString()).GetComponent<Image>();
            inventoryItem[i] = inventory.Find("Item_" + (i + 1).ToString()).GetComponent<Image>();
        }
    }

    private void OnEnable()
    {
        if(AppInstance.GetInstance() != null) { 
            AppInstance.GetInstance().ModelManager.ModelUser.onChanged += OnUpdateUserModel;
            AppInstance.GetInstance().ModelManager.ModelInventory.onChanged += OnUpdateInventoryModel;
            OnUpdateUserModel();
            OnUpdateInventoryModel();
        }
        StartCoroutine(UpdateCursorRoutine());
    }

    private void OnDisable()
    {
        AppInstance.GetInstance().ModelManager.ModelUser.onChanged -= OnUpdateUserModel;
        AppInstance.GetInstance().ModelManager.ModelInventory.onChanged -= OnUpdateInventoryModel;
        StopAllCoroutines();
    }

    public void OnUpdateUserModel()
    {
        //HpBar
        healthBar.fillAmount = AppInstance.GetInstance().ModelManager.ModelUser.GetHpPercentage();
    }

    public void OnUpdateInventoryModel()
    {
        var ModelInventory = AppInstance.GetInstance().ModelManager.ModelInventory;
        for (int i = 0; i < 10; i++)
        {
            inventorySlot[i].sprite = Black;
        }
        if(ModelInventory.currentIdx != -1)
            inventorySlot[ModelInventory.currentIdx].sprite = Red;
        //gunTypeText, bulletText;
        if (ModelInventory.IsCurrentItemWeapon())
        {
            var weapon = ModelInventory.GetCurrentItemWeapon();
            gunTypeText.text = "Weapon : " + weapon.ItemRow.Name;
            if (false)
                bulletText.text = "Reload!!!";
            else
                bulletText.text = weapon.CurrentMag.ToString();
        }
        else
        {
            gunTypeText.text = "Weapon : ";
            bulletText.text = " ";
        }
    }

    //TODO : 다른 곳으로 이동 or 별개 Prefab에서 수행할 것 

    IEnumerator UpdateCursorRoutine()
    {
        Vector2 mouseSpot;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (false)
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
