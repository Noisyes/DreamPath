using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DiamondSelf : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Character")
        {
            EventCenter.Broadcast(EventDefine.DiamondScoreUp);
            Destroy(gameObject);
        }
    }
}
