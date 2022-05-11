using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeWeapon { Sphere, Wall, Arrow } //Enum for the Different Type of Projectile

public class EnemyRange : EnemyMain
{
    public bool canAttack; //Bool to see if Enemy can Attack or Not
    public GameObject projectileSpawn; //GameObject to Keep Projectile Spawn (where the Projectile spawn from when shooting) in Script
    public GameObject range; //GameObject to Keep the projectile in script
    public RangeWeapon typeRange; //Enum to Keep the Current Projectile Type
    private float timer; //float which works as Timer

    public bool attack;//Testing Attack Purpose
    public override void InitializeEnemy() //Method to Initialize Enemy at Start()
    {
        posOrigin = transform; //Set posOrigin to the transform this gameObject is at the Start. For Pooling System.
        HpMax = 60; //Set the HpMax of this gameObject to 60
        Hp = HpMax; //Set the Hp of this gameObject to the HpMax of this gameObject, which is 60
        canAttack = true; //Set Bool canAttack to true, so enemy can attack
        timer = 0; //Set Float timer at 0
        //range.SetActive(false);
        typeRange = RangeWeapon.Arrow; //Set Enum typeRange at Arrow
    }

    public override void AttackPlayer() //Method to Attack Player
    {
        if (canAttack) //If Enemy canAttack
        {
            range.transform.position = projectileSpawn.transform.position; //Set projectile's position to projectileSpawn's position
            range.transform.rotation = projectileSpawn.transform.rotation; //Set projectile's rotation to projectileSpawn's rotation
            //range.SetActive(true);
            range.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); //Add Force to the projectile's RigidBody
            canAttack = false; //Set Bool canAttack to false, so that the Enemy doesn't attack again
        }
    }

    public override void IsAttacking() //Method to See if Enemy Can Attack or Not
    {
        if (!canAttack) //Is Enemy can't attack
        {
            timer += Time.deltaTime; //timer is increased
            if (timer >= 2f) //If timer is equal or superior to 2f;
            {
                //range.SetActive(false);
                canAttack = true; //Set Bool canAttack to True
                timer = 0; //Set timer to 0 for Restart
            }
        }
    }
    public override void VerifyDeath() //Method to Verify if Enemy (this GameObject is Dead or not)
    {
        if(Hp <= 0) //If this gameObject's Hp is equal or inferior to 0
        {
            gameObject.SetActive(false); //Set this gameObject to Inactive, thus will not show in Scene
            transform.position = posOrigin.position; //Set this gameObject's position to posOrigin's position. For Pooling System
            //GetComponent<EnemyBehaviour>().SetState(BehaviourState.none); //Set this gameObject's BehaviourState to None. In EnemyBehaviour
            DropItem(); //Call DropItem Method
        }
    }
    public override void DropItem() //Method to Drop Item Upon Death
    {
        drop.transform.position = transform.position; //Set the drop's position to this gameObject's position
    }

    public override void DisplayHealthBar() //Method to Display this gameObject's current Health
    {
        slider.value = Hp * 100 / HpMax; //Set slider's value to the result of the formula for this gameObject's Hp
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemy(); //Call InitializeEnemy Method
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking(); //Call IsAttacking Method
        DisplayHealthBar(); //Call DisplayHealthBar Method
        VerifyDeath(); //Call VerifyDeath Method

        if (attack) //Testing Attack Purpose
        {
            AttackPlayer(); //Call AttackPlayer Method
            attack = false; //Set Bool attack to false
        }
    }
}
