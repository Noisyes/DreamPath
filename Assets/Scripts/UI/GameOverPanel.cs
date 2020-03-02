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

    private void Awake()
    {
        CurScore = transform.Find("CurScore").GetComponent<Text>();
        BestScore = transform.Find("BestScore").GetComponent<Text>();
        DiamondScore = transform.Find("Diamond/DiamondScore").GetComponent<Text>();
        TryAgain = transform.Find("TryAgain").GetComponent<Button>();
        TryAgain.onClick.AddListener(OnTryAgain);
        RankButton = transform.Find("Button/RankButton").GetComponent<Button>();
        HomeButton = transform.Find("Button/HomeButton").GetComponent<Button>();
        HomeButton.onClick.AddListener(OnHomeButtonDown);
        EventCenter.AddListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
        gameObject.SetActive(false);
    }

    void ShowGameOverPanel()
    {
        CurScore.text = GamePanel.GetComponent<GamePanelController>().Score.ToString();
        BestScore.text = "最高分  " + GamePanel.GetComponent<GamePanelController>().Score.ToString();
        DiamondScore.text = "+" + GamePanel.GetComponent<GamePanelController>().DiamondScore.ToString();
        gameObject.SetActive(true);
        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
    }

    void OnHomeButtonDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isRestartGame = false;
    }
    void OnTryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isRestartGame = true;
    }
}
