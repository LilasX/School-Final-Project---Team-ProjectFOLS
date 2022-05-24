using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shooter { player, enemy }

public class BaseProjectile : MonoBehaviour
{
    public Shooter shooter;
    public RangeWeapon typeRange;

    private void OnCollisionEnter(Collision collision)
    {
        if(shooter == Shooter.player && collision.gameObject.GetComponent<EnemyMain>()) 
        {
            collision.gameObject.GetComponent<EnemyMain>().GetCurrentHP -= 20; 
        }

        if (shooter == Shooter.enemy)
        {
            switch (typeRange)
            {
                case RangeWeapon.Sphere:
                    if (collision.gameObject.GetComponent<PlayerMovements>())
                    {
                        //Direct Damage
                        //collision.gameObject.GetComponent<Player>().Hp -= 20;
                        //Indirect Damage - Player near Explosion from Sphere
                        //collision.gameObject.GetComponent<Player>().Hp -= 10;
                    }
                    Invoke("SetInactiveRange", 1f);
                    break;
                case RangeWeapon.Arrow:
                    if (collision.gameObject.GetComponent<PlayerMovements>())
                    {
                        //collision.gameObject.GetComponent<Player>().Hp -= 30;
                    }
                    Invoke("SetInactiveRange", 1f);
                    break;
                case RangeWeapon.Lance:
                    if (collision.gameObject.GetComponent<PlayerMovements>())
                    {
                        //collision.gameObject.GetComponent<Player>().Hp -= 40;
                    }
                    Invoke("SetInactiveRange", 1f);
                    break;
                case RangeWeapon.Wall:
                    if (collision.gameObject.GetComponent<PlayerMovements>())
                    {
                        //collision.gameObject.GetComponent<Player>().Hp -= 30;
                    }
                    break;
                case RangeWeapon.Floor:
                    if (collision.gameObject.GetComponent<PlayerMovements>())
                    {
                        //collision.gameObject.GetComponent<Player>().Hp -= 10;
                    }
                    break;
                case RangeWeapon.Wave:
                    if (collision.gameObject.GetComponent<PlayerMovements>())
                    {
                        //collision.gameObject.GetComponent<Player>().Hp -= 20;
                    }
                    break;
            }
        }

    }

    public void SetInactiveRange() 
    {
        gameObject.SetActive(false); 
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (typeRange) //Can I put them all together?
        {
            case RangeWeapon.Sphere:
            case RangeWeapon.Arrow:
            case RangeWeapon.Lance:
            case RangeWeapon.Wave:
                Invoke("SetInactiveRange", 4f);
                break;
            case RangeWeapon.Wall:
            case RangeWeapon.Floor:
                Invoke("SetInactiveRange", 8f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
