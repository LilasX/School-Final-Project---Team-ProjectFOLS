using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackStart_GoblinWarrior : StateAttack
{ 
    public StatePursue statePursue;
    public StateAttackMelee01 stateMeleeLight;
    public StateAttackMelee02 stateMeleeHeavy;
    public StateAttackRange02 stateRangeBoulder;
    public StateAttackMagic stateMagicQuake;
    public StateAttackMagic stateMagicWave;
    public float playerDistance;
    public int hp;
    public int hpMax;
    public bool once = false;
    public int randNum;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            hpMax = enemyBehaviour.gameObject.GetComponent<EnemyMain>().GetMaxHP;
            once = true;
        }

        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);
        hp = enemyBehaviour.gameObject.GetComponent<EnemyMain>().GetCurrentHP;

        if (hp <= 60 /*30% (hpMax *30 /100)*/) //At Low Health
        {
            if (playerDistance <= 3) //Close to Boss
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                        return stateMagicQuake;
                    case 1:
                    case 2:
                        return stateMeleeHeavy;
                    default:
                        return stateMeleeLight;
                }
            }
            else //Any Situations
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                        return stateMagicWave;
                    case 1:
                        return stateMeleeHeavy;
                    case 2:
                    case 3:
                        return stateRangeBoulder;
                    default:
                        return stateMeleeLight;
                }
            }
        } 
        else //At High Health
        {
            if (playerDistance <= 3) //Close to Boss
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                        return stateMeleeHeavy;
                    default:
                        return stateMeleeLight;
                }
            }
            else //Any Situations
            {
                randNum = Random.Range(0, 6);
                switch (randNum)
                {
                    case 0:
                        return stateMeleeHeavy;
                    case 1:
                    case 2:
                        return stateRangeBoulder;
                    default:
                        return stateMeleeLight;
                }
            }
        }

        //return this;
    }
}
