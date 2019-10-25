using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 0;

    private int ButtonPressed;
    private int maxClicks = 0;
    private TextMeshProUGUI buttonText;
    private Image buttonImage;

    private float timer = 0;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = GetComponent<Image>();

        maxClicks = Random.Range(1, 5);
        ButtonPressed = maxClicks;

        buttonText.text = ButtonPressed.ToString();
        buttonImage.color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > lifetime)
        {
            GameManager.instance.DecrementScore();
            //Destroy(gameObject);
        }
    }

    public void ButtonClicked()
    {
        timer = 0;
        ButtonPressed--;

        if (ButtonPressed > 0)
        {
            buttonText.text = ButtonPressed.ToString();
        }
        else if(ButtonPressed < 0)
        {
            GameManager.instance.DecrementScore();
            Destroy(gameObject);
        }
        else
        {
            Color c = buttonImage.color;
            c.a = 0;
            buttonImage.color = c;
            buttonText.gameObject.SetActive(false);
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnDestroy()
    {
        if(ButtonPressed == 0 && timer < lifetime)
            GameManager.instance.IncrementScore();
    }
}
