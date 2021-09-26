using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavour_Pawn : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] float speed;
    bool moving = true;

    void Start()
    {
        speed = GetComponent<CharacterMainScript>().Attributes.speed;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        checkEnemy();
        checkFriendly();
        moveForward(moving);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        

    }

    void checkEnemy()
    {
        EnemyDetector detector = transform.GetChild(0).GetChild(1).GetComponent<EnemyDetector>();
        bool isDetected = detector.isInRange();
        if (isDetected)
        {
            
            moving = false;
        }
    }

    void checkFriendly()
    {
        FriendlyDetector detector = transform.GetChild(0).GetChild(0).GetComponent<FriendlyDetector>();
        bool isDetected = detector.isInRange();
        if (isDetected)
        {
            
            moving = false;
        }
    }

    void moveForward(bool isMoving)
    {
        if (isMoving)
        {
            rb.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }
}
