using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ManageVarContainer")]
public class ManageVars : ScriptableObject
{
    // Start is called before the first frame update
    public static ManageVars GetManageVars()
    {
        return Resources.Load<ManageVars>("ManageVarsContainer");
    }
    public List<Sprite> sprites = new List<Sprite>();
    public Vector2 LeftDir = new Vector2(-0.6f, 0.65f);
    public Vector2 RightDir = new Vector2(0.6f, 0.65f);
    public GameObject PathPrefab;

    public Vector2 StartPos = new Vector2(0, -3f);

    public GameObject CharacterPrefabs;

    public Vector2 CharacterPos = new Vector2(0, -2.5f);
    public List<Sprite> PlatformTheme = new List<Sprite>();
    public List<GameObject> WinterPath = new List<GameObject>();
    public List<GameObject> GrassPath = new List<GameObject>();
    public List<GameObject> CommonPath = new List<GameObject>();

    public List<GameObject> SpikePath = new List<GameObject>();

}
