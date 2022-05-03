using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyMain
{

    public EnemyMelee(int hp, int hpMax, int dmg) : base(hp, hpMax, dmg)
    {
        base.HpMax = 100;
        base.Hp = HpMax;
        base.Dmg = 5;
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
