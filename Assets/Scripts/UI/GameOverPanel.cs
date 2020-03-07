using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text CurScore;
    public Text BestScore;

    public Text DiamondScore;

    private Button TryAgain;
    private Button RankButton;
    private Button HomeButton;

    public GameObject GamePanel;

    public GameObject NewImage;

    private void Awake()
    {
        NewImage = transform.Find("NewImage").gameObject;
        CurScore = transform.Find("CurScore").GetComponent<Text>();
        BestScore = transform.Find("BestScore").GetComponent<Text>();
        DiamondScore = transform.Find("Diamond/DiamondScore").GetComponent<Text>();
        TryAgain = transform.Find("TryAgain").GetComponent<Button>();
        TryAgain.onClick.AddListener(OnTryAgain);
        RankButton = transform.Find("Button/RankButton").GetComponent<Button>();
        RankButton.onClick.AddListener(OnRankButton);
        HomeButton = transform.Find("Button/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(OnHomeButtonDown);
        EventCenter.AddListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
        gameObject.SetActive(false);
        NewImage.SetActive(false);
    }

    void ShowGameOverPanel()
    {
        CurScore.text = GamePanel.GetComponent<GamePanelController>().Score.ToString();
        for (int i = 0; i < 3; i++)
        {
            if (GamePanel.GetComponent<GamePanelController>().Score > GameCOntroller.Instance.BestScore[i])
            {
                for (int j = 2; j > i; j--)
                {
                    GameCOntroller.Instance.BestScore[j] = GameCOntroller.Instance.BestScore[j - 1];
                }
                GameCOntroller.Instance.BestScore[i] = GamePanel.GetComponent<GamePanelController>().Score;
                if (i == 0)
                    NewImage.SetActive(true);
                break;
            }
        }
        BestScore.text = "最高分  " + GameCOntroller.Instance.BestScore[0].ToString();
        DiamondScore.text = "+" + GamePanel.GetComponent<GamePanelController>().DiamondScore.ToString();
        GamePanel.SetActive(false);
        GameCOntroller.Instance.Restore();
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
    }

    void OnHomeButtonDown()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isRestartGame = false;
    }
    void OnTryAgain()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isRestartGame = true;
    }

    void OnRankButton()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip, transform.position);
        EventCenter.Broadcast(EventDefine.ShowRankPanel);
    }
}
