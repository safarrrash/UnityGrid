using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Behavour : MonoBehaviour
{
    
    Rigidbody2D rb;

    //[SerializeField] float speed;
    [SerializeField] GameObject target;
    [SerializeField] GameObject Healthbar;

    float health, speed, damage, cooldown, attSpeed;
    int range;

    CharacterMainScript mainScript;

    float timer;
    bool moving = true;

    bool isShoot = false;

    Animator anim;
    void Start()
    {
        mainScript = GetComponent<CharacterMainScript>();
        checkAttributes();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("Walk", true);
    }

    void Update()
    {
        checkAttributes();


        timer += Time.deltaTime;

        setHealthBar(health);

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
        if (timer >= attSpeed)
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
            targetMain.SelfAttribute.health -= mainScript.SelfAttribute.damage;
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

    void setHealthBar(float amount)
    {
        if (Healthbar.transform.localScale.x >= 0)
        Healthbar.transform.localScale = new Vector3(amount / (GetComponent<CharacterMainScript>().Attributes.health), Healthbar.transform.localScale.y);
    }

    void checkAttributes()
    {
        health = mainScript.SelfAttribute.health;
        damage = mainScript.SelfAttribute.damage;
        speed = mainScript.SelfAttribute.speed;
        attSpeed = mainScript.SelfAttribute.AttSpeed;
        range = mainScript.SelfAttribute.range;
       
    }

    void moveForward(bool isMoving)
    {
        if (isMoving)
        {
            rb.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }

    public void SelfDamage(float amount)
    {
        health-=amount;
    }
    
    public GameObject getTarget()
    {
        return transform.GetComponentInChildren<EnemyDetector>().getTarget();
    }
}
