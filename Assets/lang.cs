using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class lang : MonoBehaviour
{
    public AudioSource _audio;

     string[] Contents;
     public string[] ContentsAnswers;
    public int[] correctness;
    public GameObject texts;
   public int choice;
   int previousChoice;
    public AudioClip[] audioC;
    public AudioSource auds;

    public bool kata;
    public bool Hiragana;
    public bool kataExt;
    public bool HiraganaEXT;

    public bool vocab;
    bool easy;
    bool medium;
    bool hard;
    public GameObject[] gameButtons;

    public GameObject manager;
    GameObject BManger;

    bool isMuted = false ;

    GameObject MCam;

    public Text Score;
    int intScore = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        intScore = 0;
        MCam = GameObject.FindGameObjectWithTag("MainCamera");
 
        manager = GameObject.FindGameObjectWithTag("GameController");
        kata = manager.GetComponent<SavingSystem>().getKatakanaBool();
        Hiragana = manager.GetComponent<SavingSystem>().getHiraganaBool();
        kataExt= manager.GetComponent<SavingSystem>().getKatakanaEXTBool();
        HiraganaEXT = manager.GetComponent<SavingSystem>().getHiraganaEXTBool();
        easy  = manager.GetComponent<SavingSystem>().getDiffEasy();
        medium  = manager.GetComponent<SavingSystem>().getDiffMedium();
        hard  = manager.GetComponent<SavingSystem>().getDiffHard();
        vocab = manager.GetComponent<SavingSystem>().getVocabBool();
        BManger = GameObject.FindGameObjectWithTag("ButtonM");

        if(kata == true || Hiragana == true)
        {
            BManger.GetComponent<buttonManager>().setSingle();
            texts.GetComponent<TextMesh>().characterSize = 1;
            gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
        }
        else if(vocab == true)
        {
            BManger.GetComponent<buttonManager>().setVocab();
            texts.GetComponent<TextMesh>().characterSize = 0.3f;
            gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
            for(int i = 0; i < gameButtons.Length; i++)
            {
                gameButtons[i].GetComponent<ButtonController>().setSize(70);
            }
        }
        else
        {
            BManger.GetComponent<buttonManager>().setSingle();
            gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
            texts.GetComponent<TextMesh>().characterSize = 0.7f;
        }

        

        GenerateDB();

        GenerateNewLetter();
        
    }

    void playAudioEquiptment(int i)
    {
        if(kata == true || Hiragana == true)
        {
            auds.clip = Resources.Load<AudioClip>("audio/" + i);
        }
        else if(vocab == true)
        {
            Debug.Log ("PLAY");
            
            auds.clip = Resources.Load<AudioClip>("Vocab/Genki1/" + i);
        }
        else
        {
            auds.clip = Resources.Load<AudioClip>("ExtAudio/Comb/" + i);
        }
        
        auds.Play();
    }

    void GenerateNewLetter()
    {
        choice = getRandomLetter(Contents);
        GiveButtonsLetters(Contents);
       
        if(easy == true )
        {
            playAudioEquiptment(choice);
        }
        else
        {
            
        }
    }

    public int GetCurrentNumber() { return choice; }
        

    void GenerateDB()
    {

        if(kata == true)
        {
            Contents = CreateDB(Resources.Load("Info/Katakana") as TextAsset);
            ContentsAnswers = CreateDB(Resources.Load("Info/HiraganaA") as TextAsset);
             Debug.Log("kata selected");
        }
        else if(Hiragana == true)
        {
            Contents = CreateDB(Resources.Load("Info/Hiragana") as TextAsset);
            ContentsAnswers = CreateDB(Resources.Load("Info/HiraganaA") as TextAsset);
            Debug.Log("hira selected");
        }
        else if(kataExt == true)
        {
              Contents = CreateDB(Resources.Load("Info/KataKanaComb") as TextAsset);
              ContentsAnswers = CreateDB(Resources.Load("Info/CombA") as TextAsset);
        }
        else if(HiraganaEXT == true)
        {
            Contents = CreateDB(Resources.Load("Info/HiraganaComb") as TextAsset);
            ContentsAnswers = CreateDB(Resources.Load("Info/CombA") as TextAsset);
        }
        else if(vocab == true)
        {
            Contents = CreateDB(Resources.Load("Info/GENKILESSON1") as TextAsset);
            ContentsAnswers = CreateDB(Resources.Load("Info/GENKILESSON1A") as TextAsset);
        }
        else
        {
            Debug.Log("none selected");
        }

        correctness = new int[Contents.Length];
    }
    public string[] CreateDB(TextAsset dir)
    {
        string[] storage;

        storage = dir.text.Split(',');

        return storage;
    }

    public bool CheckGuess()
    {
        string currentA = ContentsAnswers[choice];
        return true;
        
    }

   bool CheckGuess(string guess)
   {
       if(guess == ContentsAnswers[choice])
        {
            
            intScore++;
            correctness[choice]++;
            Debug.Log("increase score");
            if(hard == true)
            {
            playAudioEquiptment(choice);
            StartCoroutine(plsWait());
            }

            GenerateNewLetter();
            previousChoice = choice;
            return true;
        }
        correctness[choice]--;
        intScore = 0;
        return false;
   }

    public bool ButtonPress(string guess)
    {
        return CheckGuess(guess);
    }

    void GiveButtonsLetters(string[] arrayChoice)
    {
        texts.GetComponent<TextMesh>().text = arrayChoice[choice];
        int previousL = 100;
        int lettertemp;

        for(int i = 0; i < gameButtons.Length; i++)
        {
             gameButtons[i].GetComponent<Button>().interactable = true;

            do{
                lettertemp = getRandomLetter(arrayChoice);
                
                gameButtons[i].GetComponent<ButtonController>().GetNewLetter(ContentsAnswers[lettertemp]);

            } while(lettertemp == previousL || lettertemp == choice);
            
            previousL = lettertemp;
        }

        int tempNum;
        tempNum = Random.Range(0, gameButtons.Length);

        gameButtons[tempNum].GetComponent<ButtonController>().GetNewLetter(ContentsAnswers[choice]);
    }
    

    public int getRandomLetter(string[] AlphabetType)
    {
        int lengthTemp;

        lengthTemp = AlphabetType.Length;

        do
        {
            return Random.Range(0, lengthTemp);
        }while(choice == previousChoice);   
    }

    public void BackButton()
    { 
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SavingSystem>().setScore(intScore);
        SceneManager.LoadScene(0);
    }

    public void MuteButton()
    {
        if(isMuted == false)
        {   
            auds.volume = 0f;
            isMuted = true;
        }
        else 
        {
            auds.volume = 1f;
            isMuted = false;
        }
    }

    void Update()
    {   
       Score.text = intScore.ToString();
    }

    IEnumerator plsWait()
    {
        yield return new WaitForSeconds(2);
    }


}
