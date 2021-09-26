using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDetector : MonoBehaviour
{
    string hitTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Friendly")
        {
            if(collision.transform.position.x < this.gameObject.transform.position.x)
            hitTag = collision.tag;
        }
    }

    public string getTag()
    {
        return hitTag;
    }

}
