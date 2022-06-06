using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEffects : MonoBehaviour
{
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject dustObject;
    private ParticleSystem dust;

    public AudioClip footstepLeft;
    public AudioClip footstepRight;
    public AudioSource audioMove;

    public AudioClip swordMelee;
    public AudioSource audioSword;

    private Vector3 old_pos;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        dust = dustObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(old_pos == transform.position)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        old_pos = transform.position;
    }

    public void MoveLeft()
    {
        if(isMoving)
        {
            dustObject.transform.position = leftFoot.transform.position;
            dust.Play();
            audioMove.PlayOneShot(footstepLeft);
            //Debug.Log("Left");
        }
        
    }

    public void MoveRight()
    {
        if (isMoving)
        {
            dustObject.transform.position = rightFoot.transform.position;
            dust.Play();
            audioMove.PlayOneShot(footstepRight);
            //Debug.Log("Right");
        }
        
    }

    public void SwordMelee()
    {
        audioSword.PlayOneShot(swordMelee);
    }
}
