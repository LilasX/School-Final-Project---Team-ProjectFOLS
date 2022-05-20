using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{

    public bool rightSide;
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
            if (rightSide == false)
            {
                animator.SetBool("RightSwing", false);
                animator.SetBool("LeftSwing", true);

                rightSide = true;
            }
            else if (rightSide == true)
            {
                animator.SetBool("LeftSwing", false);
                animator.SetBool("RightSwing", true);
              
                rightSide = false;
            }

            motionCountDown = delay;
        }
    }
}
