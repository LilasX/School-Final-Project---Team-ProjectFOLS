using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMain : MonoBehaviour
{
    public int hp; 
    public int hpMax; 
    public int dmg;

    protected Transform posOrigin; 

    //Need ATK, DEF & SPD?

    public Slider slider; 
    public GameObject drop; 

    //Don't know if I need those
    public int Hp { get => hp; set => hp = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int Dmg { get => dmg; set => dmg = value; }

    public abstract void InitializeEnemy();
    public abstract void RandomWeapon();
    public abstract void AttackPlayer();
    public abstract void IsAttacking(); 
    public abstract void VerifyDeath(); 
    public abstract void DropItem(); 
    public abstract void DisplayHealthBar();
}
