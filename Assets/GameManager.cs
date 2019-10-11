using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GenerateShapes");
    }

    IEnumerator GenerateShapes()
    {
        while (true)
        {
            var Shape = Instantiate(ButtonPrefab, canvas) as GameObject;
            float x = Random.Range(-400, 400);
            float y = Random.Range(-300, 300);
            Vector2 pos = new Vector2(x, y);
            int Clicks = Random.Range(1, 5);
            Shape.GetComponent<RectTransform>().localPosition = pos;
            Shape.GetComponentInChildren<Text>().text =Clicks.ToString();
            ButtonState obj = new ButtonState();
            obj.button = Shape;
            obj.Clicks = Clicks;
            AllButtons.Add(obj);
            yield return new WaitForSeconds(5.0f);
        }
    }

}
