using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInstance : MonoBehaviour
{
    private static AppInstance instance = null;

    private EnumGameScene CurrentScene;

    public ModelManager ModelManager;
    public TableManager TableManager;

    public UISystem UISystem;
    public EventSystem EventSystem;
    public SoundSystem SoundSystem;

    private void Awake()
    {
        instance = this;

        CurrentScene = EnumGameScene.SceneGameStart;

        this.TableManager = new TableManager();
        this.ModelManager = new ModelManager();
    }

    void Start()
    {
        ChangeCurrentScene(EnumGameScene.SceneMainMenu);
        this.UISystem = UISystem.GetInstance();
        this.EventSystem = EventSystem.GetInstance();
    }

    public static AppInstance GetInstance(){
        return instance;
    }

    public void ChangeCurrentScene(EnumGameScene targetScene)
    {
        switch (CurrentScene)
        {
            case EnumGameScene.SceneGameStart:
                break;
            case EnumGameScene.SceneMainMenu:
                SceneManager.UnloadSceneAsync("SceneMainMenu");
                break;
            case EnumGameScene.SceneGameReady:
                break;
            case EnumGameScene.SceneGame:
                SceneManager.UnloadSceneAsync("SceneGameMode");
                break;
            case EnumGameScene.SceneGameOver:
                SceneManager.UnloadSceneAsync("SceneGameOver");
                break;
        }
        switch (targetScene)
        {
            case EnumGameScene.SceneGameStart:
                break;
            case EnumGameScene.SceneMainMenu:
                SceneManager.LoadScene("SceneMainMenu", LoadSceneMode.Additive);
                break;
            case EnumGameScene.SceneGameReady:
                break;
            case EnumGameScene.SceneGame:
                SceneManager.LoadScene("SceneGameMode", LoadSceneMode.Additive);
                break;
            case EnumGameScene.SceneGameOver:
                SceneManager.LoadScene("SceneGameOver", LoadSceneMode.Additive);
                break;
        }
        CurrentScene = targetScene;
        UISystem.OnChangeScene(targetScene);
    }
}
