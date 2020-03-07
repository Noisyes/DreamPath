using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    private ManageVars Vars;

    private AudioSource CharacterAudio;
    private Vector2 MousePos;
    private bool IsLeft = false;
    private bool isJumping = false;
    private Vector3 NextLeftPos;
    private Vector3 NextRightPos;

    public Transform RayTransform;

    public LayerMask Mask;
    public LayerMask ObstacleMask;
    // Start is called before the first frame update
    private Rigidbody2D rgd;

    private GameObject LastPlatform = null;
    private void Awake()
    {
        Vars = ManageVars.GetManageVars();
        EventCenter.AddListener(EventDefine.Mute,Mute);
        EventCenter.AddListener(EventDefine.CharacterChangeSkin, CharacterChangeSkin);
        rgd = GetComponent<Rigidbody2D>();
        CharacterAudio = GetComponent<AudioSource>();
        Mute();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.CharacterChangeSkin, CharacterChangeSkin);
        EventCenter.RemoveListener(EventDefine.Mute,Mute);
    }
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Vars.SkinChoose.GetComponent<Image>().sprite;
    }
    void CharacterChangeSkin()
    {
        GetComponent<SpriteRenderer>().sprite = Vars.SkinChoose.GetComponent<Image>().sprite;
    }
    // Update is called once per frame
    void Update()
    {
        /*         Debug.DrawRay(transform.position - new Vector3(0f, 0.15f, 0f), Vector2.right * 0.5f);
                Debug.DrawRay(transform.position - new Vector3(0f, 0.15f, 0f), Vector2.left * 0.5f); */
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameCOntroller.Instance.isGameStart == false)
        {
            return;
        }
        if (GameCOntroller.Instance.isGameOver == true || GameCOntroller.Instance.isGamePause == true)
        {
            //Time.timeScale = 0;
            return;
        }
        if ((transform.position.y - Camera.main.transform.position.y) < -4 && GameCOntroller.Instance.isGameOver == false)
        {
            GameCOntroller.Instance.isGameOver = true;
            GameCOntroller.Instance.Weak.gameObject.SetActive(true);
            GameCOntroller.Instance.Weak.DOColor(new Color(120, 0, 0, 0), 1.5f).From().OnComplete(ShowGameOverPanel);
            gameObject.SetActive(false);
            return;
        }
        if (Input.GetMouseButtonDown(0) && isJumping == false&&NextRightPos!=Vector3.zero&&NextLeftPos!=Vector3.zero)
        {
            EventCenter.Broadcast(EventDefine.PathCreate);
            EventCenter.Broadcast<int>(EventDefine.SpikeContinue, 1);
            MousePos = Input.mousePosition;
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
        if (IsCastObstacle() == true)
        {
            CharacterAudio.PlayOneShot(Vars.HitClip);
            //Destroy(gameObject);
            GetComponent<SpriteRenderer>().enabled = false;
            GameCOntroller.Instance.Weak.gameObject.SetActive(true);
            GameCOntroller.Instance.Weak.DOColor(new Color(120, 0, 0, 0), 1.5f).From().OnComplete(ShowGameOverPanel);
            EventCenter.Broadcast(EventDefine.CameraFollow);
            GameCOntroller.Instance.isGameOver = true;
        }
        if (IsCastCollider() == false && GameCOntroller.Instance.isGameOver == false && rgd.velocity.y < 0)
        {
            CharacterAudio.PlayOneShot(Vars.FallClip);
            GameCOntroller.Instance.isGameOver = true;
            GameCOntroller.Instance.Weak.gameObject.SetActive(true);
            GameCOntroller.Instance.Weak.DOColor(new Color(120, 0, 0, 0), 1.5f).From().OnComplete(ShowGameOverPanel);
            GetComponent<SpriteRenderer>().sortingLayerName = "Path";
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    bool IsCastObstacle()
    {
        RaycastHit2D hitLeft;
        RaycastHit2D hitRight;
        hitLeft = Physics2D.Raycast(transform.position - new Vector3(0f, 0.15f, 0f), Vector2.left, 0.25f, ObstacleMask);
        hitRight = Physics2D.Raycast(transform.position - new Vector3(0f, 0.15f, 0f), Vector2.right, 0.25f, ObstacleMask);
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.tag == "Obstacle")
            {
                return true;
            }
        }
        if (hitRight.collider != null)
        {
            if (hitRight.collider.tag == "Obstacle")
            {
                return true;
            }
        }
        return false;
    }

    bool IsCastCollider()
    {
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(RayTransform.position, Vector2.down, 1.0f, Mask);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.tag == "Path")
            {
                return true;
            }
        }
        return false;
    }
    void Jump()
    {
        isJumping = true;
        CharacterAudio.PlayOneShot(Vars.JumpClip);
        if (IsLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOJump(NextLeftPos + Vector3.up * 0.4f, 0.5f + transform.position.y, 1, 0.15f);

        }
        else
        {
            transform.localScale = Vector3.one;
            transform.DOJump(NextRightPos + Vector3.up * 0.4f, 0.5f + transform.position.y, 1, 0.15f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Path")
        {
            /*             if(LastPlatform ==null)
                            LastPlatform = other.gameObject;
                        else
                        {
                            if(other.gameObject == LastPlatform)
                            {
                                return ;
                            }
                            else
                            {
                                LastPlatform = other.gameObject;
                            }
                        } */ //防止触发两次相同的平台，但是目前没有问题
            isJumping = false;
            NextLeftPos = other.transform.position + new Vector3(Vars.LeftDir.x, Vars.LeftDir.y, 0);
            NextRightPos = other.transform.position + new Vector3(Vars.RightDir.x, Vars.RightDir.y, 0);
            EventCenter.Broadcast(EventDefine.ScoreShow);
            //EventCenter.Broadcast<Transform>(EventDefine.Fall,other.transform);
        }
    }
    void ShowGameOverPanel()
    {
        EventCenter.Broadcast(EventDefine.ShowGameOverPanel);
    }

    void Mute()
    {
        if(GameCOntroller.Instance.isMusicOn)
        {
            CharacterAudio.volume = 1;
        }
        else
        {
            CharacterAudio.volume = 0;
        }
    }
}
