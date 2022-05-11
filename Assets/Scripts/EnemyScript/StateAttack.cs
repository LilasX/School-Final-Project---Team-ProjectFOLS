using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyState
{
    public StateEscape stateEscape; //To use Script StateEscape in this script
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.GetComponent<EnemyMain>().AttackPlayer(); //Call AttackPlayer Method from EnemyMain from enemyBehaviour

        //----- ----- Condition To Go To Script StateEscape ----- -----
        if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30) //If this gameObject Health is equal or inferior to 30
            return stateEscape; //return a stateEscape, so it goes to Script StateEscape

        return this; //return a stateAttack, so it stays in this Script
    }
}
