using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shooter { player, enemy }

public class BaseProjectile : MonoBehaviour
{
    public Shooter shooter;
    public RangeWeapon typeRange;

    public bool canDmgSphere;
    public bool canDmgArrow;
    public bool canDmgLance;
    public bool canDmgWall;
    public bool canDmgFloor;
    public bool canDmgWave;
    public float timerWall;
    public float timerFloor;
    public float timerWave;


    private void OnTriggerEnter(Collider other)
    {
        if(shooter == Shooter.player && other.gameObject.GetComponent<EnemyMain>()) 
        {
            //other.gameObject.GetComponent<EnemyMain>().GetCurrentHP -= 20;
                other.gameObject.GetComponent<EnemyMain>().OnHurt(40); //FIRE BURST is instanciated twice
            
        }

        if (shooter == Shooter.enemy)
        {
            switch (typeRange)
            {
                case RangeWeapon.Sphere:
                    if (other.gameObject.GetComponent<MockTest>() && canDmgSphere) //ForTestingGrounds
                    {
                        other.gameObject.GetComponent<MockTest>().hp -= 20;
                        canDmgSphere = false;
                    }
                    if (other.gameObject.GetComponent<PlayerEntity>() && canDmgSphere)
                    {
                        //Direct Damage
                        //other.gameObject.GetComponent<Player>().Hp -= 20;
                        //Indirect Damage - Player near Explosion from Sphere
                        //other.gameObject.GetComponent<Player>().Hp -= 10;
                        canDmgSphere = false;
                    }
                    Invoke("SetInactiveRange", 1f);
                    break;
                case RangeWeapon.Arrow:
                    if (other.gameObject.GetComponent<MockTest>() && canDmgArrow) //ForTestingGrounds
                    {
                        other.gameObject.GetComponent<MockTest>().hp -= 20;
                        canDmgArrow = false;
                    }
                    if (other.gameObject.GetComponent<PlayerEntity>() && canDmgArrow)
                    {
                        //other.gameObject.GetComponent<Player>().Hp -= 30;
                        canDmgArrow = false;
                    }
                    Invoke("SetInactiveRange", 1f);
                    break;
                case RangeWeapon.Lance:
                    if (other.gameObject.GetComponent<MockTest>() && canDmgLance) //ForTestingGrounds
                    {
                        other.gameObject.GetComponent<MockTest>().hp -= 40;
                        canDmgLance = false;
                    }
                    if (other.gameObject.GetComponent<PlayerEntity>() && canDmgLance)
                    {
                        //other.gameObject.GetComponent<Player>().Hp -= 40;
                        canDmgLance = false;
                    }
                    Invoke("SetInactiveRange", 1f);
                    break;
                case RangeWeapon.Wall:
                    if (other.gameObject.GetComponent<MockTest>() && canDmgWall) //ForTestingGrounds
                    {
                        other.gameObject.GetComponent<MockTest>().hp -= 30;
                        canDmgWall = false;
                    }
                    if (other.gameObject.GetComponent<PlayerEntity>() && canDmgWall)
                    {
                        //other.gameObject.GetComponent<Player>().Hp -= 30;
                        canDmgWall = false;
                    }
                    break;
                case RangeWeapon.Floor:
                    if (other.gameObject.GetComponent<MockTest>() && canDmgFloor) //ForTestingGrounds
                    {
                        other.gameObject.GetComponent<MockTest>().hp -= 10;
                        canDmgFloor = false;
                    }
                    if (other.gameObject.GetComponent<PlayerEntity>() && canDmgFloor)
                    {
                        //other.gameObject.GetComponent<Player>().Hp -= 10;
                        canDmgFloor = false;
                    }
                    break;
                case RangeWeapon.Wave:
                    if (other.gameObject.GetComponent<MockTest>() && canDmgWave) //ForTestingGrounds
                    {
                        other.gameObject.GetComponent<MockTest>().hp -= 20;
                        canDmgWave = false;
                    }
                    if (other.gameObject.GetComponent<PlayerEntity>() && canDmgWave)
                    {
                        //other.gameObject.GetComponent<Player>().Hp -= 20;
                        canDmgWave = false;
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
        canDmgSphere = true;
        canDmgArrow = true;
        canDmgLance = true;
        canDmgWall = true;
        canDmgFloor = true;
        canDmgWave = true;
        timerWall = 0f;
        timerFloor = 0f;
        timerWave = 0f;

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
        if (!canDmgWall)
        {
            timerWall += Time.deltaTime;
            if (timerWall >= 1.2f)
            {
                canDmgWall = true;
                timerWall = 0f;
            }
        }

        if (!canDmgFloor)
        {
            timerFloor += Time.deltaTime;
            if (timerFloor >= 0.4f)
            {
                canDmgFloor = true;
                timerFloor = 0f;
            }
        }

        if (!canDmgWave)
        {
            timerWave += Time.deltaTime;
            if (timerWave >= 0.8f)
            {
                canDmgWave = true;
                timerWave = 0f;
            }
        }
    }
}
