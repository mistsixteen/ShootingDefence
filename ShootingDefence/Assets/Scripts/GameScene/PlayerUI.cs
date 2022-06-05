using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text waveText, gunTypeText, bulletText;
    public Image healthBar;

    public PlayerCharacter player;

    public Texture2D normalTexture, reloadTexture;

    public bool isReload = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateUIRoutine());
        StartCoroutine(UpdateCursorRoutine());
    }

    IEnumerator UpdateUIRoutine()
    {
        while(true)
        {
            //WaveText
            waveText.text = "Wave 001";

            //HpBar
            healthBar.fillAmount = player.getHpPercentage();
            //GunUI
            gunTypeText.text = GunInventory.GetInstance().GetCurrentItem().gunName;
            bulletText.text = "" + GunInventory.GetInstance().GetCurrentItem().bulletLeft + " / " + GunInventory.GetInstance().GetCurrentItem().totalBulletLeft;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator UpdateCursorRoutine()
    {
        Vector2 mouseSpot;
        while(true)
        {
            yield return new WaitForEndOfFrame();
            if(isReload)
            {
                mouseSpot.x = reloadTexture.width / 2;
                mouseSpot.y = reloadTexture.width / 2;

                Cursor.SetCursor(reloadTexture, mouseSpot, CursorMode.Auto);
            }
            else
            {
                mouseSpot.x = normalTexture.width / 2;
                mouseSpot.y = normalTexture.width / 2;

                Cursor.SetCursor(normalTexture, mouseSpot, CursorMode.Auto);
            }
            

        }
            
    }

}
