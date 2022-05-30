using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy } 

public class BaseMelee : MonoBehaviour
{
    private GameManager gameManager;
    public Striker striker; 


    private void OnTriggerEnter(Collider other) 
    {
        if (striker == Striker.enemy && other.gameObject.GetComponent<PlayerEntity>()) 
        {
            other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 10; 
        }
        if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
        {
            if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
            {
                other.gameObject.GetComponent<EnemyMain>().GetCurrentHP -= 30;
            }
            //Debug.Log("Touched");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
