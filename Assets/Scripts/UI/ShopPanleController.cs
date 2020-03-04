using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShopPanleController : MonoBehaviour
{
    ManageVars Vars;
    private Transform ScrollRect;

    private GameObject Choose;

    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        //Init();
    }

}
