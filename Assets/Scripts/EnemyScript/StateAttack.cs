using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyState
{
    public StateEscape stateEscape;
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        enemyBehaviour.GetComponent<EnemyMain>().AttackPlayer();

        //----- ----- Condition To Go To Script StateEscape ----- -----
        if (enemyBehaviour.GetComponent<EnemyMain>().Hp <= 30) 
            return stateEscape;

        return this;
    }
}
