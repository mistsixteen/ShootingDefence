using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIGameMode : MonoBehaviour
{
    [SerializeField]
    private UIPlayerHealthBar UIHealthBar;

    [SerializeField]
    private UICurrentWeapon UICurrentWeapon;

    [SerializeField]
    private Texture2D normalTexture, reloadTexture;

    [SerializeField]
    private Sprite Red, Black;

    [SerializeField]
    private Transform inventory;

    private Image[] inventorySlot;
    private Image[] inventoryItem;

    private bool isReloading = false;

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
            EventSystem.GetInstance()?.RegistEventListener(EventType.onModelInvenChanged, OnUpdateInventoryModel);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onModelUserChanged, OnUpdateUserModel);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onReloadStarted, OnReloadStart);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onReloadFinished, OnReloadEnd);
            OnUpdateUserModel();
            OnUpdateInventoryModel();
            isReloading = false;
        }
        StartCoroutine(UpdateCursorRoutine());
    }

    private void OnDisable()
    {
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onModelInvenChanged, OnUpdateInventoryModel);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onModelUserChanged, OnUpdateUserModel);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onReloadStarted, OnReloadStart);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onReloadFinished, OnReloadEnd);
        StopAllCoroutines();
    }

    //Event Functions

    public void OnUpdateUserModel()
    {
        UIHealthBar.UpdateHealthBar();
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

        UICurrentWeapon.UpdateStatus();
    }

    public void OnReloadStart()
    {
        isReloading = true;
        UICurrentWeapon.SetReloadMode();
    }

    public void OnReloadEnd()
    {
        isReloading = false;
        UICurrentWeapon.UpdateStatus();
    }

    //TODO : 다른 곳으로 이동 or 별개 Prefab에서 수행할 것 

    IEnumerator UpdateCursorRoutine()
    {
        Vector2 mouseSpot;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (isReloading)
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
