using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExitGameResult : MonoBehaviour
{
    public Button MainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton.onClick.AddListener(() => {
            AppInstance.GetInstance()?.ChangeCurrentScene(EnumGameScene.SceneMainMenu);
        });
    }

}
