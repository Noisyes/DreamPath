using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class GamePanelController : MonoBehaviour
{
    // Start is called before the first frame update]
    private Button PlayButton;
    private Button PauseButton;
    private Text ScoreText;
    private Text DiamondCount;

    private int Score = 0;

    private int LastScore = 0;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ScoreShow,ScoreUpdate);
        EventCenter.AddListener(EventDefine.GamePanel, Show);
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
        EventCenter.RemoveListener(EventDefine.GamePanel,OnPlayButton);
        EventCenter.RemoveListener(EventDefine.ScoreShow,ScoreUpdate);
    }
    void Update()
    {

    }
    void OnPlayButton()
    {
        PlayButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        Time.timeScale =0;
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
        if(Score - LastScore>=50)
        {
            LastScore = Score;
            GameCOntroller.Instance.TimeToFall -= 0.4f;
            if(GameCOntroller.Instance.TimeToFall<0.3f)
            {
                GameCOntroller.Instance.TimeToFall = 0.3f;
            }
        }
        ScoreText.text = Score.ToString();
    }
}
