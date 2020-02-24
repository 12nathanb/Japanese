using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class lang : MonoBehaviour
{
    
     string[] txtContentH;
     public string[] txtContentA;
    string[] txtContentK;
    public string[] txtContentKE;
    public string[] txtContentHE;
    public string[] txtContentExtA;
    public GameObject texts;
   public int choice;
   int previousChoice;
    public AudioClip[] audioC;
    public AudioSource auds;

    public bool kata;
    public bool Hiragana;
    public bool kataExt;
    public bool HiraganaEXT;
    bool easy;
    bool medium;
    bool hard;
    public GameObject[] gameButtons;

    GameObject manager;

    bool isMuted = false ;

    GameObject MCam;

    public Text Score;
    int intScore;
    


    // Start is called before the first frame update
    void Start()
    {
        intScore = 0;
        MCam = GameObject.FindGameObjectWithTag("MainCamera");
        //audioListener = MCam.GetComponent<AudioListener>();
        gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
        audioC = Resources.LoadAll<AudioClip>(Application.dataPath + "/Audio");
        manager = GameObject.FindGameObjectWithTag("GameController");
        kata = manager.GetComponent<SavingSystem>().getKatakanaBool();
        Hiragana = manager.GetComponent<SavingSystem>().getHiraganaBool();
        kataExt= manager.GetComponent<SavingSystem>().getKatakanaEXTBool();
        HiraganaEXT = manager.GetComponent<SavingSystem>().getHiraganaEXTBool();
        easy  = manager.GetComponent<SavingSystem>().getDiffEasy();
        medium  = manager.GetComponent<SavingSystem>().getDiffMedium();
        hard  = manager.GetComponent<SavingSystem>().getDiffHard();

        if(kata == true || Hiragana == true)
        {
            texts.GetComponent<TextMesh>().characterSize = 1;
        }
        else
        {
            texts.GetComponent<TextMesh>().characterSize = 0.7f;
        }

        GenerateNewLetter();
        
    }

    void playAudioEquiptment(int i)
    {
        if(kata == true || Hiragana == true)
        {
            auds.clip = Resources.Load<AudioClip>("audio/" + i);
        }
        else
        {
            auds.clip = Resources.Load<AudioClip>("ExtAudio/Comb/" + i);
        }
        
        auds.Play();
    }

    void GenerateNewLetter()
    {
        
        TextAsset tempA;
        TextAsset tempK;
        TextAsset tempH;
        TextAsset tempKE;
        TextAsset ExtendedA;
        TextAsset tempHE;

        tempH = Resources.Load("Info/Hiragana") as TextAsset;
        tempA = Resources.Load("Info/HiraganaA") as TextAsset;
        tempK = Resources.Load("Info/Katakana") as TextAsset;
        tempKE = Resources.Load("Info/KataKanaComb") as TextAsset;
        tempHE = Resources.Load("Info/HiraganaComb") as TextAsset;
        ExtendedA = Resources.Load("Info/CombA") as TextAsset;

        txtContentH = CreateDB(tempH);
        txtContentA = CreateDB(tempA);
        txtContentK = CreateDB(tempK);
        txtContentKE = CreateDB(tempKE);
        txtContentExtA = CreateDB(ExtendedA);
        txtContentHE = CreateDB(tempHE);

        choice = getRandomLetter();

        if(kata == true)
        {
            GiveButtonsLetters(txtContentK);
        }
        else if(Hiragana == true)
        {
            GiveButtonsLetters(txtContentH);
        }
        else if(kataExt == true)
        {
            GiveButtonsLetters(txtContentKE);
        }
        else if(HiraganaEXT == true)
        {
            GiveButtonsLetters(txtContentHE);
        }
        
        if(easy == true)
        {
            playAudioEquiptment(choice);
        }
            

    }

    public int GetCurrentNumber() { return choice; }
        

    
    public string[] CreateDB(TextAsset dir)
    {
        string[] storage;

       storage = dir.text.Split(',');

        return storage;
    }

    public bool CheckGuess()
    {
        string currentA = txtContentA[choice];
        return true;
        
    }

   bool CheckHiraAndKata(string guess)
   {
       if(guess == txtContentA[choice])
        {
            if(hard == true)
            {
                intScore += 2;
            }
            else
            {
                 intScore++;
            }
           
            if(hard == true)
            {
            playAudioEquiptment(choice);
            StartCoroutine(plsWait());
            }

            GenerateNewLetter();
            previousChoice = choice;
            return true;
        }
        intScore = 0;
        return false;
   }

   bool CheckHiraEXTAndKataEXT(string guess)
   {
       if(guess == txtContentExtA[choice])
        {
            if(hard == true)
            {
                intScore += 2;
            }
            else
            {
                 intScore++;
            }
            if(hard == true)
            {

                playAudioEquiptment(choice);
                StartCoroutine(plsWait());
            }
            
            GenerateNewLetter();
            previousChoice = choice;
            return true;
        }
        intScore = 0;
        return false;
   }

    public bool ButtonPress(string guess)
    {
        if(kata == true || Hiragana == true )
        {
           return CheckHiraAndKata(guess);
        }
        else
        {
            return CheckHiraEXTAndKataEXT(guess);
        }
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
                lettertemp = getRandomLetter();
                if(kata == true || Hiragana == true )
                {
                    gameButtons[i].GetComponent<ButtonController>().GetNewLetter(txtContentA[lettertemp]);
                }
                else
                {
                    gameButtons[i].GetComponent<ButtonController>().GetNewLetter(txtContentExtA[lettertemp]);
                }

                
            } while(lettertemp == previousL || lettertemp == choice);
            

            previousL = lettertemp;



        }

        int tempNum;
        tempNum = Random.Range(0, gameButtons.Length);

        if(kata == true || Hiragana == true )
        {
            gameButtons[tempNum].GetComponent<ButtonController>().GetNewLetter(txtContentA[choice]);
        }
        else
        {
            gameButtons[tempNum].GetComponent<ButtonController>().GetNewLetter(txtContentExtA[choice]);
        }
        

    }
    

    public int getRandomLetter()
    {
        int lengthTemp;

        if(kata == true || Hiragana == true )
        {
            lengthTemp = txtContentH.Length;
        }
        else
        {
            lengthTemp = txtContentKE.Length;
        }



        do
        {
            return Random.Range(0, lengthTemp);
        }while(choice == previousChoice);
        
    }

    public void BackButton()
    {
        
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
