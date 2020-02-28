using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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
    public bool isGamepause = false;

    public Sprite curTheme = null;
}
