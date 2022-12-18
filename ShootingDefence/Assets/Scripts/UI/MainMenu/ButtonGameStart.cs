using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGameStart : MonoBehaviour
{
    public Button GameStartButton;
    // Start is called before the first frame update
    void Start()
    {
        GameStartButton.onClick.AddListener(() => {
            AppInstance.GetInstance()?.ChangeCurrentScene(EnumGameScene.SceneGame);
        });
    }

}
