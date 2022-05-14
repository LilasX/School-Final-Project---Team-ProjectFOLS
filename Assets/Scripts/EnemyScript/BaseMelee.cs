using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy } 

public class BaseMelee : MonoBehaviour
{
    public Striker striker; 

    private void OnCollisionEnter(Collision collision) 
    {
        if (striker == Striker.enemy && collision.gameObject.GetComponent<PlayerMovements>()) 
        {
            //collision.gameObject.GetComponent<Player>().Hp -= 10; 
        }
        if (striker == Striker.player && collision.gameObject.GetComponent<EnemyMain>())
        {
            collision.gameObject.GetComponent<EnemyMain>().Hp -= 20;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
