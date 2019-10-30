using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 0;

    [SerializeField]
    private GameObject particles;

    private int ButtonPressed;
    private int maxClicks = 0;
    private TextMeshProUGUI buttonText;
    private Image[] buttonImage;
    Animator anim;

    private float timer = 0;
    private bool dying = false;
    private float u = 200;

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
            transform.localPosition += new Vector3(0, u * Time.deltaTime - 0.5f * 1000 * Time.deltaTime * Time.deltaTime , 0);
            u -= 1000 * Time.deltaTime;
        }
        else if(timer > lifetime)
        {
            BreakHeart();
        }
    }

    public void ButtonClicked()
    {
        if (dying)
            return;

        timer = 0;
        ButtonPressed--;

        if (ButtonPressed > 0)
        {
            buttonText.text = ButtonPressed.ToString();
            anim.SetTrigger("Pressed");
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
            Destroy(gameObject, 0.2f);
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
        {
            GameManager.instance.IncrementScore();
            Destroy(Instantiate(particles, transform.position + particles.transform.position, particles.transform.rotation, GameManager.instance.canvas), 1.0f);
        }
        else
            GameManager.instance.DecrementScore();
    }
}
