using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    public GameObject vocab;
    public GameObject single;

    GameObject managers;

    public bool kata;
    public bool Hiragana;
    public bool kataExt;
    public bool HiraganaEXT;
    // Start is called before the first frame update
    void Start()
    {
        managers = GameObject.FindGameObjectWithTag("GameController");
        kata = managers.GetComponent<SavingSystem>().getKatakanaBool();
        Hiragana = managers.GetComponent<SavingSystem>().getHiraganaBool();
        kataExt= managers.GetComponent<SavingSystem>().getKatakanaEXTBool();
        HiraganaEXT = managers.GetComponent<SavingSystem>().getHiraganaEXTBool();

        

        if(kata == true || Hiragana == true || kataExt == true || HiraganaEXT == true)
        {
            single.SetActive(true);
        }
        else{
            vocab.SetActive(true);
        }
        
        
    }

    public void setVocab()
    {
        vocab.SetActive(true);
        single.SetActive(false);
    }
    public void setSingle()
    {
        vocab.SetActive(false);
        single.SetActive(true);
    }
}
