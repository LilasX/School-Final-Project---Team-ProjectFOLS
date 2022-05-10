using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shooter { player, enemy } //Enum For Testing Purpose and for Preventing Friendly Fire & Self-Harms

public class BaseProjectile : MonoBehaviour
{
    public Shooter shooter; //create enum Striker to keep enum of this gameObject, which is who is holding this gameObject.

    private void OnCollisionEnter(Collision collision) //On Collision Enter Method
    {
        if(shooter == Shooter.enemy && collision.gameObject.GetComponent<PlayerMovements>()) //if projectile is from an enemy and is colliding with a gameObject possessing the script PlayerMovements
        {
            //collision.gameObject.GetComponent<Player>().Hp -= 10; //Set the player's health the projectile is colliding to Minus 10
            Invoke("SetInactiveRange", 2f); //Call SetInactiveRange Method after 2f
        }
        if(shooter == Shooter.player && collision.gameObject.GetComponent<EnemyMain>()) //if projectile is from a player and is colliding with a gameObject possessing the script EnemyMain
        {
            collision.gameObject.GetComponent<EnemyMain>().Hp -= 20; //Set the enemy's health the projectile is colliding to Minus 20
        }
    }

    public void SetInactiveRange() //Method to Hide the Projectile in Scene
    {
        gameObject.SetActive(false); //Set this Projectile to inactive
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
