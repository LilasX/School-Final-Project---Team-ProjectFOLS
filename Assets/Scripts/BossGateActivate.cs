using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateActivate : MonoBehaviour
{
    public GameObject boss_GoblinWarrior;
    public GameObject boss_GoblinShaman;
    public GameObject boss_Golem;

    public bool isWarrior;
    public bool isShaman;
    public bool isGolem;

    public bool once = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerEntity>())
        {
            if (!once)
            {
                once = true;
                ContactBoss();
            }
        }
    }

    public void ContactBoss()
    {
        if (isWarrior)
        {
            boss_GoblinWarrior.GetComponent<EnemyBehaviour>().StartBossAttack();
        }
        else if (isShaman)
        {
            boss_GoblinShaman.GetComponent<EnemyBehaviour>().StartBossAttack();
        }
        else if (isGolem)
        {
            boss_Golem.GetComponent<EnemyBehaviour>().StartBossAttack();
        }
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
