using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinMelee : StateStart
{
    public StateAttackMelee01 stateMelee01;
    public StateAttackMelee02 stateMelee02;
    public StateAttackRange01 stateRange01;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        stateMelee01.once1 = false; 
        stateMelee01.once2 = false; 
        stateMelee01.once3 = false;

        stateMelee02.once1 = false; 
        stateMelee02.once2 = false; 
        stateMelee02.once3 = false;
        
        stateRange01.once1 = false; 
        stateRange01.once2 = false;
        stateRange01.once3 = false;

        statePursue.once = false;

        stateKnocked.once1 = false;
        stateKnocked.once2 = false;

        stateDeath.once1 = false;
        stateDeath.once2 = false;
        stateDeath.once3 = false;

        if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
        {
            stateRange01.isPooling = true;
        } 
        else
        {
            stateRange01.isPooling = false;
        }

        enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();

        return stateWander;
    }
}
