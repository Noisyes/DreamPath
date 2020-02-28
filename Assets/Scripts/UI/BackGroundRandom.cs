using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRandom : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int index = Random.Range(0,ManageVars.GetManageVars().sprites.Count);
        spriteRenderer.sprite =  ManageVars.GetManageVars().sprites[index];
    }

    // Update is called once per frame
}
