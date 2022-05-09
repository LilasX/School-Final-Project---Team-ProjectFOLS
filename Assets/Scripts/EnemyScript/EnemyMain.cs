using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMain : MonoBehaviour
{
    public int hp; //Current Health of Enemy
    public int hpMax; //Maximum Health of Enemy
    public int dmg; //Currently, Damage Dealt to Opponent. If ATK is introduced, will served as Result of Damage Calculation

    protected Transform posOrigin;

    //Need ATK, DEF & SPD?

    public Slider slider;
    public GameObject drop;

    public int Hp { get => hp; set => hp = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int Dmg { get => dmg; set => dmg = value; }

    public abstract void InitializeEnemy();
    public abstract void AttackPlayer();
    public abstract void IsAttacking();
    public abstract void VerifyDeath();
    public abstract void DropItem();
    public abstract void DisplayHealthBar();
}
