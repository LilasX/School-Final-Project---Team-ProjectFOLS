using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMovement : MonoBehaviour
{
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject dustObject;
    private ParticleSystem dust;

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

    public void DustMoveLeft()
    {
        if(isMoving)
        {
            dustObject.transform.position = leftFoot.transform.position;
            dust.Play();
            Debug.Log("Left");
        }
        
    }

    public void DustMoveRight()
    {
        if (isMoving)
        {
            dustObject.transform.position = rightFoot.transform.position;
            dust.Play();
            Debug.Log("Right");
        }
        
    }
}
