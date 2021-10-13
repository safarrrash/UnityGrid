using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyDetector : MonoBehaviour
{
    
    bool detectedF;
    int Range;
    Dictionary<int, float> getOffset = new Dictionary<int, float>()
    {
        {0, 1},
        {1, 1},
        {2, 1.5f},
        {3, 2f},
        {4, 2.5f},
        {5, 3},
        {6, 3.5f},
        {7, 4},
        {8, 4.5f},
        {9, 5},
        {10, 5.5f},
        {11, 6},
        {12, 6.5f},
        {13, 7f},
        {14, 7.5f},
        {15, 8f},
        {16, 8.5f},
        {17, 9f},
        {18, 9.5f},
        {19, 10f},
        {20, 10.5f},
    };
    GameObject target;
    List<GameObject> Targets = new List<GameObject>();


    private void Start()
    {
        Range = transform.parent.GetComponent<CharacterMainScript>().Attributes.range;
        if (Range == 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1, 0.5f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0); 
        }
        else if (Range > 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(Range, 0.5f);
            GetComponent<BoxCollider2D>().offset = new Vector2(getOffset[Range], 0);
        }

    }

    private void Update()
    {
        if (Targets.Count != 0)
        {
            detectedF = true;
            target = Targets[0];
        }

        else if (Targets.Count == 0)
        {
            detectedF = false;
            target = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Friendly")
        {
            if (collision.TryGetComponent<Transform>(out Transform colT))
                Targets.Add(colT.gameObject);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.TryGetComponent<Transform>(out Transform colT))
                if (Targets.Contains(collision.gameObject))
                    Targets.Remove(collision.gameObject);
                else
                    print(collision.transform + " is not in the list");
        }

    }

    public bool isDetected()
    {
        return detectedF;
    }

    public GameObject getTarget()
    {
        if (Targets.Count == 0) return null;
        return target;
    }

}
