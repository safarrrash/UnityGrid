using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDetector : MonoBehaviour
{
    string hitTag;
    bool detectedF;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Friendly")
        {
            detectedF = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedF = false;
        
    }

    public bool isDetected()
    {
        return detectedF;
    }

    public string getTag()
    {
        return hitTag;
    }

}
