using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class GameCOntroller : MonoBehaviour
{
    private static GameCOntroller _instance;
    public static GameCOntroller Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    
    public bool isGameStart = false;
    public bool isGamePause = false;
    public bool isGameOver = false;
    public Sprite curTheme = null;

    public Text Weak;

    public float TimeToFall {get;set;} = 5f;
}
