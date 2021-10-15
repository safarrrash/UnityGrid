using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBlob_Behavour : MonoBehaviour
{
    FriendlyDetector Detector;
    Rigidbody2D rb;
    CharacterMainScript mainScript;
    [SerializeField] float speed;
    [SerializeField] GameObject target;
    bool moving = true;
    float timer;
    float attSpeed;

    private void Start()
    {
        mainScript = GetComponent<CharacterMainScript>();
        rb = GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<CharacterMainScript>().Attributes.speed;
        Detector = transform.GetComponentInChildren<FriendlyDetector>();
        attSpeed = mainScript.Attributes.attSpeed;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DebugText.text = Detector.isDetected().ToString();
        if (Detector.isDetected()) moving = false;
        moveForward(moving);

        checkTarget();

        checkHealth();

        if(timer >= attSpeed)
        {
            if (target != null)
            {
                attack(target);
                timer = 0;
            }
        }
        

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

    void checkTarget()
    {
        FriendlyDetector detector = transform.GetComponentInChildren<FriendlyDetector>();
        bool isDetected = detector.isDetected();
        if (isDetected)
        {
            target = getTarget();
        }
    }

    void attack(GameObject target)
    {
        target.GetComponent<CharacterMainScript>().SelfDamage(GetComponent<CharacterMainScript>().SelfAttribute.damage);
    }

    GameObject getTarget()
    {
        return transform.GetComponentInChildren<FriendlyDetector>().getTarget();
    }

    void death()
    {
        Destroy(this, 0.5f);
        this.gameObject.SetActive(false);
        
    }

}
