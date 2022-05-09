using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shooter { player, enemy }

public class BaseProjectile : MonoBehaviour
{

    public Shooter shooter;

    private void OnCollisionEnter(Collision collision)
    {
        if(shooter == Shooter.enemy && collision.gameObject.GetComponent<PlayerMovements>())
        {
            //collision.gameObject.GetComponent<Player>().Hp -= 10;
            Invoke("SetInactiveRange", 2f);
        }
        if(shooter == Shooter.player && collision.gameObject.GetComponent<EnemyMain>())
        {
            collision.gameObject.GetComponent<EnemyMain>().Hp -= 20;
        }
    }

    public void SetInactiveRange()
    {
        gameObject.SetActive(false);
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
