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
    public GameObject texts;
   public int choice;
   int previousChoice;
    public AudioClip[] audioC;
    public AudioSource auds;

    public bool kata;
    public bool Hiragana;

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
        GenerateNewLetter();
        
    }

    void playAudioEquiptment(int i)
    {
        auds.clip = Resources.Load<AudioClip>("audio/" + i);
        auds.Play();
    }

    void GenerateNewLetter()
    {
        
        TextAsset tempA;
        TextAsset tempK;
        TextAsset tempH;

        tempH = Resources.Load("Info/Hiragana") as TextAsset;
        tempA = Resources.Load("Info/HiraganaA") as TextAsset;
        tempK = Resources.Load("Info/Katakana") as TextAsset;
        

        txtContentH = CreateDB(tempH);
        txtContentA = CreateDB(tempA);
        txtContentK = CreateDB(tempK);

        choice = getRandomLetter();

        if(kata == true)
        {
            GiveButtonsLetters(txtContentK);
        }
        else
        {
            GiveButtonsLetters(txtContentH);
        }
        

        playAudioEquiptment(choice);    

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

    public bool ButtonPress(string guess)
    {
        if(guess == txtContentA[choice])
        {
            if(isMuted == true)
            {
                intScore += 2;
            }
            else
            {
                 intScore++;
            }
           
            
            GenerateNewLetter();
            previousChoice = choice;
            return true;
        }
        intScore = 0;
        return false;
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
                gameButtons[i].GetComponent<ButtonController>().GetNewLetter(txtContentA[lettertemp]);
            } while(lettertemp == previousL || lettertemp == choice);
            

            previousL = lettertemp;



        }

        int tempNum;
        tempNum = Random.Range(0, gameButtons.Length);
        gameButtons[tempNum].GetComponent<ButtonController>().GetNewLetter(txtContentA[choice]);

    }
    

    public int getRandomLetter()
    {
        do
        {
            return Random.Range(0, txtContentH.Length);
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
}
