using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : PhysicalEntity
{
    private int currentMana;
    private int maxMana;
    private int currentStamina;
    private int maxStamina;

    public int GetCurrentMana { get => currentMana; set => currentMana = value; }
    public int GetMaxMana { get => maxMana; set => maxMana = value; }
    public int GetCurrentStamina { get => currentStamina; set => currentStamina = value; }
    public int GetMaxStamina { get => maxStamina; set => maxStamina = value; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void OnDeath()
    {
        //Death animation maybe then Respawn to Hub, maybe get health, mana and stamina to full by default?
    }

    public override void OnHurt(int damage)
    {
        //Player taking damage
        GetCurrentHP -= damage;
        if (GetCurrentHP <= 0)
        {
            OnDeath();
        }
    }

    public override void OnHeal(int hp)
    {
        //Player healing their health points
        if (GetCurrentHP < GetMaxHP)
        {
            GetCurrentHP += hp;
        }
    }
    public override void OnAttack()
    {
        //Define the player's attacks
    }

    public override void OnBlock()
    {
        //Define how player blocks enemies' attacks (Shield?)
    }

    public override void OnChangeSpeed()
    {
        //Define the player's movement speed changes like walk, run, etc.
    }

    public override void Move()
    {
        //How the player character move
    }

    public void OnCharge(int mana)
    {
        //When player recovers his mana points for his skills
        if (GetCurrentMana < GetMaxMana)
        {
            GetCurrentMana += mana;
        }
    }
    public virtual void OnRecover(int stamina)
    {
        //When player recovers his stamina for using dodge, run, shield(can be in skills TBD), etc.
        if (GetCurrentStamina < GetMaxStamina)
        {
            GetCurrentStamina += stamina;
        }
    }

    //Not sure if it's the player that should manage the following methods below...
    public void OnInteract()
    {
        //How the player interacts with the NPC or objects in the hub?
    }

    public void OnBuy()
    {
        //Define how and what happens when player buys an item (potion for example from the shop)
    }

    public void OnSell()
    {
        //If the player can sell items, Define how and what happens when player sells an item (equipment if implementing the concept for example to the shop)
    }

}
