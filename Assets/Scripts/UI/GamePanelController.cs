using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
public class GamePanelController : MonoBehaviour
{
    // Start is called before the first frame update]
    private Button PlayButton;
    private Button PauseButton;
    private Text ScoreText;
    private Text DiamondCount;

    public int Score { get; set; } = 0;
    public int DiamondScore { get; set; } = 0;

    public int LastScore { get; set; } = 0;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ScoreShow, ScoreUpdate);
        EventCenter.AddListener(EventDefine.GamePanel, Show);
        EventCenter.AddListener(EventDefine.DiamondScoreUp, DiamondScoreToUp);
        PlayButton = transform.Find("PlayButton").GetComponent<Button>();
        PlayButton.onClick.AddListener(OnPlayButton);
        PauseButton = transform.Find("PauseButton").GetComponent<Button>();
        PauseButton.onClick.AddListener(OnPauseButton);
        ScoreText = transform.Find("Score").GetComponent<Text>();
        DiamondCount = transform.Find("Diamond/diamondCount").GetComponent<Text>();
        gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
    }

    // Update is called once per frame

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ScoreShow, ScoreUpdate);
        EventCenter.RemoveListener(EventDefine.DiamondScoreUp, DiamondScoreToUp);
        EventCenter.RemoveListener(EventDefine.GamePanel, Show);
    }
    void OnPlayButton()
    {
        PlayButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        GameCOntroller.Instance.isGamePause = true;
    }
    void OnPauseButton()
    {
        PlayButton.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        GameCOntroller.Instance.isGamePause = false;
    }
    void Show()
    {
        gameObject.SetActive(true);
    }

    void ScoreUpdate()
    {
        Score++;
        if (Score - LastScore >= 50)
        {
            LastScore = Score;
            GameCOntroller.Instance.TimeToFall -= 0.4f;
            if (GameCOntroller.Instance.TimeToFall < 0.3f)
            {
                GameCOntroller.Instance.TimeToFall = 0.3f;
            }
        }
        ScoreText.text = Score.ToString();
    }
    void DiamondScoreToUp()
    {
        DiamondScore++;
        DiamondCount.text = DiamondScore.ToString();
    }
}
