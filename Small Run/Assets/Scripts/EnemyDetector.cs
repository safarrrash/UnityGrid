using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    bool detectedE;
    int inRange = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            inRange++;
            detectedE = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange--;
        if (inRange == 0) detectedE = false;

    }

    public bool isInRange()
    {
        return detectedE;
    }
}
