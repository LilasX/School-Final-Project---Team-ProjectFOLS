using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Inventory inventoryScript;

    public Transform target;
    //public float minModifier;
    //public float maxModifier;

    public float distance = 5;
    public float timer;
    public GameManager manager;

    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        target = manager.player.transform;
        inventoryScript = manager.inventoryscript;
        timer = 0;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Triggering");
    //    if (other.gameObject == manager.player)
    //    {
    //        Debug.Log("Pickup");
    //        inventoryScript.CoinPickup(); //À Revoir.
    //                                      //PlaySound PickUp
    //                                      // Destroy(gameObject);
    //        gameObject.SetActive(false);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == manager.player)
        {
            Debug.Log("Pickup");
            inventoryScript.CoinPickup(); //À Revoir.
                                          //PlaySound PickUp
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 3f)
        {
            if (target.position.x - transform.position.x <= distance)
            {
                //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y + 1, target.position.z), ref velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
                transform.position = Vector3.MoveTowards(transform.position, manager.player.transform.position, 10 * Time.deltaTime);
            }
        }
        timer += Time.deltaTime;
        /*
        if (transform.position == target.position)
        {
            inventoryScript.CoinPickup();
            Destroy(this);
        }
        */
    }

}
