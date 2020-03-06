using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using UnityEngine.UI;
public class GameCOntroller : MonoBehaviour
{
    private static GameCOntroller _instance;
    private GameData data;

    private ManageVars Vars;
    public static GameCOntroller Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        InitGameData();
        _instance = this;
    }

    public void InitGameData()
    {
        Load();
        if (data != null)
        {
            isFirstGame = data.isFirstGame;
        }
        else
        {
            isFirstGame = true;
        }
        if (isFirstGame)
        {
            isMusicOn = true;
            BestScore = new int[3];
            CharacterIsUnlock = new bool[Vars.CharacterSpriteList.Count];
            CharacterIsUnlock[0] = true;
            SelectCharacterIndex = 0;
            DiamondCount = 10;
            isFirstGame = false;
            data = new GameData();
            Restore();
        }
        else
        {
            isFirstGame = data.isFirstGame;
            isMusicOn = data.isMusicOn;
            BestScore = data.BestScore;
            CharacterIsUnlock = data.CharacterIsUnlock;
            SelectCharacterIndex = data.SelectCharacterIndex;
            DiamondCount = data.DiamondCount;
        }
    }
    public bool isGameStart = false;
    public bool isGamePause = false;
    public bool isGameOver = false;
    public Sprite curTheme = null;
    public Text Weak;
    public bool isFirstGame { get; set; }
    public bool isMusicOn { get; set; }
    public int[] BestScore { get; set; }
    public int SelectCharacterIndex { get; set; }
    public bool[] CharacterIsUnlock { get; set; }

    public int DiamondCount { get; set; }
    public float TimeToFall { get; set; } = 5f;

    void Load()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream file = File.Open(Application.persistentDataPath + "/GameData.data", FileMode.Open))
            {
                data = (GameData) formatter.Deserialize(file);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void Restore()
    {
        try
        {
            BinaryFormatter formater = new BinaryFormatter();
            using(FileStream file = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                data.isFirstGame = isFirstGame;
                data.isMusicOn = isMusicOn;
                data.BestScore = BestScore;
                data.CharacterIsUnlock = CharacterIsUnlock;
                data.SelectCharacterIndex = SelectCharacterIndex;
                data.DiamondCount = DiamondCount;
                formater.Serialize(file, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
