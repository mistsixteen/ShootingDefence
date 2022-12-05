using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInstance : MonoBehaviour
{
    private static AppInstance instance;

    public ModelManager ModelManager;
    public TableManager TableManager;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        instance = this;

        this.ModelManager = new ModelManager();
        this.TableManager = new TableManager();
    }

    void Start()
    {
        ChangeCurrentScene(EnumGameScene.SceneMainMenu);
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
    }
}
