using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    Inventory inventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(180 * Time.deltaTime, 0, 0);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<PlayerEntity>())
    //    {
    //        inventoryScript.CoinPickup();
    //        Destroy(this);
    //    }
    //}
}
