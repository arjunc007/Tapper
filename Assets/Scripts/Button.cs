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
    private Image[] buttonImage;
    Animator anim;

    private float timer = 0;
    private bool dying = false;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = GetComponentsInChildren<Image>();
        anim = GetComponent<Animator>();

        maxClicks = Random.Range(1, 5);
        ButtonPressed = maxClicks;

        buttonText.text = ButtonPressed.ToString();

        Color randomColor = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);
        foreach (var img in buttonImage)
            img.color = randomColor;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(dying)
        {
            transform.localPosition += new Vector3(0, -50 * timer + 0.5f * 10 * timer * timer , 0);
        }
        else if(timer > lifetime)
        {
            BreakHeart();
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
            BreakHeart();
        }
        else
        {
            Color c = buttonImage[0].color;
            c.a = 0;
            foreach (var img in buttonImage)
                img.color = c;

            buttonText.gameObject.SetActive(false);
            Destroy(gameObject, 0.3f);
        }
    }

    private void BreakHeart()
    {
        dying = true;
        timer = 0;
        anim.SetTrigger("Break");
        Destroy(gameObject, 1.5f);
    }

    private void OnDestroy()
    {
        if(ButtonPressed == 0 && timer < lifetime)
            GameManager.instance.IncrementScore();
        else
            GameManager.instance.DecrementScore();
    }
}
