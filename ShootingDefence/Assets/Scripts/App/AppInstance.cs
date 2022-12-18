using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInstance : MonoBehaviour
{
    private static AppInstance instance = null;

    public ModelManager ModelManager;
    public TableManager TableManager;

    public UISystem UISystem;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        instance = this;

        this.TableManager = new TableManager();
        this.ModelManager = new ModelManager();
    }

    void Start()
    {
        ChangeCurrentScene(EnumGameScene.SceneMainMenu);
        this.UISystem = UISystem.GetInstance();
    }

    public static AppInstance GetInstance(){
        return instance;
    }

    public void ChangeCurrentScene(EnumGameScene targetScene)
    {
        switch (targetScene)
        {
            case EnumGameScene.SceneGameStart:
                break;
            case EnumGameScene.SceneMainMenu:
                SceneManager.LoadScene("SceneMainMenu");
                break;
            case EnumGameScene.SceneGameReady:
                break;
            case EnumGameScene.SceneGame:
                SceneManager.LoadScene("SceneGameMode");
                break;
            case EnumGameScene.SceneGameOver:
                break;
        }
        UISystem.OnChangeScene(targetScene);
    }
}
