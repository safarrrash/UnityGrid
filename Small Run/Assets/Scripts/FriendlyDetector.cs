using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDetector : MonoBehaviour
{
    string hitTag;
    bool detectedF;
    int inRange = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Friendly")
        {
            inRange++;
            detectedF = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange--;
        if (inRange == 0) detectedF = false;
        
    }

    public bool isInRange()
    {
        return detectedF;
    }

    public string getTag()
    {
        return hitTag;
    }

}
