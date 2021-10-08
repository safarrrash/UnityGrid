using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBlob_Behavour : MonoBehaviour
{
    FriendlyDetector Detector;
    Rigidbody2D rb;
    CharacterMainScript mainScript;
    [SerializeField] float speed;
    [SerializeField] GameObject Target;
    bool moving = true;

    private void Start()
    {
        mainScript = GetComponent<CharacterMainScript>();
        rb = GetComponent<Rigidbody2D>();
        speed = gameObject.GetComponent<CharacterMainScript>().Attributes.speed;
        Detector = transform.GetComponentInChildren<FriendlyDetector>();
    }
    
    private void Update()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DebugText.text = Detector.isDetected().ToString();
        if (Detector.isDetected()) moving = false;
        moveForward(moving);

        checkHealth();

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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DebugText.text = mainScript.Attributes.health.ToString();
        if (mainScript.Attributes.health <= 0) death();
    }

    void death()
    {
        Destroy(this, 0.5f);
        this.gameObject.SetActive(false);
        
    }

}
