using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeWeapon { Sphere, Arrow, Lance, Wall, Floor, Wave }

public class EnemyRange : EnemyMain
{
    public bool canAttack;
    public GameObject projectileSpawn;
    public GameObject floorSpawn;
    public GameObject wallSpawn;
    public GameObject waveSpawn;
    public GameObject[] range = new GameObject[6];
    public RangeWeapon typeRange; 
    private float timer;
    public int randNum;

    public bool attack;//Testing Attack Purpose
    public override void InitializeEnemy() 
    {
        posOrigin = transform; 
        HpMax = 60; 
        Hp = HpMax; 
        canAttack = true; 
        timer = 0;
        HideRangedWeapon();
        RandomWeapon();
    }

    public void HideRangedWeapon()
    {
        for (int i = 0; i > range.Length; i++)
        {
            range[i].SetActive(false);
        }
    }

    public override void RandomWeapon()
    {
        randNum = Random.Range(0, 6);
        switch(randNum)
        {
            case 1:
                typeRange = RangeWeapon.Arrow;
                break;
            /*case 2:
                typeRange = RangeWeapon.Lance;
                break;*/
            case 3:
                typeRange = RangeWeapon.Wall;
                break;
            case 4:
                typeRange = RangeWeapon.Floor;
                break;
            case 5:
                typeRange = RangeWeapon.Wave;
                break;
            default:
                typeRange = RangeWeapon.Sphere;
                break;
        }
    }

    public override void AttackPlayer()
    {
        if (canAttack)
        {
            switch (typeRange)
            {
                case RangeWeapon.Sphere:
                    range[randNum].transform.position = projectileSpawn.transform.position;
                    range[randNum].transform.rotation = projectileSpawn.transform.rotation;
                    range[randNum].SetActive(true);
                    range[randNum].GetComponent<Rigidbody>().AddForce(transform.forward * 16, ForceMode.Impulse);
                    break;
                case RangeWeapon.Arrow:
                    range[randNum].transform.position = projectileSpawn.transform.position;
                    range[randNum].transform.rotation = projectileSpawn.transform.rotation;
                    range[randNum].SetActive(true);
                    range[randNum].GetComponent<Rigidbody>().AddForce(transform.forward * 24, ForceMode.Impulse);
                    break;
                /*case RangeWeapon.Lance:
                    range[randNum].transform.position = projectileSpawn.transform.position;
                    range[randNum].transform.rotation = projectileSpawn.transform.rotation;
                    range[randNum].SetActive(true);
                    range[randNum].GetComponent<Rigidbody>().AddForce(transform.forward * 12, ForceMode.Impulse);
                    break;*/
                case RangeWeapon.Wave:
                    range[randNum].transform.position = waveSpawn.transform.position;
                    range[randNum].transform.rotation = waveSpawn.transform.rotation;
                    range[randNum].SetActive(true);
                    range[randNum].GetComponent<Rigidbody>().AddForce(transform.forward * 12, ForceMode.Impulse);
                    break;
                case RangeWeapon.Wall:
                    range[randNum].transform.position = wallSpawn.transform.position;
                    range[randNum].transform.rotation = wallSpawn.transform.rotation;
                    range[randNum].SetActive(true);
                    break;
                case RangeWeapon.Floor:
                    range[randNum].transform.position = floorSpawn.transform.position;
                    range[randNum].transform.rotation = floorSpawn.transform.rotation;
                    range[randNum].SetActive(true);
                    break;
            }
            canAttack = false; 
        }
    }

    public override void IsAttacking() 
    {
        if (!canAttack) 
        {
            timer += Time.deltaTime; 
            switch (typeRange)
            {
                case RangeWeapon.Sphere:
                case RangeWeapon.Arrow:
                /*case RangeWeapon.Lance:*/
                case RangeWeapon.Wave:
                    if (timer >= 4f)
                    {
                        canAttack = true;
                        timer = 0;
                    }
                    break;
                case RangeWeapon.Wall:
                case RangeWeapon.Floor:
                    if (timer >= 8f)
                    {
                        canAttack = true;
                        timer = 0;
                    }
                    break;
            }
        }
    }
    public override void VerifyDeath() 
    {
        if(Hp <= 0) 
        {
            transform.position = posOrigin.position; 
            DropItem();
            gameObject.SetActive(false);
        }
    }
    public override void DropItem() 
    {
        drop.transform.position = transform.position;
    }

    public override void DisplayHealthBar() 
    {
        slider.value = Hp * 100 / HpMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking(); 
        DisplayHealthBar();
        VerifyDeath(); 

        if (attack)
        {
            AttackPlayer(); 
            attack = false; 
        }
    }
}
