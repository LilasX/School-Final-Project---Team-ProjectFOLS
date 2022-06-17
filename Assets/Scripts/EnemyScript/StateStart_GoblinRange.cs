using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinRange : StateStart
{
    public StateAttackRange02 stateRange01;
    public StateAttackRange02 stateRange02;
    public StateAttackRange02 stateRange03;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        stateRange01.once1 = false;
        stateRange01.once2 = false;
        stateRange01.once3 = false;
        stateRange01.once4 = true;

        stateRange02.once1 = false;
        stateRange02.once2 = false;
        stateRange02.once3 = false;
        stateRange02.once4 = true;

        stateRange03.once1 = false;
        stateRange03.once2 = false;
        stateRange03.once3 = false;
        stateRange03.once4 = true;

        statePursue.once = false;

        stateKnocked.once1 = false;
        stateKnocked.once2 = false;

        stateDeath.once1 = false;
        stateDeath.once2 = false;
        stateDeath.once3 = false;

        return stateWander;
    }
}
