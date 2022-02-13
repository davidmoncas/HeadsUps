using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

/// <summary>
/// Saves and loads the state of the game
/// </summary>
public class SaveManager : Singleton<SaveManager>
{

    // NOTE: to add a new type of data to load/save do the following:
    // create a serializable class with the needed fields, (only primitive types)
    // create the load and save methods. Save should return the serializable object from last step. Load should take it as argument
    // register the serializable object in the SerializableDataObject class. 
    // register the save method to the GenerateJsonObject Method.
    // Register the load method in the Start() function

    // NOTE II: This script should be at start (or in the firsts ones) in the execution order. 




    [Header("Managers")]
    TimeFetcher timeFetcher;
    PlayerMovement playerMovement;

    static public bool everythingLoaded;
    [Header("Scene references")]


    private string key = "%C*F-JaNdRgUkXp2";
    private string IV = "w9z$C&F)J@NcRfUj";

    private string fileName = "/data.json";
    private bool encrypt = false;

    byte[] key_bytes;
    byte[] IV_bytes;

    public int timesAppHasBeenPlayed = 0;


    private void Start()
    {
        timeFetcher = FindObjectOfType<TimeFetcher>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        key_bytes = Encoding.ASCII.GetBytes(key);
        IV_bytes = Encoding.ASCII.GetBytes(IV);
        print(Application.persistentDataPath);

        print("----------starting save manager");

        if (System.IO.File.Exists(Application.persistentDataPath + fileName))
        {
            string decryptedData;

            if (encrypt)
            {
                byte[] encryptedData = File.ReadAllBytes(Application.persistentDataPath + fileName);
                decryptedData = Encryption.DecryptStringFromBytes_Aes(encryptedData, key_bytes, IV_bytes);
            }

            else
            {
                decryptedData = File.ReadAllText(Application.persistentDataPath + fileName);
            }


            SerializableDataObject data = JsonUtility.FromJson<SerializableDataObject>(decryptedData);


            if (data.UserData != null) LoadUserData(data.UserData);
            else SetInitialValues();

        }
        else
        {  //Initialize everything from scratch, no data saved previously

            EventManager.TriggerEvent("No Saved Data" );
            SetInitialValues();
        }

        if (System.IO.File.Exists(Application.persistentDataPath + "/settingsData.json"))
        {
            LoadSettingsData();
        }
        EventManager.TriggerEvent("Everything Loaded");
        EventManager.TriggerEvent("Set Times Played", timesAppHasBeenPlayed);
        everythingLoaded = true;
        print("Everything loaded in: " + Application.persistentDataPath);
    }

    private void SaveSettingsData()
    {

        SerializableSettingsData settingsData = new SerializableSettingsData();
        settingsData.soundOn = GameManager.Instance.audioOn;
        settingsData.vibrationOn = GameManager.Instance.vibrationOn;
        string settingsDataString = JsonUtility.ToJson(settingsData);

        byte[] encripted = Encryption.EncryptStringToBytes_Aes(settingsDataString, key_bytes, IV_bytes);
        File.WriteAllBytes(Application.persistentDataPath + "/settingsData.json", encripted);

        print("------ Saving settings data: sound" + settingsData.vibrationOn + "  vibration: " + settingsData.vibrationOn);
    }
    private void LoadSettingsData()
    {
        byte[] userScoreData = File.ReadAllBytes(Application.persistentDataPath + "/settingsData.json");
        string decrypted = Encryption.DecryptStringFromBytes_Aes(userScoreData, key_bytes, IV_bytes);
        SerializableSettingsData settingsData = JsonUtility.FromJson<SerializableSettingsData>(decrypted);
        GameManager.Instance.vibrationOn = settingsData.vibrationOn;
        GameManager.Instance.audioOn = settingsData.soundOn;


        print("------ Loading settings data: sound" + settingsData.vibrationOn + "  vibration: " + settingsData.vibrationOn);

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save() {
        string json = GenerateJsonObject();
        SaveEncryptedData(json);
        SaveSettingsData();

    }

    #region Load Methods

    void SetInitialValues() {
        ScoreManager.Instance.score = 0;
        ScoreManager.Instance.lifes = 10;
    }


    void LoadUserData(SerializableUserData data)
    {

        ScoreManager.Instance.highestScore = data.highScore;
        ScoreManager.Instance.lifes = data.lifes;

        //playerMovement.speedX = data.speedX;
        //playerMovement.speedY = data.speedY;
        //playerMovement.medianSpeedX = data.medianSpeedX;

        GameManager.Instance.watchAdvertisements = data.showAdvertisements;

        timesAppHasBeenPlayed = data.timesAppWasOpened;
    }



    #endregion

    [ContextMenu("Reset the scene")]
    public void ResetGame()
    {

        ScoreManager.Instance.highestScore = 0;
        ScoreManager.Instance.lifes = 10;

        //playerMovement.speedX = 6;
        //playerMovement.speedY = 4;
        //playerMovement.medianSpeedX = 0.005f;


        File.Delete(Application.persistentDataPath + fileName);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);

    }

    #region Save methods

    String GenerateJsonObject()
    {
        SerializableDataObject data = new SerializableDataObject
        {
            UserData = GetUserData(),
            
        };
        string jsonString = JsonUtility.ToJson(data);
        return jsonString;
    }



    void SaveEncryptedData(string jsonData)
    {
        if (encrypt)
        {
            byte[] encripted = Encryption.EncryptStringToBytes_Aes(jsonData, key_bytes, IV_bytes);
            File.WriteAllBytes(Application.persistentDataPath + fileName, encripted);
        }

        else
        {
            File.WriteAllText(Application.persistentDataPath + fileName, jsonData);
        }
    }

   


    SerializableUserData GetUserData()
    {

        SerializableUserData userData = new SerializableUserData
        {

            highScore = ScoreManager.Instance.highestScore,
            lifes = ScoreManager.Instance.lifes,

            //speedX = playerMovement.speedX,
            //speedY = playerMovement.speedY,
            //medianSpeedX = playerMovement.medianSpeedX,



            showAdvertisements = GameManager.Instance.watchAdvertisements,
            timesAppWasOpened = this.timesAppHasBeenPlayed + 1
        };

        return userData;
    }




    #endregion



    #region Serializable objects

    // add the needed data objects to this class
    [Serializable]
    class SerializableDataObject
    {
        public SerializableUserData UserData;
    }



    [Serializable]
    class SerializableUserData
    {
        public int highScore;
        public int lifes;
        public string lastDateTimeGameClosed = "01/01/0001 00:00:00";
        public int currentDailyAdsWatched;
        public int interstitialsWatched;
        public bool freshInstall;
        public int timesAppWasOpened;
        public bool showAdvertisements;

        //movement variables
        //public float speedX;
        //public float speedY;
        //public float medianSpeedX; 

    }


    [Serializable]
    public class SerializableSettingsData
    {
        public bool soundOn;
        public bool vibrationOn;

    }


    #endregion
}
