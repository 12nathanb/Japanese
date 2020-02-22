using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class lang : MonoBehaviour
{
     string txtFileH;
     string txtFileA;
     string[] txtContentH;
     public string[] txtContentA;
    public GameObject texts;
    public Text uiT;
    public Text ifd;
   public int choice;
    public AudioClip[] audioC;
    public AudioSource auds;
    // Start is called before the first frame update
    void Start()
    {
        audioC = Resources.LoadAll<AudioClip>(Application.dataPath + "/Audio");
        GenerateNewLetter();

        
        
    }

    void playAudioEquiptment(int i)
    {
        
        auds.clip = Resources.Load<AudioClip>("Audio/" + i);
        auds.Play();
    }

    void GenerateNewLetter()
    {
        ifd.text = "";
        txtFileH = Application.dataPath + "/Info/Hiragana.txt";
        txtContentH = CreateDB(txtFileH);

        txtFileA = Application.dataPath + "/Info/HiraganaA.txt";
        txtContentA = CreateDB(txtFileA);

        choice = getRandomLetter();

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

    public void ButtonPress()
    {
        if(CheckGuess() == true)
        {
            GenerateNewLetter();
        }
    }

    public int getRandomLetter()
    {
        return Random.Range(0, txtContentH.Length);
    }

    void Update()
    {
       


        texts.GetComponent<TextMesh>().text = txtContentH[choice];
    }
}
