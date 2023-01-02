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
    private UIQuickBar UIQuickBar;

    [SerializeField]
    private Texture2D normalTexture, reloadTexture;

    [SerializeField]
    private Sprite Red, Black;

    [SerializeField]
    private Button ButtonConfig;

    [SerializeField]
    private Text InfoText;

    private bool isReloading = false;

    private void OnEnable()
    {
        if(AppInstance.GetInstance() != null) { 
            EventSystem.GetInstance()?.RegistEventListener(EventType.onModelInvenChanged, OnUpdateInventoryModel);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onModelUserChanged, OnUpdateUserModel);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onReloadStarted, OnReloadStart);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onReloadFinished, OnReloadEnd);
            EventSystem.GetInstance()?.RegistEventListener(EventType.onGamePlayStateChanged, OnGameStateChanged);
            OnUpdateUserModel();
            OnUpdateInventoryModel();
            isReloading = false;
        }
        StartCoroutine(UpdateCursorRoutine());

        ButtonConfig.onClick.AddListener(() => {
            UISystem.GetInstance()?.UICreatePopup(UIAddressPrefabPopup.UIPopupInGameConfig);
        });
    }

    private void OnDisable()
    {
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onModelInvenChanged, OnUpdateInventoryModel);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onModelUserChanged, OnUpdateUserModel);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onReloadStarted, OnReloadStart);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onReloadFinished, OnReloadEnd);
        EventSystem.GetInstance()?.UnRegistEventListener(EventType.onGamePlayStateChanged, OnGameStateChanged);
        StopAllCoroutines();

        ButtonConfig.onClick.RemoveAllListeners();
    }

    //Event Functions

    public void OnUpdateUserModel()
    {
        UIHealthBar.UpdateHealthBar();
    }

    public void OnUpdateInventoryModel()
    {
        UIQuickBar.UpdateStatus();
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

    public void OnGameStateChanged()
    {
        var cState = GameStateManager.GetInstance().GetCurrentState();
        switch(cState)
        {
            case StateGamePlay.StateGameDay:
                this.InfoText.text = "Current State : Day";
                break;
            case StateGamePlay.StateGameNight:
                this.InfoText.text = "Current State : Night";
                break;
            default:
                this.InfoText.text = " ";
                break;
        }
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
