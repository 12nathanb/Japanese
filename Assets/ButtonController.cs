using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public string current;
    public Text cardText;
    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        card = GameObject.FindGameObjectWithTag("Card");
    }

    // Update is called once per frame
    void Update()
    {
    
        cardText.text = current;
    }

    public void buttonPress()
    {
        card.GetComponent<lang>().ButtonPress(current);
    }

    public void GetNewLetter(string i)
    {
        current = i;
    }
}
