using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class ShopPanleController : MonoBehaviour
{
    ManageVars Vars;
    private Transform ScrollRect;

    private GameObject Choose;

    private Button BuyButton;
    private Button SelectButton;

    private Button BackToMainButton;

    private Text DiamondText;

    

    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        EventCenter.AddListener(EventDefine.ShowShopUI, ShowShopUI);
        EventCenter.AddListener<bool>(EventDefine.ShowBuyOrSelect, ShowBuyOrSelectButton);
        BuyButton = transform.Find("BuyButton").GetComponent<Button>();
        BuyButton.onClick.AddListener(BuyCharacter);
        SelectButton = transform.Find("SelectButton").GetComponent<Button>();
        SelectButton.onClick.AddListener(SelectCharacter);
        BackToMainButton = transform.Find("BackButton_Shop").GetComponent<Button>();
        BackToMainButton.onClick.AddListener(BackTOMain);
        DiamondText = transform.Find("DiamonPanel/Diamond_Shop/Text").GetComponent<Text>();
        ScrollRect = transform.Find("ScrollRect");
        Choose = transform.Find("ScrollRect/Choose").gameObject;
        gameObject.SetActive(false);
    }

    void ShowShopUI()
    {
        gameObject.SetActive(true);
        DiamondText.text = GameCOntroller.Instance.DiamondCount.ToString();
    }

    void BackTOMain()
    {
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip,transform.position);
        EventCenter.Broadcast(EventDefine.ShowMainUI);
        gameObject.SetActive(false);
    }
    void BuyCharacter()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip,transform.position);
        int index = transform.Find("ScrollRect").GetComponent<DragController>().TOBackScrollIndex;
        int result = Vars.CharacterCost[index];
        if (GameCOntroller.Instance.DiamondCount >= result)
        {
            GameCOntroller.Instance.DiamondCount -= result;
            GameCOntroller.Instance.CharacterIsUnlock[index] = true;
            ShowBuyOrSelectButton(true);
            DiamondText.text = GameCOntroller.Instance.DiamondCount.ToString();
            EventCenter.Broadcast(EventDefine.ShowGreyForCharacter);
        }
        else
        {
            Text textForDiamond = BuyButton.transform.Find("Text").GetComponent<Text>();
            textForDiamond.DOColor(Color.red,0.1f).SetEase(Ease.InBounce).SetLoops(3).From();
        }
    }
    void SelectCharacter()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip,transform.position);
        int index = transform.Find("ScrollRect").GetComponent<DragController>().TOBackScrollIndex;
        Debug.Log(index);
        Vars.SkinChoose.GetComponent<Image>().sprite = Vars.BackCharacter[index];
        GameCOntroller.Instance.SelectCharacterIndex = index;
        EventCenter.Broadcast(EventDefine.CharacterChangeSkin);
        EventCenter.Broadcast(EventDefine.FastLook);
        BackTOMain();
    }

    void ShowBuyOrSelectButton(bool isUnlock)
    {
        if (isUnlock)
        {
            SelectButton.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(false);
        }
        else
        {
            ShowCost();
            SelectButton.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopUI, ShowShopUI);
        EventCenter.RemoveListener<bool>(EventDefine.ShowBuyOrSelect, ShowBuyOrSelectButton);
    }

    void ShowCost()
    {
        int index = transform.Find("ScrollRect").GetComponent<DragController>().TOBackScrollIndex;
        BuyButton.transform.Find("Text").GetComponent<Text>().text = "$ " + Vars.CharacterCost[index].ToString();
    }
}
