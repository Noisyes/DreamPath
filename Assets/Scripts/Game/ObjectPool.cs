using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            return _instance;
        }
    }

    public ManageVars Vars;
    private void Awake()
    {
        _instance = this;
        Vars = ManageVars.GetManageVars();
        Init();
    }

    public List<GameObject> NormalPlatform = new List<GameObject>();
    public List<GameObject> GrassPlatform = new List<GameObject>();
    public List<GameObject> WinterPlatform = new List<GameObject>();
    public List<GameObject> CommonPlatform = new List<GameObject>();

    public List<GameObject> SpikeLeftPlatform = new List<GameObject>();

    public List<GameObject> SpikeRightPlatform = new List<GameObject>();

    public int number = 5; //number to instantiate

    void Init()
    {
        NewCommonPlatform();
        NewGrassrPlatform();
        NewNormalPlatform();
        NewSpikeLeftlPlatform();
        NewSpikeRightPlatform();
        NewWinterPlatform();
    }
    public GameObject InstantiateObject(ref List<GameObject> list, GameObject prefab)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        list.Add(go);
        return go;
    }

    void NewNormalPlatform()
    {
        for (int i = 0; i < number; i++)
        {
            InstantiateObject(ref NormalPlatform, Vars.PathPrefab);
        }
    }

    void NewSpikeLeftlPlatform()
    {
        for (int i = 0; i < number; i++)
        {
            InstantiateObject(ref SpikeLeftPlatform, Vars.SpikePath[0]).GetComponent<PathSelf>().isSpike = true;
        }
    }

    void NewSpikeRightPlatform()
    {
        for (int i = 0; i < number; i++)
        {
            InstantiateObject(ref SpikeRightPlatform, Vars.SpikePath[1]).GetComponent<PathSelf>().isSpike = true;
        }
    }

    void NewWinterPlatform()
    {
        for (int i = 0; i < Vars.WinterPath.Count; i++)
        {
            for (int j = 0; j < number; j++)
            {
                int index = Random.Range(0, Vars.WinterPath.Count);
                InstantiateObject(ref WinterPlatform, Vars.WinterPath[i]);
            }
        }
    }

    void NewGrassrPlatform()
    {
        for (int i = 0; i < Vars.GrassPath.Count; i++)
        {
            for (int j = 0; j < number; j++)
            {
                int index = Random.Range(0, Vars.GrassPath.Count);
                InstantiateObject(ref GrassPlatform, Vars.GrassPath[i]);
            }
        }
    }

    void NewCommonPlatform()
    {
        for (int i = 0; i < Vars.CommonPath.Count; i++)
        {
            for (int j = 0; j < number; j++)
            {
                int index = Random.Range(0, Vars.CommonPath.Count);
                InstantiateObject(ref CommonPlatform, Vars.CommonPath[i]);
            }
        }
    }

    public GameObject GetPlatform(ref List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].activeInHierarchy == false)
            {
                return list[i];
            }
        }
        int index = Random.Range(0, list.Count);
        return InstantiateObject(ref list, list[index]);//TODO: 有没有设置成Spike的物体导致移除监听失败
    }
}
