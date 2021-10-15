using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public enum TargetType
    {
        Troop,
        Building,
        Any
    }

    [SerializeField] TargetType targetType;
    [SerializeField] string targetTag;

    bool isDetected = false;
    int range;
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

    CharacterMainScript mainScript;

    [SerializeField]GameObject target;
    List<GameObject> Targets = new List<GameObject>();

    private void Start()
    {
        mainScript = transform.GetComponentInParent<CharacterMainScript>();

        range = mainScript.SelfAttribute.range;

        if (range == 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1, 0.5f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        }

        else if (range > 0)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(range, 0.5f);
            GetComponent<BoxCollider2D>().offset = new Vector2(getOffset[range], 0);
        }
    }

    private void Update()
    {
        if (Targets.Count != 0)
        {
            isDetected = true;
            target = Targets[0];
        }

        else if (Targets.Count == 0)
        {
            Targets.Clear();
            isDetected = false;
            target = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        checkforTarget(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checkforTargetLeave(collision);
    }

    void checkforTarget(Collider2D col)
    {
        if(col.tag == targetTag && isTargetTypeMatch(targetType, col.GetComponent<CharacterMainScript>().SelfAttribute.charType))
        {
            if (col.TryGetComponent<Transform>(out Transform colT))
                Targets.Add(colT.gameObject);
        }
    }

    void checkforTargetLeave(Collider2D _target)
    {
        if (Targets.Contains(_target.gameObject))
        {
            Targets.Remove(_target.gameObject);
        }
    }

    bool isTargetTypeMatch(TargetType targetType, CharacterMainScript.CharacterType characterType)
    {
        if (targetType == TargetType.Any && characterType != CharacterMainScript.CharacterType.Spell) return true;
        else if (targetType == TargetType.Troop && characterType == CharacterMainScript.CharacterType.Troop) return true;
        else if (targetType == TargetType.Building && characterType == CharacterMainScript.CharacterType.Building) return true;
        else return false;
    }

    public bool isThereTarget()
    {
        if (target != null) return true;
        else if (target == null) return false;
        else return false;
    }

    public GameObject GetTarget()
    {
        if (target == null) Debug.LogWarning("Target is Returned as a null");
        return target;
    }

    public bool IsDetected()
    {
        return isDetected;
    }
}
