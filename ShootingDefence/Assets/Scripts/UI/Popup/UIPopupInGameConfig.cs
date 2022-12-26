using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupInGameConfig : MonoBehaviour
{
    [SerializeField]
    private Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        closeButton.onClick.AddListener(() => {
            UISystem.GetInstance().UIRemovePopup(this.gameObject);
        });
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }
}
