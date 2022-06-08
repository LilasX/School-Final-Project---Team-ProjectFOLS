using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeWeapon { Knife, Sword, Spear, Hammer }

public class EnemyMelee : EnemyMain
{
    public GameObject waveSpawnerObject;

    public bool canAttack; 
    public GameObject[] melee = new GameObject[2];
    public Animator meleeAnim;
    //public Animator animState;
    public MeleeWeapon typeMelee;
    private float timer;
    private int randNum;
    public GameObject coin;

    public bool attack; //Testing Attack Purpose in Inspector
    public bool die; //Testing OnDeath

    public override void InitializeEnemy() 
    {
        gameManager = GameManager.instance;
        posOrigin = transform;
        GetMaxHP = 100;
        GetCurrentHP = GetMaxHP;
        canAttack = true; 
        timer = 0;
        cameraMain = gameManager.cameraMain;
        RandomWeapon(); 
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

    public override void OnAttack()
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

            //if (animState.GetCurrentAnimatorStateInfo(0).IsName("MSword And Shield Slash"))
            //{
                Invoke("CanDamage", 0.1f);
            //}

        }
    }

    public void CanDamage()
    {
        melee[randNum].GetComponent<BaseMelee>().canDmg = true;
    }

    /*public override void AttackPlayer()
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
    }*/

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

    public override void OnDeath()
    {
        transform.position = posOrigin.position;
        CoinDrop();
        //DropItem();
        waveSpawnerObject.GetComponent<WaveSpawner>().EnemyCount(-1);
        //GetComponent<EnemyBehaviour>().enemyAnim.SetTrigger("isDead"); Use State
        gameObject.SetActive(false);
    }

    /*public override void VerifyDeath()
    {
        if (Hp <= 0)
        {
            transform.position = posOrigin.position;
            DropItem();
            gameObject.SetActive(false);
        }
    }*/

    public override void DropItem() 
    {
        drop.transform.position = transform.position; 
    }

    public void CoinDrop()
    {
        //int ranNum = Random.Range(0, 100);

        //if (ranNum <= 24)
        //{
            Instantiate(coin, new Vector3(this.transform.position.x,this.transform.position.y +0.5f, this.transform.position.z), this.transform.rotation);
        //}
    }

    public override void DisplayHealthBar() 
    {
        canvas.transform.LookAt(cameraMain.transform);
        slider.value = GetCurrentHP * 100 / GetMaxHP; 
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        InitializeEnemy();
        //waveSpawnerScript = FindObjectOfType<WaveSpawner>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        IsAttacking(); 
        DisplayHealthBar(); 
        //VerifyDeath(); 
        if (die)
        {
            OnDeath();
        }
        if (attack) 
        {
            //AttackPlayer(); 
            OnAttack();
            attack = false; 
        }
    }
}
