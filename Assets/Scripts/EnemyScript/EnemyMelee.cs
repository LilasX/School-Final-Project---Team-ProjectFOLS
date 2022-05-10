using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeWeapon { Knife, Sword, Spear, Hammer } //Still for Testing, but Enum for Attack Type of Enemy (Weapon Only)

public class EnemyMelee : EnemyMain
{
    public bool canAttack; //Bool to See if Enemy Can Attack
    public GameObject melee; //GameObject to Keep Melee in Script
    public Animator meleeAnim; //Animator to Keep Animation of Melee in Script
    public MeleeWeapon typeMelee; //Enum MeleeWeapon to keep current type of Melee in Script; Still for Testing
    private float timer; //float to be used as Timer


    public bool attack; //Testing Attack Purpose in Inspector

    public override void InitializeEnemy() //Method to Initialize Enemy in Start();
    {
        posOrigin = transform; //Keep Starting Transform in posOrigin. For Pooling System
        HpMax = 100; //Setting HpMax at 100;
        Hp = HpMax; //Setting Hp at HpMax, which is 100;
        canAttack = true; //Setting Bool CanAttack to True;
        timer = 0; //Setting Float Timer to 0;
        //melee.SetActive(false);
        typeMelee = MeleeWeapon.Sword; //Setting Enum typeMelee to Sword
    }

    public override void AttackPlayer() //Method to Attack Player. 
    {
        if (canAttack) //If Enemy can attack
        {
            //melee.SetActive(true);
            //Need Confirmation for Anim
            //Need public Animator for 
            if (meleeAnim) meleeAnim.SetTrigger("Attack"); //if Animator is present, play it by setting the Trigger of the Animation.
            canAttack = false; //Set bool canAttack at False. To Prevent Attacking Multiple Times
        }
    }

    public override void IsAttacking() //Method to see if Enemy can Attack
    {
        if (!canAttack) //If Enemy can't attack due to already having attacked
        {
            timer += Time.deltaTime; //Add float value to float timer
            if (timer >= 2f) //if timer is equal or superior to 2f
            {
                canAttack = true; //Set bool canAttack to True so Enemy can attack again
                timer = 0; //Return the Timer to 0 to restart
                //melee.SetActive(false);//Testing Attack
            }
        }
    }

    public override void VerifyDeath() //Method to see if Enemy is Dead
    {
        if (Hp <= 0) //If Hp is equal or inferior to 0
        {
            gameObject.SetActive(false); //this gameObject is set to inactive, so it stop showing itself
            transform.position = posOrigin.position; //return the position of this gameObject to the position of posOrigin. For Pooling System
            GetComponent<EnemyBehaviour>().SetState(BehaviourState.none); //Set Behaviour State to None. For System in EnemyBehaviour
            DropItem(); //Call DropItem Method
        }
    }

    public override void DropItem() //Method to Drop Item upon Death
    {
        drop.transform.position = transform.position; //set the position of the drop to this gameObject's position
    }

    public override void DisplayHealthBar() //Method to Display the Health Bar
    {
        slider.value = Hp * 100 / HpMax; //set the slider's value to the result of the formula for the Health of this gameObject Enemy
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemy(); //Call InitializeEnemy Method
    }

    // Update is called once per frame
    void Update()
    {
        IsAttacking(); //Call IsAttacking Method.Need to see if there is an effective way than this
        DisplayHealthBar(); //Call DisplayHealthBar Method.
        VerifyDeath(); //Call VerifyDeath Method

        if (attack) //Testing Attack Purpose
        {
            AttackPlayer(); //Call AttackPlayer Method
            attack = false; //Set bool attack to false
        }
    }
}
