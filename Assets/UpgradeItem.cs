using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItem : MonoBehaviour
{
    public Sprite itemicon;
    public string textInfo;
    public int buyPrice = 10;
    public Image itemIconImage;
    public TextMeshProUGUI textInfoText;
    public TextMeshProUGUI buyPriceText;
    public Button button;

    public void Start()
    {
        if(itemicon != null && itemIconImage != null)
            itemIconImage.sprite = itemicon;
        if(textInfoText != null)
            textInfoText.text = textInfo;
        if(buyPriceText != null)
            buyPriceText.text = "$" + buyPrice.ToString();
    }
   
    public void EnableButton()
    {
        button.interactable = true;
        ColorBlock colors = button.colors;
        Color newNormalColor = colors.normalColor;
        newNormalColor.a = 1f;
        colors.normalColor = newNormalColor;
        button.colors = colors;
    }

    public void DisableButton()
    {
        button.interactable = false;
        ColorBlock colors = button.colors;
        Color newNormalColor = colors.normalColor;
        newNormalColor.a = 0.2f;
        colors.normalColor = newNormalColor;
        button.colors = colors;
    }
}
