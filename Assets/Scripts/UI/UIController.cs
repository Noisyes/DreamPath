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

    private void Awake()
    {
        Init();

    }

    private void Start()
    {
        if (GameData.isRestartGame == true)
        {
            OnStartButton();
        }
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

    }
}
