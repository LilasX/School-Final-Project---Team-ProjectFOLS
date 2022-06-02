using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    private Inventory hasKeys;

    // Start is called before the first frame update
    void Start()
    {
        hasKeys = FindObjectOfType<Inventory>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    
    {
        if (hasKeys.keys >= 1)
        {
            animator.SetBool("IsOpen", true);
            hasKeys.DoorOpened();
        }
        else return;
    }
}
