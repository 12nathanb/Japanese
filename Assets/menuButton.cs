using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class menuButton : MonoBehaviour
{
    public GameObject kTog;
    public GameObject hTog; 

    public bool k;
    public bool h;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        h = true;
        k = false;
        
        manager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void setKTrue()
    {
        k = true;
        h = false;
    }

    public void setHTrue()
    {
        h = true;
        k = false;
    }


    // Update is called once per frame
    void Update()
    {
      if(k == true)
      {
          kTog.GetComponent<Toggle>().isOn = true;
      }
      else
      {
          kTog.GetComponent<Toggle>().isOn = false;
      }

      if(h == true)
      {
          hTog.GetComponent<Toggle>().isOn = true;
      }
      else
      {
          hTog.GetComponent<Toggle>().isOn = false;
      }
    }

    public void ButtonPressed()
    {
        manager.GetComponent<SavingSystem>().setLangBool(k, h);
        SceneManager.LoadScene(1);
    }

    
}
