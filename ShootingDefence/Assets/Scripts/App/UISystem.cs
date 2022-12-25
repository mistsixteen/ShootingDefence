using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    private static UISystem instance;

    [SerializeField]
    private GUIGameMode GUIGameMode;

    public static UISystem GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
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
