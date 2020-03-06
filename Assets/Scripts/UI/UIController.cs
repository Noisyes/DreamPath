using System.Runtime.InteropServices.ComTypes;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    private Button StartButton;
    private Button RankButton;
    private Button SoundButton;
    private Button ShopButton;
    private ManageVars Vars;
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        EventCenter.AddListener(EventDefine.FastLook,ToLookSkin);
        Init();
    }

    private void Start()
    {
        if (GameData.isRestartGame == true)
        {
            OnStartButton();
        }
        ShopButton.transform.Find("Image").GetComponent<Image>().sprite = Vars.CharacterSpriteList[GameCOntroller.Instance.SelectCharacterIndex];
    }
    
    void Init()
    {
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        StartButton.onClick.AddListener(OnStartButton);
        RankButton = GameObject.Find("RankButton").GetComponent<Button>();
        RankButton.onClick.AddListener(OnRankButton);
        SoundButton = GameObject.Find("SoundButton").GetComponent<Button>();
        SoundButton.onClick.AddListener(OnSoundButton);
        ShopButton = GameObject.Find("ShopButton").GetComponent<Button>();
        ShopButton.onClick.AddListener(OnShopButton);
        EventCenter.AddListener(EventDefine.ShowMainUI, ShowMainUI);
    }

    private void OnStartButton()
    {
        GameCOntroller.Instance.isGameStart = true;
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.GamePanel);
    }

    private void OnRankButton()
    {

    }

    private void OnSoundButton()
    {

    }

    private void OnShopButton()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowShopUI);
    }

    private void ShowMainUI()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainUI,ShowMainUI);
        EventCenter.RemoveListener(EventDefine.FastLook,ToLookSkin);
    }

    void ToLookSkin()
    {
         ShopButton.transform.Find("Image").GetComponent<Image>().sprite = Vars.CharacterSpriteList[GameCOntroller.Instance.SelectCharacterIndex];
    }
}
