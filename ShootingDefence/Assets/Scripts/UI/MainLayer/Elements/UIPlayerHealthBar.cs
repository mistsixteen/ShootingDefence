using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image ImageHealthBar;

    public void UpdateHealthBar()
    {
        ImageHealthBar.fillAmount = AppInstance.GetInstance().ModelManager.ModelUser.GetHpPercentage();
    }
}