using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeWeapon { Knife, Sword, Spear, Hammer }

public class EnemyMelee : EnemyMain
{
    public bool canAttack; 
    public GameObject[] melee = new GameObject[2];
    public Animator meleeAnim; 
    public MeleeWeapon typeMelee;
    private float timer;
    private int randNum;

    public bool attack; //Testing Attack Purpose in Inspector

    public override void InitializeEnemy() 
    {
        posOrigin = transform;
        HpMax = 100; 
        Hp = HpMax;
        canAttack = true; 
        timer = 0;
        //melee.SetActive(false);
        RandomWeapon();
        //typeMelee = MeleeWeapon.Sword; 
    }

    public override void RandomWeapon()
    {
        randNum = Random.Range(0, 2);
        switch (randNum)
        {
            case 0:
                melee[0].SetActive(true);
                melee[1].SetActive(false);
                break;
            default:
                melee[0].SetActive(false);
                melee[1].SetActive(true);
                break;
        }
    }

    public override void AttackPlayer()
    {
        if (canAttack) 
        {
            //melee.SetActive(true);
            //Need Confirmation for Anim
            //Need public Animator for 
            //if (meleeAnim) meleeAnim.SetTrigger("Attack"); //For Capsule

            //enemyBehaviour.enemyAnim.SetTrigger("IsAttacking");
            GetComponent<EnemyBehaviour>().enemyAnim.SetTrigger("IsAttacking"); //For Goblin
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
                //melee.SetActive(false);//Testing Attack
            }
        }
    }

    public override void VerifyDeath()
    {
        if (Hp <= 0)
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
