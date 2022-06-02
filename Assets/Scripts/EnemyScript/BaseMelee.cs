using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Striker { player, enemy } 

public class BaseMelee : MonoBehaviour
{
    private GameManager gameManager;
    public Striker striker;
    public bool canDmg = false;


    private void OnTriggerEnter(Collider other) 
    {
        if (striker == Striker.enemy && other.gameObject.GetComponent<PlayerEntity>() && canDmg) 
        {
            other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 10;
            canDmg = false;
        }
        if (striker == Striker.player && other.gameObject.GetComponent<EnemyMain>())
        {
            if (gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee)
            {
                other.gameObject.GetComponent<EnemyMain>().GetCurrentHP -= 30;
                gameManager.player.GetComponent<PlayerEntity>().HasUsedMelee = false;
            }
        }
        if (other.gameObject.GetComponent<MockTest>() && canDmg)
        {
            other.gameObject.GetComponent<MockTest>().hp -= 5;
            canDmg = false;
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
