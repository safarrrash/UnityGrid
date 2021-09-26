using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAnimation : MonoBehaviour
{

    enum State {idle, shootingStand, shoot, dead}

    private Animator animator;

    private State state;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        state = State.idle;
    }

    private void Update()
    {
        
        switch (state)
        {
            case State.idle:
                animator.SetBool("Idle", true);
                break;
            case State.shootingStand:
                animator.SetBool("Idle", false);
                break;
            case State.shoot:
                animator.SetTrigger("Shoot"); 
                break;
            case State.dead:
                animator.SetTrigger("Died");
                break;                   
        }
        if (state == State.shoot) state = State.shootingStand;
    }



    //UI
    public void SetState(string order)
    {
        switch (order)
        {
            case "idle":
                state = State.idle;
                break;
            case "shooting":
                state = State.shootingStand;
                break;
            case "shoot":
                state = State.shoot;
                break;
            case "died":
                state = State.dead;
                break;


        }
    }

}
