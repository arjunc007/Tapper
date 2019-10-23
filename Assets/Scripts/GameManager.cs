using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonState
{
    public GameObject button;
    public int Clicks;
}
public class GameManager : MonoBehaviour
{ 
    public GameObject ButtonPrefab;
    public Transform canvas;
    public List<ButtonState> AllButtons = new List<ButtonState>();

    private float halfScreenWidth, halfScreenHeight;

    // Start is called before the first frame update
    void Start()
    {
        halfScreenHeight = Screen.height / 2;
        halfScreenWidth = Screen.width / 2;
        StartCoroutine("GenerateShapes");
    }

    IEnumerator GenerateShapes()
    {
        while (true)
        {
            var Shape = Instantiate(ButtonPrefab, canvas) as GameObject;
            float x = Random.Range(-0.9f * halfScreenWidth, 0.9f * halfScreenWidth);
            float y = Random.Range(-0.9f * halfScreenHeight, 0.9f * halfScreenHeight);
            Vector2 pos = new Vector2(x, y);
            int Clicks = Random.Range(1, 5);
            Shape.GetComponent<RectTransform>().localPosition = pos;
            Shape.GetComponentInChildren<TextMeshProUGUI>().text = Clicks.ToString();
            ButtonState obj = new ButtonState();
            obj.button = Shape;
            obj.Clicks = Clicks;
            AllButtons.Add(obj);
            yield return new WaitForSeconds(2.0f);
        }
    }

}
