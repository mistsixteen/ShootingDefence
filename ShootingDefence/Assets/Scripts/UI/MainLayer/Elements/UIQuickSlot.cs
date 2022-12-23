using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuickSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite spriteNormal, spriteSelect;

    [SerializeField]
    private Image slotOutLine;

    [SerializeField]
    private Text textItemIcon;

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
            slotOutLine.sprite = spriteSelect;
        else
            slotOutLine.sprite = spriteNormal;
    }
    
    public void SetSlotText(string text)
    {
        if (text == null)
            textItemIcon.text = "";
        else
            textItemIcon.text = text;
    }
}
