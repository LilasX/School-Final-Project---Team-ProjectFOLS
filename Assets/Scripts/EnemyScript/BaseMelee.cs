using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy } //Enum For Testing Purpose and Preventing Friendly Fire or Self-Harms

public class BaseMelee : MonoBehaviour
{
    public Striker striker; //create enum Striker to keep enum of this gameObject, which is who is holding this gameObject.

    private void OnCollisionEnter(Collision collision) //On Collision Enter Method
    {
        if (striker == Striker.enemy && collision.gameObject.GetComponent<PlayerMovements>()) //If the melee is from an enemy and the collision it touched possess the script PlayerMovements;
        {
            //collision.gameObject.GetComponent<Player>().Hp -= 10; //Set the Hp of the collision by accessing its script to Minus 10;
        }
        if (striker == Striker.player && collision.gameObject.GetComponent<EnemyMain>()) //If the melee is from a player and the collision it touched possess the script EnemyMain;
        {
            collision.gameObject.GetComponent<EnemyMain>().Hp -= 20; //Set the Hp of enemy it is touching to Minus 20
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
