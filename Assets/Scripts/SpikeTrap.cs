using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{

    public bool trapTriggered = false;
    public float motionCountDown;
    public float delay;

    public Animator animator;


    public AudioClip spike;
    public AudioSource audioSpike;

    // Start is called before the first frame update
    void Start()
    {
        motionCountDown = delay;
        audioSpike = GetComponent<AudioSource>();
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
                audioSpike.PlayOneShot(spike);
            }
            else if (trapTriggered == true)
            {
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);

                trapTriggered = false;
                motionCountDown = delay;
            }

            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            //Play knockback animation
            if(!other.gameObject.GetComponent<PlayerEntity>().IsUsingShield)
            {
                if (other.gameObject.GetComponent<PlayerEntity>().isInvincible)
                {
                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(0);
                }
                else
                {
                    other.gameObject.GetComponent<PlayerEntity>().playerState.ChangeState(other.gameObject.GetComponent<PlayerEntity>().KnockedState);
                    other.gameObject.GetComponent<PlayerEntity>().OnHurt(20);
                }
            }
        }
    }
}
