using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Behavour : MonoBehaviour
{
    
    Rigidbody2D rb;
    CharacterMainScript mainScript;

    GameObject target; 

    float speed;
    bool moving;
    bool canShoot = false;
    bool attacked = false;

    Animator anim;

    void Start()
    {
        mainScript = GetComponent<CharacterMainScript>();
        checkAttributes();
        
        anim = GetComponent<Animator>();
        rb = mainScript.rigidBody;
        anim.SetBool("Walk", true);
    }

    void Update()
    {
        checkAttributes();

        checkTarget();
        checkMoving();
        checkShooting();

        moveForward(mainScript.CanMove());

        shoot(canShoot, target);

    }

    

  
    void shoot(bool isShot, GameObject target)
    {
        if (isShot)
        {
            anim.SetBool("Shoot", true);
            anim.SetBool("Walk", false);
            if (target != null)
            {
                target.GetComponent<CharacterMainScript>().SelfDamage(mainScript.SelfAttribute.damage);
                attacked = true;
                if (!target.GetComponent<CharacterMainScript>().IsAlive())
                {
                    mainScript.targetManager.clearTarget();
                }
            }
        }

        else
        {
            anim.SetBool("Shoot", false);
            anim.SetBool("Walk", true);
        }
    }

    void checkTarget()
    {
        bool isDetected = mainScript.targetManager.haveTarget;
        if (isDetected)
        {
            target = mainScript.targetManager.getTarget();
        }
    }


    void checkAttributes()
    {
        speed = mainScript.SelfAttribute.speed;
    }

    void moveForward(bool isMoving)
    {
        if (isMoving)
        {
            rb.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }

    void checkMoving()
    {
        if (target != null)
        {
            moving = false;
        }

        else if (target == null)
        {
            moving = true;
        }

        mainScript.setCanMove(moving);

    }

    void checkShooting()
    {
        if (attacked)
        {
            mainScript.setCanAttack(false);
            attacked = false;
        }

        canShoot = mainScript.CanAttack();
    }


}
