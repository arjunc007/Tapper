using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private Transform canvas;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float halfScreenWidth, halfScreenHeight;
    private float buttonWidth;

    private int score = 0;
    private float buttonFrequency;
    private int level;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        halfScreenHeight = Screen.height / 2;
        halfScreenWidth = Screen.width / 2;
        buttonWidth = buttonPrefab.GetComponent<RectTransform>().rect.width / 2;
        scoreText.text = score.ToString();
        buttonFrequency = 2.0f;
        level = 1;

        StartCoroutine("GenerateShapes");
    }

    IEnumerator GenerateShapes()
    {
        while (true)
        {
            var Shape = Instantiate(buttonPrefab, canvas) as GameObject;
            float x = Random.Range(-halfScreenWidth + buttonWidth, halfScreenWidth - buttonWidth);
            float y = Random.Range(-halfScreenHeight + buttonWidth, halfScreenHeight - buttonWidth);
            Vector2 pos = new Vector2(x, y);
            int Clicks = Random.Range(1, 5);
            Shape.GetComponent<RectTransform>().localPosition = pos;
            Shape.GetComponentInChildren<TextMeshProUGUI>().text = Clicks.ToString();
            yield return new WaitForSeconds(buttonFrequency);
        }
    }

    public void IncrementScore()
    {
        scoreText.text = (++score).ToString();

        if (score % 10 == 0 && score > 0)
        {
            buttonFrequency = Mathf.Max(0.25f, buttonFrequency - 0.2f);
        }
    }

    public void DecrementScore()
    {
        score = Mathf.Max(0, --score);
        scoreText.text = (score).ToString();
    }

}
