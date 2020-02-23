using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class lang : MonoBehaviour
{
    
     string[] txtContentH;
     public string[] txtContentA;
    string[] txtContentK;
    public GameObject texts;
   public int choice;
    public AudioClip[] audioC;
    public AudioSource auds;

    public bool kata;
    public bool Hiragana;

    public GameObject[] gameButtons;

    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
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

    public void ButtonPress(string guess)
    {
        if(guess == txtContentA[choice])
        {
            GenerateNewLetter();
        }
    }

    void GiveButtonsLetters(string[] arrayChoice)
    {
        texts.GetComponent<TextMesh>().text = arrayChoice[choice];

        for(int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].GetComponent<ButtonController>().GetNewLetter(txtContentA[getRandomLetter()]);
            Debug.Log(1);
        }

        int tempNum;
        tempNum = Random.Range(0, gameButtons.Length);
        gameButtons[tempNum].GetComponent<ButtonController>().GetNewLetter(txtContentA[choice]);

    }
    

    public int getRandomLetter()
    {
        return Random.Range(0, txtContentH.Length);
    }

    void Update()
    {   
        
    }
}
