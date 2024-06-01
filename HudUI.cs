using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] Image hudImage;
    [SerializeField] Sprite[] hudSprites;
    [SerializeField] TMP_Text starCount;


    void Update()
    {
        starCount.text = $"X {ItemManager.Instance.Starcount}";

        switch (ItemManager.Instance.CarrotCount)
        {
            case 0:
                hudImage.sprite = hudSprites[0];
                break;
            case 1:
                hudImage.sprite = hudSprites[1];
                break;
            case 2:
                hudImage.sprite = hudSprites[2];
                break;
            case 3:
                hudImage.sprite = hudSprites[3];
                break;
        }
        
    }
}
