using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text waveText, gunTypeText, bulletText;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateUIRoutine());
    }

    IEnumerator UpdateUIRoutine()
    {
        while(true)
        {
            //WaveText
            waveText.text = "Wave 001";

            //HpBar

            //GunUI
            gunTypeText.text = GunInventory.GetInstance().GetCurrentItem().gunName;
            bulletText.text = "" + GunInventory.GetInstance().GetCurrentItem().bulletLeft + " / " + GunInventory.GetInstance().GetCurrentItem().totalBulletLeft;

            yield return new WaitForSeconds(0.1f);
        }
    }

}
