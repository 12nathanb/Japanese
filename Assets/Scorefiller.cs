using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scorefiller : MonoBehaviour
{
    public GameObject hirachart;
    public GameObject yoonchart;
    public GameObject vocabchart;
    public GameObject manager;
    public GameObject[] objs;
    public GameObject[] objsText;

    public int size;

    public int[] score;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");

        

        if (manager.GetComponent<SavingSystem>().getKatakanaBool() == true ||manager.GetComponent<SavingSystem>().getHiraganaBool() == true) 
        {
            hirachart.SetActive(true);
            yoonchart.SetActive(false);
            vocabchart.SetActive(false);
        }

        if(manager.GetComponent<SavingSystem>().getHiraganaEXTBool() == true || manager.GetComponent<SavingSystem>().getKatakanaEXTBool() == true)
        {
            hirachart.SetActive(false);
            yoonchart.SetActive(true);
            vocabchart.SetActive(false);
        }

        if (manager.GetComponent<SavingSystem>().getVocabBool() == true)
        {
            hirachart.SetActive(false);
            yoonchart.SetActive(false);
            vocabchart.SetActive(true);
        }


        objs = GameObject.FindGameObjectsWithTag("scorez");
        objsText = GameObject.FindGameObjectsWithTag("ScoreText");
        score = new int[objs.Length];


        for (int i = 0; i < manager.GetComponent<SavingSystem>().getArraySize(); i ++)
        {
            objsText[i].GetComponent<TextMesh>().text = manager.GetComponent<SavingSystem>().getStructType(i);
            score[i] = manager.GetComponent<SavingSystem>().getStructScore(i);

            size ++;

            if(score[i] < 0)
            {
               objs[i].GetComponent<Image>().color = Color.red;
            }
            if(score[i] == 0)
            {
                 objs[i].GetComponent<Image>().color = Color.white;
            }
            if(score[i] > 0)
            {
                objs[i].GetComponent<Image>().color = Color.yellow;
            }
            if(score[i] > 10)
            {
                objs[i].GetComponent<Image>().color = Color.green;
            }
            if(score[i] > 100)
            {
                objs[i].GetComponent<Image>().color = new Color(212f,175f, 55f);
            }

        
        }

        for(int r = size ; r < objs.Length; r++)
        {
            objs[r].SetActive(false);
        }

        
    }

    public void CloseScore()
    {
        SceneManager.LoadScene(3);
    }

}
