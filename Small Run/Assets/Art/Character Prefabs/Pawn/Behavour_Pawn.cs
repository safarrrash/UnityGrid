using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavour_Pawn : MonoBehaviour
{
    [SerializeField] Collider2D EnemyCol, FriendlyCol;
    Rigidbody2D rb;

    [SerializeField] float speed = 1f;
    bool moving = true;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveForward(moving);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            print(collision.gameObject.tag);
            moving = false;
        }

        if(gameObject.transform.GetChild(0).GetComponent<FriendlyDetector>().getTag() == "Friendly")
        {
            print(collision.gameObject.tag);
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
