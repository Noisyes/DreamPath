using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class DragController : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    // Start is called before the first frame update
    private GameObject Choose;

    private ManageVars Vars;

    private Transform MinDistenceToCenterItem;

    private List<GameObject> SkinChooseList = new List<GameObject>();

    

    private bool isBackToCenterPos = true;
    private int TOBackScrollIndex;
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        Init();
    }

    private void Update()
    {
        if (isBackToCenterPos)
        {
            Debug.Log(TOBackScrollIndex);
            Choose.transform.localPosition = Vector3.MoveTowards(Choose.transform.localPosition, new Vector3(-120 * TOBackScrollIndex - 240, 0, 0), Time.deltaTime * 300f);
        }
        foreach(var tmp in SkinChooseList)
        {
            if(tmp != SkinChooseList[TOBackScrollIndex])
            tmp.transform.localScale = Vector3.one;
        }
        if(Vector3.Distance(Choose.transform.localPosition, new Vector3(-120 * TOBackScrollIndex - 240, 0, 0))<0.01f&&isBackToCenterPos)
        {
            SkinChooseList[TOBackScrollIndex].transform.DOScale(Vector3.one * 1.3f,0.2f);
            isBackToCenterPos = false;
        }

    }

    void Init()
    {
        Choose = transform.Find("Choose").gameObject;
        for (int i = 0; i < Vars.CharacterSpriteList.Count; i++)
        {
            GameObject go = Instantiate(Vars.SkinChoose, Choose.transform);
            Choose.GetComponent<RectTransform>().sizeDelta = new Vector2(480 + i * 120, 260);
            go.transform.localPosition = new Vector3(240 + 120 * i, 0, 0);
            go.GetComponent<Image>().sprite = Vars.CharacterSpriteList[i];
            SkinChooseList.Add(go);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        MinDistenceToCenterItem = SkinChooseList[0]?.transform;
        TOBackScrollIndex = 0;
        isBackToCenterPos = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < SkinChooseList.Count; i++)
        {
            if (Vector3.Distance(-Choose.transform.localPosition, SkinChooseList[i].transform.localPosition) < Vector3.Distance(MinDistenceToCenterItem.localPosition, -Choose.transform.localPosition))
            {

                MinDistenceToCenterItem = SkinChooseList[i].transform;
                TOBackScrollIndex = i;
            }
            //Debug.Log("what happend" + -Choose.transform.localPosition);
        }

        isBackToCenterPos = true;
    }
}
