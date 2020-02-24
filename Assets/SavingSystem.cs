using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SavingSystem : MonoBehaviour
{
    public bool Hiragana;
    public bool Katakana;

    public bool KatakanaEXT;
    public bool HiraganaEXT;
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLangBool(bool k, bool h, bool ke, bool he)
    {
        Hiragana = h;
        Katakana = k;
        KatakanaEXT = ke;
        HiraganaEXT = he;
    }

    public bool getHiraganaBool() {return Hiragana;}
    public bool getKatakanaBool() {return Katakana;}
    public bool getKatakanaEXTBool() {return KatakanaEXT;}
    public bool getHiraganaEXTBool() {return HiraganaEXT;}

}
