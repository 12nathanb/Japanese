using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class lang : MonoBehaviour
{
        
    string[] Contents;
     public string[] ContentsAnswers;
    public GameObject texts;
   public int choice ;
   int previousChoice;
    public AudioSource auds;
    public GameObject[] test;
    public bool kata, Hiragana, kataExt, HiraganaEXT,vocab, Brandom, inOrder;
    
    bool easy, hard;

    public GameObject[] gameButtons;

    public GameObject manager;
    GameObject BManger;

    bool isMuted = false ;
    
    public GameObject pauseScreen;

    string gameType;

    // Start is called before the first frame update
    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameController");
        kata = manager.GetComponent<SavingSystem>().getKatakanaBool();
        Hiragana = manager.GetComponent<SavingSystem>().getHiraganaBool();
        kataExt= manager.GetComponent<SavingSystem>().getKatakanaEXTBool();
        HiraganaEXT = manager.GetComponent<SavingSystem>().getHiraganaEXTBool();
        easy  = manager.GetComponent<SavingSystem>().getDiffEasy();

        hard  = manager.GetComponent<SavingSystem>().getDiffHard();
        vocab = manager.GetComponent<SavingSystem>().getVocabBool();
        BManger = GameObject.FindGameObjectWithTag("ButtonM");

        Brandom = manager.GetComponent<SavingSystem>().getRan();
        inOrder = manager.GetComponent<SavingSystem>().getIn();

        

        if(kata == true)
        {
            gameType = "Katakana";
            SaveData tempdata = SavingSystem.Load(gameType);
            
        }
        else if(Hiragana == true)
        {
            gameType = "Hiragana";
        }
        else if(HiraganaEXT == true)
        {
            gameType = "HiraganaE";
        }
        else if(kataExt == true)
        {
            gameType = "KatakanaE";
        }
        else if(vocab == true)
        {
            gameType = "Vocab";
        }

        

        if(kata == true || Hiragana == true)
        {
            BManger.GetComponent<buttonManager>().setSingle();
            texts.GetComponent<TextMesh>().characterSize = 1;
            gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
            test = GameObject.FindGameObjectsWithTag("res");
        }
        else if(vocab == true)
        {
            BManger.GetComponent<buttonManager>().setVocab();
            texts.GetComponent<TextMesh>().characterSize = 0.3f;
            gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
            for(int i = 0; i < gameButtons.Length; i++)
            {
                gameButtons[i].GetComponent<ButtonController>().setSize(60);
            }
        }
        else
        {
            BManger.GetComponent<buttonManager>().setSingle();
            gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
            texts.GetComponent<TextMesh>().characterSize = 0.7f;
        }

        if (inOrder == true)
        {
            choice = 0;
        }
       
        GenerateDB();
        GenerateNewLetter();
         manager.GetComponent<SavingSystem>().loadArray(gameType);
    }

    public void buttonAudio()
    {
        if(easy == true)
        {
            playAudioEquiptment();
        }
    }


    void playAudioEquiptment()
    {
        if(kata == true || Hiragana == true)
        {
            auds.clip = Resources.Load<AudioClip>("audio/" + choice);
        }
        else if(vocab == true)
        {
           
            
            auds.clip = Resources.Load<AudioClip>("Vocab/Genki1/" + choice);
        }
        else
        {
            auds.clip = Resources.Load<AudioClip>("ExtAudio/Comb/" + choice);
        }
        
        auds.Play();
    }

    void GenerateNewLetter()
    {
         if(inOrder == true && choice < Contents.Length)
        {
            choice = getRandomLetter(Contents);
            GiveButtonsLetters(Contents);
        }

        if(Brandom == true)
        {
            choice = getRandomLetter(Contents);
            GiveButtonsLetters(Contents);
        }
        
      
        if(easy == true )
        {
             StartCoroutine(plsWait());
            playAudioEquiptment();
        }
        
    }
        

    void GenerateDB()
    {

        if(kata == true)
        {
            Contents = CreateDB(Resources.Load("Info/Katakana") as TextAsset);
            ContentsAnswers = CreateDB(Resources.Load("Info/HiraganaA") as TextAsset);
           
        }
        else if(Hiragana == true)
        {
            Contents = CreateDB(Resources.Load("Info/Hiragana") as TextAsset);
            ContentsAnswers = CreateDB(Resources.Load("Info/HiraganaA") as TextAsset);
            
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
         
        }

        manager.GetComponent<SavingSystem>().setSize(Contents);
    }
    
    public string[] CreateDB(TextAsset dir)
    {
        string[] storage;

        storage = dir.text.Split(',');

        return storage;
    }

   bool CheckGuess(string guess)
   {
       
       
       if(guess == ContentsAnswers[choice])
        {
       
            manager.GetComponent<SavingSystem>().setScoreArray(choice, 1);
            if(hard == true)
            {
                playAudioEquiptment();
                StartCoroutine(plsWait());
            }

            
            if(inOrder == true)
            {
                    choice++;
               
                    if (checkArrayAmount() == false)
                 {
           BackButton();
       }
                
            }

            GenerateNewLetter();
            previousChoice = choice;
            

            return true;
        }
        manager.GetComponent<SavingSystem>().setScoreArray(choice, -1);
        return false;
   }

   bool checkArrayAmount()
   {
       if(choice < Contents.Length)
       {
           return true;
       }
       else
       {
           return false;
       }
   }

    public bool ButtonPress(string guess)
    {
        if (checkArrayAmount() == true)
        {
             return CheckGuess(guess);
        }
        else
        {
            BackButton();
            return false;
        }

        
    }

    void GiveButtonsLetters(string[] arrayChoice)
    {
        texts.GetComponent<TextMesh>().text = arrayChoice[choice];
        int lettertemp;

        for(int i = 0; i < gameButtons.Length; i++)
        {
             gameButtons[i].GetComponent<Button>().interactable = true;

            do{
                lettertemp = Random.Range(0, arrayChoice.Length);
                
                gameButtons[i].GetComponent<ButtonController>().GetNewLetter(ContentsAnswers[lettertemp]);

            } while(lettertemp == choice);
        }

        int tempNum;
        tempNum = Random.Range(0, gameButtons.Length);

        gameButtons[tempNum].GetComponent<ButtonController>().GetNewLetter(ContentsAnswers[choice]);
    }
    

    public int getRandomLetter(string[] AlphabetType)
    {   
        int index = 0;

        if(Brandom ==true)
        {
            index = Random.Range(0, AlphabetType.Length);
        }
        else
        {
            index = choice;
        }
                 
        return index;
        
    }

    public void BackButton()
    { 

        
       
        SceneManager.LoadScene(0);
    }

    public void PauseButton()
    {
        pauseScreen.SetActive(true); 
    }
    public void ClosePauseButton()
    {
        
        pauseScreen.SetActive(false);
    }

    public void OpenScore()
    {
         int[] tp = manager.GetComponent<SavingSystem>().getStructData();
         SavingSystem.Save(gameType,tp);
        SceneManager.LoadScene(4);
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

    IEnumerator plsWait()
    {
        yield return new WaitForSeconds(2);
    }

}
