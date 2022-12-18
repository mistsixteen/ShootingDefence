using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    private static UISystem instance;

    [SerializeField]
    private GameObject GUIGameMode;

    public static UISystem GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void OnChangeScene(EnumGameScene targetScene)
    {
        if(targetScene == EnumGameScene.SceneGame)
        {
            GUIGameMode.SetActive(true);
        }
        else
        {
            GUIGameMode.SetActive(false);
        }
    }
}
