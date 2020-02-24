using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class menuButton : MonoBehaviour
{
    public GameObject kTog;
    public GameObject hTog; 

    public GameObject keTog;
    public GameObject HeTog;
    public bool k;
    public bool h;

    public bool ke;
    public bool he;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        h = true;
        k = false;
        ke = false;
        he = false;
        
        manager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void setKTrue()
    {
        k = true;
        h = false;
        ke = false;
        he = false;
    }

    public void setHTrue()
    {
        h = true;
        k = false;
        ke = false;
        he = false;
    }

    public void setKETrue()
    {
         h = false;
        k = false;
        ke = true;
        he = false;
    }

      public void setHETrue()
    {
        h = false;
        k = false;
        ke = false;
        he = true;
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

      if(ke == true)
      {
          keTog.GetComponent<Toggle>().isOn = true;
      }
      else
      {
          keTog.GetComponent<Toggle>().isOn = false;
      }

      if(he == true)
      {
          HeTog.GetComponent<Toggle>().isOn = true;
      }
      else
      {
          HeTog.GetComponent<Toggle>().isOn = false;
      }

    }

    public void ButtonPressed()
    {
        manager.GetComponent<SavingSystem>().setLangBool(k, h, ke, he);
        SceneManager.LoadScene(1);
    }

    
}
