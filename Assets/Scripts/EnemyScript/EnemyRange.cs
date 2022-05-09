using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeWeapon { Sphere, Wall, Arrow }

public class EnemyRange : EnemyMain
{
    public bool canAttack;
    public GameObject projectileSpawn;
    public GameObject range;
    public RangeWeapon typeRange;
    private float timer;

    public bool attack;//Testing Attack Purpose
    public override void InitializeEnemy()
    {
        posOrigin = transform;
        HpMax = 60;
        Hp = HpMax;
        canAttack = true;
        timer = 0;
        //range.SetActive(false);
        typeRange = RangeWeapon.Arrow;
    }

    public override void AttackPlayer()
    {
        
        if (canAttack)
        {
            range.transform.position = projectileSpawn.transform.position;
            range.transform.rotation = projectileSpawn.transform.rotation;
            //range.SetActive(true);
            range.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
            canAttack = false;
        }
    }

    public override void IsAttacking()
    {
        if (!canAttack)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                //range.SetActive(false);
                canAttack = true;
                timer = 0;
            }
        }
    }
    public override void VerifyDeath()
    {
        if(Hp <= 0)
        {
            gameObject.SetActive(false);
            transform.position = posOrigin.position;
            GetComponent<EnemyBehaviour>().SetState(BehaviourState.none);
            DropItem();
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
        if (attack) //Testing Attack Purpose
        {
            AttackPlayer();
            attack = false;
        }
    }
}
