using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SavingSystem : MonoBehaviour
{
    public struct data
    {
        public string typeOfData;
        public int score;
    }
    public bool Hiragana;

    public data[] dataArray;
    public bool Katakana;

    public bool vocab;
    public bool KatakanaEXT;
    public bool HiraganaEXT;

    public bool easy;
    public bool medium;
    public bool hard;

    public bool random;
    public bool inOrder;

    public int arraySize;
public GameObject temp ;

    public int score;
    void Awake() 
    {
        DontDestroyOnLoad(this.gameObject); //Makes it So this item is not destroyed when scene changes

        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(this.gameObject); //this checks to see if there is already one and will delete the other
        }
        
    }

    public static bool Save(string type, int[] scores)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        string path = Application.persistentDataPath + type + ".save";
        FileStream file = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(type, scores);
        
        formatter.Serialize(file, data);

        file.Close();

        return true;
    } 

    public static SaveData Load(string type)
    {
        string path2 = Application.persistentDataPath + type + ".save";

        if(File.Exists(path2))
        {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream stream = new FileStream(path2, FileMode.Open);

            SaveData sData = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return sData;
        }else
        {
            Debug.LogError("FILE NOT FOUND");
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
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
    
    public void setSize(string[] i)
    {
        arraySize = i.Length;
        dataArray = new data[i.Length];

        for (int t = 0; t < i.Length; t++)
        {
            dataArray[t].typeOfData = i[t];
            Debug.Log(dataArray[t].typeOfData);
        }
    }

    public void LoadArray(data[] a)
    {
        dataArray = a;
    }

    public void setScoreArray(int choice, int scores)
    {
        dataArray[choice].score += scores;
         Debug.Log(dataArray[choice].typeOfData + " " + dataArray[choice].score );
    }

    public void loadArray(string type)
    {
        SaveData t = Load(type);

        if(type == "Katakana")
        {
            for (int i = 0; i < t.katadata.Length; i++)
            {
                dataArray[i].score = t.katadata[i];
            }

        }
        else if(type == "Hiragana")
        {
            for (int i = 0; i < t.Hiradata.Length; i++)
            {
                dataArray[i].score = t.Hiradata[i];
            }
        }
        else if(type == "HiraganaE")
        {
            for (int i = 0; i < t.hedata.Length; i++)
            {
                dataArray[i].score = t.hedata[i];
            }

        }
        else if(type == "KatakanaE")
        {
            for (int i = 0; i < t.kedata.Length; i++)
            {
                dataArray[i].score = t.kedata[i];
            }

        }
        else if(type == "Vocab")
        { 
            for (int i = 0; i < t.vocabdata.Length; i++)
            {
                dataArray[i].score = t.vocabdata[i];
            }
        }
        
    }

    public void setLangBool(bool k, bool h, bool ke, bool he, bool v)
    {
        Hiragana = h;
        Katakana = k;
        KatakanaEXT = ke;
        HiraganaEXT = he;
        vocab = v;
    }

    public string getStructType(int pos){return dataArray[pos].typeOfData;}
    public int getStructScore(int pos){return dataArray[pos].score;}
    public int[] getStructData()
    {
        int[] temp;

        temp = new int[dataArray.Length];
        
        for (int i = 0; i < dataArray.Length; i++)
        {
            temp[i] = dataArray[i].score;
        }
        
        return temp;
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

    public int getArraySize() {return arraySize;}
    public bool getVocabBool() {return vocab;}

    

}

[System.Serializable]
public class SaveData
{
    public int[] Hiradata;
    public int[] katadata;
    public int[] hedata;
    public int[] kedata;

    public int[] vocabdata;

    public SaveData(string type, int[] savingData)
    {
        if(type == "Hiragana")
        {
            Hiradata = savingData;
        }
        if(type == "Katakana")
        {
            katadata = savingData;
        }
        if(type == "HiraganaE")
        {
            hedata = savingData;
        }
        if(type == "KatakanaE")
        {
            kedata = savingData;
        }
        if(type == "Vocab")
        {
            vocabdata = savingData;
        }

        Debug.Log("SAVED");
    }

}