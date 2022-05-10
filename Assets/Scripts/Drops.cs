using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    private Transform posOrigin; //Transform to keep the starting transform of this drops. for Pooling System

    public void PickUpDrop() //Method to make Drop Disappear and return to his original position
    {
        gameObject.SetActive(false); //Set this gameObject to Inactive
        gameObject.transform.position = posOrigin.position; //Set this gameObject's position to the posOrigin's position. for Pooling System.
    }

    // Start is called before the first frame update
    void Start()
    {
        posOrigin = transform; //set posOrigin to this current transform. For Pooling System
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
