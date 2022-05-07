using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeWeapon { Knife, Sword, Spear, Hammer }

public class EnemyMelee : EnemyMain
{
    public bool canAttack;
    public GameObject melee;
    public MeleeWeapon typeMelee;
    private float timer;

    public bool attack; //Testing Attack Purpose

    public override void InitializeEnemy()
    {
        posOrigin = transform;
        HpMax = 100;
        Hp = HpMax;
        canAttack = true;
        timer = 0;
        melee.SetActive(false);
        typeMelee = MeleeWeapon.Sword;
    }

    public override void AttackPlayer()
    {
        if (canAttack)
        {
            melee.SetActive(true);
            //Need Confirmation for Anim
            //Need public Animator for Anim.SetTrigger("Attack");
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
                canAttack = true;
                timer = 0;
                melee.SetActive(false);//Testing Attack
            }
        }
    }

    public override void VerifyDeath()
    {
        if (Hp <= 0)
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

    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking();

        if (attack) //Testing Attack Purpose
        {
            AttackPlayer();
            attack = false;
        }
    }

}
