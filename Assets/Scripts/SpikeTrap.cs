using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public bool trapTriggered = false;
    public float motionCountDown;
    public float delay;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        motionCountDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        motionCountDown -= Time.deltaTime;

        if (motionCountDown <= 0)
        {
            if (trapTriggered == false)
            {
                animator.SetBool("Down", false);
                animator.SetBool("Up", true);

                trapTriggered = true;
            }
            else if (trapTriggered == true)
            {
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);

                trapTriggered = false;
            }

            motionCountDown = delay;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            //Play knockback animation
            if(!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield)
            {
                other.gameObject.GetComponent<PlayerEntity>().playerState.ChangeState(other.gameObject.GetComponent<PlayerEntity>().KnockedState);
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 20;
            }
        }
    }
}
