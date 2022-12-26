//C#
using System.Collections.Generic;

//Unity
using UnityEngine;

public class UISystem : MonoBehaviour
{
    private static UISystem instance;

    [SerializeField]
    private GUIGameMode GUIGameMode;

    [SerializeField]
    private Transform transformUIPopup;

    List<GameObject> listUIPopup;

    public static UISystem GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        listUIPopup = new List<GameObject>();
        instance = this;
    }

    public GameObject UICreatePopup(string Addr)
    {
        var uiPrefab = Resources.Load(Addr) as GameObject;
        if (uiPrefab != null)
        {
            var newUIObject = GameObject.Instantiate(uiPrefab, transformUIPopup);
            if(newUIObject != null) { 
                listUIPopup.Add(newUIObject);
                return newUIObject;
            }
        }
        Debug.LogError("UICreatePopup Failed : " + Addr);
        return null;
    }

    public void UIRemovePopup(GameObject obj)
    {
        listUIPopup.Remove(obj);
        Destroy(obj);
    }

    public void OnChangeScene(EnumGameScene targetScene)
    {
        if(targetScene == EnumGameScene.SceneGame)
        {
            GUIGameMode.gameObject.SetActive(true);
        }
        else
        {
            GUIGameMode.gameObject.SetActive(false);
        }
    }
}
