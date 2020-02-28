using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using DG.Tweening;

using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private ManageVars Vars;
    private Vector2 MousePos;
    private bool IsLeft = false;
    private bool isJumping = false;
    private Vector3 NextLeftPos;
    private Vector3 NextRightPos;
    // Start is called before the first frame update
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCOntroller.Instance.isGameStart == false||GameCOntroller.Instance.isGamepause == true)
            return;
        if (Input.GetMouseButtonDown(0) && isJumping == false)
        {
            EventCenter.Broadcast(EventDefine.PathCreate);
            EventCenter.Broadcast<int>(EventDefine.SpikeContinue,1);
            MousePos = Input.mousePosition;
            //Debug.Log(MousePos);
            if (MousePos.x <= Screen.width / 2)
            {
                IsLeft = true;

            }
            else
            {
                IsLeft = false;
            }
            Jump();
        }
    }
    void Jump()
    {
        isJumping = true;
        if (IsLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOJump(NextLeftPos + Vector3.up * 0.4f, 0.5f + transform.position.y, 1, 0.3f);

        }
        else
        {
            transform.localScale = Vector3.one;
            transform.DOJump(NextRightPos + Vector3.up * 0.4f, 0.5f + transform.position.y, 1, 0.3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Path")
        {
            isJumping = false;
            NextLeftPos = other.transform.position + new Vector3(Vars.LeftDir.x, Vars.LeftDir.y, 0);
            NextRightPos = other.transform.position + new Vector3(Vars.RightDir.x, Vars.RightDir.y, 0);
            //Debug.Log(NextRightPos);
        }
    }
}
