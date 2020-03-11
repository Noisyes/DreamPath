using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    private Button StartButton;
    private Button RankButton;
    private Button SoundButton;
    private Button ShopButton;
    private ManageVars Vars;

    private Button ResetButton;
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        EventCenter.AddListener(EventDefine.FastLook, ToLookSkin);
        Init();
    }

    private void Start()
    {
        if (GameData.isRestartGame == true)
        {
            OnStartButton();
        }
        ToLookSkin();
        if (GameCOntroller.Instance.isMusicOn)
        {
            Debug.Log("wah");
            SoundButton.transform.Find("Image").gameObject.SetActive(true);
            SoundButton.transform.Find("mute").gameObject.SetActive(false);
        }
        else
        {
            SoundButton.transform.Find("Image").gameObject.SetActive(false);
            SoundButton.transform.Find("mute").gameObject.SetActive(true);
        }
    }

    void Init()
    {
        StartButton = GameObject.Find("StartButton").GetComponent<Button>();
        StartButton.onClick.AddListener(OnStartButton);
        RankButton = GameObject.Find("RankButton").GetComponent<Button>();
        RankButton.onClick.AddListener(OnRankButton);
        SoundButton = GameObject.Find("SoundButton").GetComponent<Button>();

        EventCenter.Broadcast(EventDefine.Mute);
        SoundButton.onClick.AddListener(OnSoundButton);
        ShopButton = GameObject.Find("ShopButton").GetComponent<Button>();
        ShopButton.onClick.AddListener(OnShopButton);
        ResetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        ResetButton.onClick.AddListener(OnResetButton);
        EventCenter.AddListener(EventDefine.ShowMainUI, ShowMainUI);
    }

    private void OnStartButton()
    {
        if (GameCOntroller.Instance.isMusicOn)
            AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        GameCOntroller.Instance.isGameStart = true;
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.GamePanel);
    }
    private void OnResetButton()
    {
        if (GameCOntroller.Instance.isMusicOn)
            AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        EventCenter.Broadcast(EventDefine.ShowResetPanel);
    }

    private void OnRankButton()
    {
        if (GameCOntroller.Instance.isMusicOn)
            AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        EventCenter.Broadcast(EventDefine.ShowRankPanel);
    }

    private void OnSoundButton()
    {
        if (GameCOntroller.Instance.isMusicOn)
            AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        GameCOntroller.Instance.isMusicOn = !GameCOntroller.Instance.isMusicOn;
        if (GameCOntroller.Instance.isMusicOn)
        {
            SoundButton.transform.Find("Image").gameObject.SetActive(true);
            SoundButton.transform.Find("mute").gameObject.SetActive(false);
        }
        else
        {
            SoundButton.transform.Find("Image").gameObject.SetActive(false);
            SoundButton.transform.Find("mute").gameObject.SetActive(true);
        }
        EventCenter.Broadcast(EventDefine.Mute);
    }

    private void OnShopButton()
    {
        if (GameCOntroller.Instance.isMusicOn)
            AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowShopUI);
    }

    private void ShowMainUI()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainUI, ShowMainUI);
        EventCenter.RemoveListener(EventDefine.FastLook, ToLookSkin);
    }

    void ToLookSkin()
    {
        ShopButton.transform.Find("Image").GetComponent<Image>().sprite = Vars.CharacterSpriteList[GameCOntroller.Instance.SelectCharacterIndex];
    }
}
