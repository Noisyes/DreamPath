using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;

using UnityEngine;
public enum ThemeType
{
    Winter,
    Grass,
}

public class PathController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 NextPos;
    private GameObject PathPrefab;

    private int PathCount = 5;

    private ManageVars Vars;

    private bool Dir = false;
    private Sprite SelectedSprite;
    private GameObject CharacterPrefab;

    public ThemeType TypeTheme;
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
    }
    void Start()
    {
        RandomPlatform();
        NextPos = Vars.StartPos;
        PathPrefab = Vars.PathPrefab;
        CharacterPrefab = Vars.CharacterPrefabs;
        EventCenter.AddListener(EventDefine.PathCreate, IsChangeDir);
        for (int i = 0; i < 5; i++)
        {
            IsChangeDir();
        }
        Instantiate(CharacterPrefab, new Vector3(Vars.CharacterPos.x, Vars.CharacterPos.y, 0), Quaternion.identity);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.PathCreate, IsChangeDir);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void RandomPlatform()
    {
        int index = Random.Range(0, Vars.PlatformTheme.Count);
        SelectedSprite = Vars.PlatformTheme[index];
        GameCOntroller.Instance.curTheme = SelectedSprite;
        if (index == 2)
        {
            TypeTheme = ThemeType.Winter;

        }
        else
        {
            TypeTheme = ThemeType.Grass;
        }
    }
    void IsChangeDir()
    {
        if (PathCount > 1)
        {
            PathCount--;
            InstantiatePath();
        }
        else
        {
            PathCount = Random.Range(3, 6);
            PathCount--;
            Dir = !Dir;
            int randomNumber = Random.Range(0,3);
            if(randomNumber == 0 )
            {
                InstantiateCommonPath();
            }
            else if(randomNumber == 1)
            {
                InstantiateThemePath();
            }
            else
            {
                bool spikeDir = (Dir == false)? false:true;
                InstantiateSpikePath(spikeDir);  
            }
        }
    }

    void InstantiateSpikePath(bool left)
    {
        GameObject go;
        if(left)
        {
            go = Instantiate(Vars.SpikePath[0]);
        }
        else
        {
            go = Instantiate(Vars.SpikePath[1]);
        }
        go.GetComponent<PathSelf>().Init(SelectedSprite);
        if (Dir == false)
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.RightDir;
        }
        else
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.LeftDir;
        }
    }

    void InstantiatePath()
    {
        GameObject go = ObjectPool.Instance.GetPlatform(ref ObjectPool.Instance.NormalPlatform);
        go.SetActive(true);
        go.GetComponent<PathSelf>().Init(SelectedSprite);
        if (Dir == false)
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.RightDir;
        }
        else
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.LeftDir;
        }
    }

    void InstantiateCommonPath()
    {
        //int index = Random.Range(0, Vars.CommonPath.Count);
        GameObject go = ObjectPool.Instance.GetPlatform(ref ObjectPool.Instance.CommonPlatform);
        go.SetActive(true);
        go.GetComponent<PathSelf>().Init(SelectedSprite);
        if (Dir == false)
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.RightDir;
        }
        else
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.LeftDir;
        }
    }

    void InstantiateThemePath()
    {
        switch (TypeTheme)
        {
            case ThemeType.Winter:
                InstantiateWinterPath();
                break;
            case ThemeType.Grass:
                InstantiateGrassPath();
                break;
        }
    }

    void InstantiateWinterPath()
    {
       // int index = Random.Range(0, Vars.WinterPath.Count);
        GameObject go = ObjectPool.Instance.GetPlatform(ref ObjectPool.Instance.WinterPlatform);
        go.SetActive(true);
        go.GetComponent<PathSelf>().Init(SelectedSprite);
        if (Dir == false)
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.RightDir;
        }
        else
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.LeftDir;
        }
    }

    void InstantiateGrassPath()
    {
        //int index = Random.Range(0, Vars.GrassPath.Count);
        GameObject go = ObjectPool.Instance.GetPlatform(ref ObjectPool.Instance.GrassPlatform);
        go.SetActive(true);
        go.GetComponent<PathSelf>().Init(SelectedSprite);
        if (Dir == false)
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.RightDir;
        }
        else
        {
            go.transform.position = new Vector3(NextPos.x, NextPos.y, 0);
            NextPos += Vars.LeftDir; 
        }
    }

}
