using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    // Start is called before the first frame update

    private Text First;
    private Text Second;
    private Text Last;
    private void Awake()
    {
        First = transform.Find("FirstScore/Text").GetComponent<Text>();
        Second = transform.Find("SecondScore/Text").GetComponent<Text>();
        Last = transform.Find("LastScore/Text").GetComponent<Text>();
        GetComponent<Button>().onClick.AddListener(HideRankPanel);
        EventCenter.AddListener(EventDefine.ShowRankPanel, ShowRankPanel);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowRankPanel,ShowRankPanel);
    }
    void ShowRankPanel()
    {
        First.text = GameCOntroller.Instance.BestScore[0].ToString();
        Second.text = GameCOntroller.Instance.BestScore[1].ToString();
        Last.text = GameCOntroller.Instance.BestScore[2].ToString();
        gameObject.SetActive(true);
    }

    void HideRankPanel()
    {
        if(GameCOntroller.Instance.isMusicOn)
        AudioSource.PlayClipAtPoint(ManageVars.GetManageVars().ButtonClip,transform.position);
        gameObject.SetActive(false);
    }
}
