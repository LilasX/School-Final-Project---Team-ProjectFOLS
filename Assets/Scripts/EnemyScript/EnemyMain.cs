using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMain : EnemyEntity
{
    //There is currentHP and MaxHP in BaseEntity
    //public int hp;
    //public int GetCurrentHP;
    //public int hpMax; 
    //public int dmg;

    protected Transform posOrigin;

    //Need ATK, DEF & SPD?
    public GameManager gameManager;
    public Animator animState;
    public Canvas canvas;
    public Slider slider;
    public GameObject cameraMain;
    public GameObject drop; 

    //Don't know if I need those
    /*public int Hp { get => hp; set => hp = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int Dmg { get => dmg; set => dmg = value; }*/

    public abstract void InitializeEnemy();
    public abstract void RandomWeapon();
    //public abstract void AttackPlayer();
    //public abstract void OnAttack();
    public abstract void IsAttacking();
    //public abstract void VerifyDeath(); 
    //public abstract void OnDeath();
    public abstract void DropItem(); //Not Needed since Coin Existed
    //public abstract void DropCoin();
    public abstract void DisplayHealthBar();

    public override void OnHurt(int damage)
    {
        GetCurrentHP -= damage;

        if(GetCurrentHP <= 0)
        {
            OnDeath();
        }
        else
        {
            //animState.SetTrigger("isKnocked"); //Done Multiple Times. Why?
            //GetComponent<EnemyBehaviour>().enemyAnim.SetTrigger("isKnocked");
        }
    }
}
