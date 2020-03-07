using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ResetPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private Button YesButton;
    private Button NOButton;
    private ManageVars Vars;
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        YesButton = transform.Find("YesButton").GetComponent<Button>();
        YesButton.onClick.AddListener(OnYesButton);
        NOButton = transform.Find("NoButton").GetComponent<Button>();
        NOButton.onClick.AddListener(OnNoButton);
        EventCenter.AddListener(EventDefine.ShowResetPanel, ShowResetPanel);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowResetPanel,ShowResetPanel);
    }

    void OnYesButton()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip,transform.position);
        GameCOntroller.Instance.isMusicOn = true;
        GameCOntroller.Instance.BestScore = new int[3];
        GameCOntroller.Instance.CharacterIsUnlock = new bool[Vars.CharacterSpriteList.Count];
        GameCOntroller.Instance.CharacterIsUnlock[0] = true;
        GameCOntroller.Instance.SelectCharacterIndex = 0;
        GameCOntroller.Instance.DiamondCount = 10;
        GameCOntroller.Instance.isFirstGame = false;
        GameCOntroller.Instance.Restore();
        EventCenter.Broadcast(EventDefine.FastLook);
        Vars.SkinChoose.GetComponent<Image>().sprite = Vars.BackCharacter[0];
        gameObject.SetActive(false);
    }
    void OnNoButton()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip,transform.position);
        gameObject.SetActive(false);
    }

    void ShowResetPanel()
    {
        gameObject.SetActive(true);
    }
}
