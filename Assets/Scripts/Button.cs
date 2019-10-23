using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour
{
    int ButtonPressed=1;
    int maxClicks = 0;
    TextMeshProUGUI buttonText;
    Image buttonImage;

    public void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = GetComponent<Image>();

        maxClicks = Random.Range(1, 5);
        ButtonPressed = maxClicks;

        buttonText.text = ButtonPressed.ToString();
        buttonImage.color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);
    }
    public void ButtonClicked()
    {
        ButtonPressed--;

        if (ButtonPressed > 0)
        {
            buttonText.text = ButtonPressed.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

}
