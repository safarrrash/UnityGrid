using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Behavour : MonoBehaviour
{
    
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] float shootSpeed;
    [SerializeField] GameObject target;
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

        checkTarget();
        
        if(target != null)
        {
            moving = false;
        }
        else if (target == null)
        {
            moving = true;
        }

        moveForward(moving);

        isShoot = false;
        if (timer >= shootSpeed)
        {
            if(!moving)
            isShoot = true;
            timer = 0;
        }

        shoot(isShoot, target);

    }

    

  
    void shoot(bool isShot, GameObject target)
    {
        if (isShot)
        {
            anim.SetBool("Shoot", true);
            anim.SetBool("Walk", false);

            CharacterMainScript targetMain = target.GetComponent<CharacterMainScript>();
            targetMain.Attributes.health -= GetComponent<CharacterMainScript>().Attributes.damage;
        }
        else
        {
            anim.SetBool("Shoot", false);
            anim.SetBool("Walk", true);
        }
    }

    void checkTarget()
    {
        EnemyDetector detector = transform.GetComponentInChildren<EnemyDetector>();
        bool isDetected = detector.isDetected();
        if (isDetected)
        {
            target = getTarget();
        }
    }

    

    void moveForward(bool isMoving)
    {
        if (isMoving)
        {
            rb.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }

    public GameObject getTarget()
    {
        return transform.GetComponentInChildren<EnemyDetector>().getTarget();
    }
}
