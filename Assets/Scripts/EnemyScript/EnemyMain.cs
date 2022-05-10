using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMain : MonoBehaviour
{
    public int hp; //Current Health of Enemy
    public int hpMax; //Maximum Health of Enemy
    public int dmg; //Currently, Damage Dealt to Opponent. If ATK is introduced, will served as Result of Damage Calculation

    protected Transform posOrigin; //For Pooling Purpose. Keeping their first and original transform for when enemy died

    //Need ATK, DEF & SPD?

    public Slider slider; //Slider for Health Bar 
    public GameObject drop; //GameObject Drop to drop upon Death

    //Don't know if I need those
    public int Hp { get => hp; set => hp = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int Dmg { get => dmg; set => dmg = value; }

    public abstract void InitializeEnemy(); //Method for Initializing Enemy in Start();
    public abstract void AttackPlayer(); //Method for Attacking the Player. How to Attack Depend on Enemy Type
    public abstract void IsAttacking(); //Method to Determine if Enemy is Attacking or Not. To Prevent from Attacking Multiple Times in a Seconds.
    public abstract void VerifyDeath(); //Method to Verify if Enemy is Death in Update();
    public abstract void DropItem(); //Method to Drop an Item upon Death;
    public abstract void DisplayHealthBar(); //Method to Display the Current Health of Enemy in Slider
}
