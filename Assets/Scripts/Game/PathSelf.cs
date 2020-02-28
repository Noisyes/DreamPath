using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

using UnityEngine;

public class PathSelf : MonoBehaviour
{
    public List<SpriteRenderer> curSpriteRender = new List<SpriteRenderer>();
    public bool isSpike = false;

    private ManageVars Vars = null;

    private Vector3? NextPos = null;

    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
    }
    public void Init(Sprite SelectedSprite)
    {
        for (int i = 0; i < curSpriteRender.Count; i++)
        {
            curSpriteRender[i].sprite = SelectedSprite;
        }
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (isSpike)
        {
            int SpikePathContinueNumber = Random.Range(2, 4);
            SpikePathContinue(SpikePathContinueNumber);
            EventCenter.AddListener<int>(EventDefine.SpikeContinue, SpikePathContinue);
        }
    }

    public void SpikePathContinue(int SpikePathContinueNumber)
    {
        //int SpikePathContinueNumber = Random.Range(2, 4);
        Transform Obstacle = transform.Find("Obstacle");
        if (Obstacle.localPosition.x < 0)
        {
            if(NextPos == null)
            NextPos = new Vector3(Vars.LeftDir.x, Vars.LeftDir.y, 0);
            InstantiateSpike(Vars.LeftDir, SpikePathContinueNumber, Obstacle);
        }
        else
        {
            if(NextPos == null )
            NextPos = new Vector3(Vars.RightDir.x, Vars.RightDir.y, 0);
            InstantiateSpike(Vars.RightDir, SpikePathContinueNumber, Obstacle);
        }
    }

    void InstantiateSpike(Vector2 dirToInstantiate, int number, Transform parent)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject go = Instantiate(Vars.PathPrefab, parent);
            go.GetComponent<PathSelf>().Init(GameCOntroller.Instance.curTheme);
            go.transform.localPosition = (Vector3)NextPos;
            NextPos += new Vector3(dirToInstantiate.x, dirToInstantiate.y, 0);
            Debug.Log(NextPos);
        }
    }

    private void OnDestroy()
    {
        if(isSpike)
            EventCenter.RemoveListener<int>(EventDefine.SpikeContinue,SpikePathContinue);
    }
}
