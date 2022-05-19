using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour, IDamageable
{
    private int currentHP;
    private int maxHP;
    private enum EntityType { Player, Enemy, NPC, Other }

    public int GetCurrentHP { get => currentHP; set => currentHP = value; }
    public int GetMaxHP { get => maxHP; set => maxHP = value; }


    public abstract void OnDeath();

    public virtual void OnHurt(int damage) { }
}
