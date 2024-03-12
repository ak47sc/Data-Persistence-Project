using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance {get ; private set;}

    [Serializable]
    class SaveData{
        public string name;
        public int score;

        public SaveData(string name , int score){
            this.name = name;
            this.score = score;
        }
    }   

    public string playerName;
    public string highScorePlayerName;
    public int highScore;
    void Awake(){
        if(Instance != null){
            Destroy(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        LoadDataFromFile();
    }

    public void LoadDataFromFile(){
        string path = Path.Combine(Application.persistentDataPath , "save.json");
        Debug.Log(path);
        if(File.Exists(path)){
            string dataLoaded = File.ReadAllText(path);
            SaveData loadDataObject = JsonUtility.FromJson<SaveData>(dataLoaded);
            highScorePlayerName = loadDataObject.name;
            highScore = loadDataObject.score;
        }
    }
    public void SavaDataToFile(string name , int highScore){
        SaveData dataToSaveObj = new SaveData(name , highScore);
        string dataStringFormat = JsonUtility.ToJson(dataToSaveObj);
        File.WriteAllText(Application.persistentDataPath + "/save.json" , dataStringFormat);
    }
    public void NewPlayerName(InputField field){
        playerName = field.text;
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void ExitGame(){
        Application.Quit();
    }

}
