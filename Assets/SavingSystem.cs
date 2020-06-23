using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SavingSystem : MonoBehaviour
{
    public bool Hiragana;
    public bool Katakana;

    public bool vocab;
    public bool KatakanaEXT;
    public bool HiraganaEXT;

    public bool easy;
    public bool medium;
    public bool hard;

    public bool random;
    public bool inOrder;
public GameObject temp ;

    public int score;
    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
       {
         Destroy(this.gameObject);
       }
        
    }
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEasy(bool e)
    {
        easy = e;
        hard = !e;
    }

    public void setMedium(bool m)
    {
        medium = m;
    }


    public void setHard(bool h)
    {
        hard = h;
        easy = !h;
    }

    public void setRandom(bool r)
    {
        random = r;
        inOrder = !r;
    }

    public void setInOrder(bool i)
    {
        inOrder = i;
        random = !i;
    }

    public void setScore(int i)
    {
        score += i;
    }
   

    public void setLangBool(bool k, bool h, bool ke, bool he, bool v)
    {
        Hiragana = h;
        Katakana = k;
        KatakanaEXT = ke;
        HiraganaEXT = he;
        vocab = v;
    }

    public bool getDiffEasy() { return easy;}
    public bool getDiffMedium() { return medium;}

    public bool getDiffHard() { return hard;}

    public bool getRan() {return random;}

    public bool getIn(){return inOrder;}
    public bool getHiraganaBool() {return Hiragana;}
    public bool getKatakanaBool() {return Katakana;}
    public bool getKatakanaEXTBool() {return KatakanaEXT;}
    public bool getHiraganaEXTBool() {return HiraganaEXT;}

    public bool getVocabBool() {return vocab;}

    

}
