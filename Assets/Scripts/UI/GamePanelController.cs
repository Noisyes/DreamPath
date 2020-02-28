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
    private void Awake()
    {
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
    }
    void Update()
    {

    }
    void OnPlayButton()
    {
        PlayButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(true);
    }
    void OnPauseButton()
    {
        PlayButton.gameObject.SetActive(true);
        PauseButton.gameObject.SetActive(false);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
}
