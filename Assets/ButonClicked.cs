using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButonClicked : MonoBehaviour
{
    public GameObject Button;
     int  ButtonPressed=1;
    int maxClicks = 0;

    public void Start()
    {
        string clicks = Button.GetComponentInChildren<Text>().text;
        maxClicks = Convert.ToInt32(clicks);
    }
    public void ButtonClicked()
    {
        if (ButtonPressed < maxClicks)
        {
            ButtonPressed++;
        }
        else
        {
            Button.SetActive(false);
        }

    }

}
