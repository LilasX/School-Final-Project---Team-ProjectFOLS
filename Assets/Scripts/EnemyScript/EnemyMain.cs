using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMain : MonoBehaviour
{
    private int hp; //Current Health of Enemy
    private int hpMax; //Maximum Health of Enemy
    private int dmg; //Currently, Damage Dealt to Opponent. If ATK is introduced, will served as Result of Damage Calculation

    //Need ATK, DEF & SPD?

    private Slider slider;

    public EnemyMain()
    {
        this.HpMax = 100;
        this.Hp = HpMax;
        this.Dmg = 5;
    }

    public EnemyMain(int hp, int hpMax, int dmg)
    {
        this.Hp = hp;
        this.HpMax = hpMax;
        this.Dmg = dmg;
    }

    public int Hp { get => hp; set => hp = value; }
    public int HpMax { get => hpMax; set => hpMax = value; }
    public int Dmg { get => dmg; set => dmg = value; }

    /*private void ReceiveDamage() //Damage Calculation Method Upon Receiving Damage
    {

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            this.Hp -= 5;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Stick"))
        {
            this.Hp -= 5;
        }

        if (this.Hp <= 0)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
