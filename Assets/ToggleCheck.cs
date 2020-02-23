using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleCheck : MonoBehaviour
{
    public GameObject kTog;
    public GameObject hTog; 
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        kTog.GetComponent<Toggle>().isOn = false;
        hTog.GetComponent<Toggle>().isOn = false;
        manager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
