using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBlob_Behavour : MonoBehaviour
{
    Rigidbody2D rb;
    CharacterMainScript mainScript;
    [SerializeField] GameObject target;


    float speed;
    bool moving;
    bool canShoot = false;
    bool attacked = false;

    private void Start()
    {
        mainScript = GetComponent<CharacterMainScript>();
        checkAttributes();
        rb = mainScript.rigidBody;
    }
    
    private void Update()
    {
        checkTarget();
        checkMoving();
        checkShooting();
        moveForward(moving);

        checkTarget();

        checkHealth();

        moveForward(mainScript.CanMove());

        attack(canShoot, target);


    }

    void attack(bool isShot, GameObject target)
    {
        if (isShot)
        {
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


    void moveForward(bool isMoving)
    {
        if (isMoving)
        {
            rb.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

    void checkHealth()
    {
        
        if (mainScript.SelfAttribute.health <= 0) death();
    }

    void death()
    {
        Destroy(this, 0.5f);
        this.gameObject.SetActive(false);
        
    }

    void checkAttributes()
    {
        speed = mainScript.SelfAttribute.speed;
    }

}
