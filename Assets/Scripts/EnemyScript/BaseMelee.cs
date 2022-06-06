using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy } 

public class BaseMelee : MonoBehaviour
{
    private GameManager gameManager;
    public Striker striker;
    public bool canDmg = false;
    public GameObject otherObject;

    private void OnTriggerEnter(Collider other) 
    {
        if (striker == Striker.enemy && other.gameObject.GetComponent<PlayerEntity>() && canDmg) 
        {
            if(other.gameObject.GetComponent<PlayerEntity>().IsUsingShield)
            {
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP = other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP;
            }
            else
            {
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 10;
                canDmg = false;
            }
        }
        if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
        {

            if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
            {
                if (!gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                {
                    gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Add(other.gameObject);
                    other.gameObject.GetComponent<EnemyMain>().OnHurt(30);
                    gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
                }

                //if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                //{
                //    int currentHP;
                //    currentHP = other.gameObject.GetComponent<EnemyMain>().GetCurrentHP;
                //    other.gameObject.GetComponent<EnemyMain>().GetCurrentHP = currentHP;
                //    //gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(other.gameObject);
                //}
            }

            if (gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
            {
                if (!gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
                {
                    gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Add(other.gameObject);
                    other.gameObject.GetComponent<EnemyMain>().OnHurt(10);
                    gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash = false;
                }
            }


            //if (gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
            //{
            //    other.gameObject.GetComponent<EnemyMain>().OnHurt(10);
            //    //gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
            //}
        }
        if (other.gameObject.GetComponent<MockTest>() && canDmg)
        {
            other.gameObject.GetComponent<MockTest>().hp -= 5;
            canDmg = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        otherObject = other.gameObject;
        if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(otherObject))
            {
                int currentHP;
                currentHP = other.gameObject.GetComponent<EnemyMain>().GetCurrentHP;
                otherObject.GetComponent<EnemyMain>().GetCurrentHP = currentHP;
    
                //gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(other.gameObject);
            }
        }

        if (gameManager.player.GetComponent<PlayerEntity>().IsSlashing)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(other.gameObject))
            {
                int currentHP;
                currentHP = other.gameObject.GetComponent<EnemyMain>().GetCurrentHP;
                otherObject.GetComponent<EnemyMain>().GetCurrentHP = currentHP;
            }
        }

        if (otherObject.GetComponent<EnemyMain>().GetCurrentHP <= 0)
        {
            gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(otherObject);
        }

    }


    private void OnTriggerExit(Collider other)
    {

        if (!gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee || !gameManager.player.GetComponent<PlayerEntity>().hasRequestedSlash)
        {
            if (gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Contains(otherObject))
            {
                otherObject.GetComponent<EnemyMain>().GetCurrentHP = otherObject.GetComponent<EnemyMain>().GetCurrentHP;
                gameManager.player.GetComponent<PlayerEntity>().damagedEnemiesList.Remove(otherObject);
            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        canDmg = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
