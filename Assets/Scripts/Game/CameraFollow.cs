using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform CurCharacter = null;
    private Vector3 Offset;

    private Vector2 Velocity = new Vector2(1.0f,1.0f);


    // Update is called once per frame
    private void Update()
    {
        if(CurCharacter==null && GameObject.FindGameObjectWithTag("Character")!=null)
            CurCharacter = GameObject.FindGameObjectWithTag("Character").transform;
        if(CurCharacter!=null)
        Offset = CurCharacter.position - transform.position;
    }
    private void LateUpdate()
    {
        float Posx = Mathf.SmoothDamp(transform.position.x,(transform.position + Offset).x,ref Velocity.x,0.1f);
        float Posy = Mathf.SmoothDamp(transform.position.y,(transform.position + Offset).y,ref Velocity.y,0.1f);
        if(transform.position.y<Posy)
        transform.position = new Vector3(Posx,Posy,transform.position.z);
    }
}
