using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class lang : MonoBehaviour
{
     string txtFileH;
     string txtFileA;
    string txtFileK;
     string[] txtContentH;
     public string[] txtContentA;
    string[] txtContentK;
    public GameObject texts;
    public Text uiT;
    public Text ifd;
   public int choice;
    public AudioClip[] audioC;
    public AudioSource auds;

    public bool kata;
    public bool Hiragana;

    public GameObject[] gameButtons;
    // Start is called before the first frame update
    void Start()
    {
        gameButtons = GameObject.FindGameObjectsWithTag("Buttons");
        audioC = Resources.LoadAll<AudioClip>(Application.dataPath + "/Audio");
        GenerateNewLetter();
      
        
        
    }

    void playAudioEquiptment(int i)
    {
        auds.clip = Resources.Load<AudioClip>("audio/" + i);
        auds.Play();
    }

    void GenerateNewLetter()
    {
        ifd.text = "";
        txtFileH = Application.dataPath + "/Info/Hiragana.txt";
        txtContentH = CreateDB(txtFileH);

        txtFileA = Application.dataPath + "/Info/HiraganaA.txt";
        txtContentA = CreateDB(txtFileA);

        txtFileK = Application.dataPath + "/Info/Katakana.txt";
        txtContentK = CreateDB(txtFileK);

        choice = getRandomLetter();

        GiveButtonsLetters();

        playAudioEquiptment(choice);    

    }

    public int GetCurrentNumber() { return choice; }
        

    // Update is called once per frame
    public string[] CreateDB(string dir)
    {
        string temp;
        string[] storage;

       

        StreamReader reader = new StreamReader(dir);
        temp = reader.ReadToEnd();
        storage = temp.Split(',');
        reader.Close();

        return storage;
    }

    public bool CheckGuess()
    {
        string currentA = txtContentA[choice];

        if (ifd.text.ToUpper() == currentA)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    public void ButtonPress(string guess)
    {
        if(guess == txtContentA[choice])
        {
            GenerateNewLetter();
        }
    }

    void GiveButtonsLetters()
    {
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
       


        texts.GetComponent<TextMesh>().text = txtContentK[choice];
    }
}
