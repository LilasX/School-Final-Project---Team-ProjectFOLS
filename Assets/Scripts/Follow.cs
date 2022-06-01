using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Inventory inventoryScript;

    public Transform target;
    public float minModifier;
    public float maxModifier;

    public float distance = 5;

    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (target.position.x - transform.position.x <= distance )
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
        } 

        if (transform.position == target.position)
        {
            inventoryScript.CoinPickup();
            Destroy(this);
        }
    
    }

}
