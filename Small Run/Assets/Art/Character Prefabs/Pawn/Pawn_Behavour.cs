using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Behavour : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float shootSpeed;

    float timer;
    bool moving = true;

    bool isShoot = false;

    Animator anim;
    void Start()
    {
        speed = GetComponent<CharacterMainScript>().Attributes.speed;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("Walk", true);
    }

    void Update()
    {
        timer += Time.deltaTime;

        checkEnemy();
        
        moveForward(moving);

        isShoot = false;
        if (timer >= shootSpeed)
        {
            if(!moving)
            isShoot = true;
            timer = 0;
        }

        shoot(isShoot);

    }

    

  
    void shoot(bool isShot)
    {
        if (isShot)
        {
            anim.SetBool("Shoot", true);
            anim.SetBool("Walk", false);
            
        }
        else
        {
            anim.SetBool("Shoot", false);
            anim.SetBool("Walk", true);
        }
    }

    void checkEnemy()
    {
        EnemyDetector detector = transform.GetComponentInChildren<EnemyDetector>();
        bool isDetected = detector.isDetected();
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