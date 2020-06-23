using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DifficultySelect : MonoBehaviour
{

    public GameObject manager;
    
    // Start is called before the first frame update
    void Start()
    {
         manager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEasy()
    {
        manager.GetComponent<SavingSystem>().setEasy(true);
        SceneManager.LoadScene(2);
    }

    public void Medium()
    {
        manager.GetComponent<SavingSystem>().setMedium(true);
        SceneManager.LoadScene(2);
    }

    public void Hard()
    {
        manager.GetComponent<SavingSystem>().setHard(true);
        SceneManager.LoadScene(2);
    }

    public void random()
    {
        manager.GetComponent<SavingSystem>().setRandom(true);
        SceneManager.LoadScene(3);
    }

    public void inOrder()
    {
        manager.GetComponent<SavingSystem>().setInOrder(true);
        SceneManager.LoadScene(3);
    }
}
