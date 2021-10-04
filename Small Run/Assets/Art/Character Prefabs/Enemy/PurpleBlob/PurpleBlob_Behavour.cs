using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBlob_Behavour : MonoBehaviour
{
    FriendlyDetector Detector;
    Rigidbody2D rb;

    [SerializeField] float speed;
    bool moving = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Detector = transform.GetChild(0).GetComponent<FriendlyDetector>();
        speed = gameObject.GetComponent<CharacterMainScript>().Attributes.speed;
        Detector = transform.GetComponentInChildren<FriendlyDetector>();
    }
    
    private void Update()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DebugText.text = Detector.isDetected().ToString();
        if (Detector.isDetected()) moving = false;
        moveForward(moving);

    }


    void moveForward(bool isMoving)
    {
        if (isMoving)
        {
            rb.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

}
