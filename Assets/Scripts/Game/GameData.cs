using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable] public class GameData
{
    public static bool isRestartGame { get; set; } = false;
    public bool isFirstGame{get;set;}
    public bool isMusicOn{get;set;}
    public int[] BestScore{get;set;}

    public int SelectCharacterIndex{get;set;}

    public bool[] CharacterIsUnlock{get;set;}

    public int DiamondCount{get;set;}
}
