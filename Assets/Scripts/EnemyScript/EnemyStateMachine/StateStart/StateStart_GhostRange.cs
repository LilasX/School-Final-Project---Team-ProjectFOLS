using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GhostRange : StateStart
{
    public StateAttackMagic stateMagic01;
    public StateAttackMagic stateMagic02;
    public StateAttackMagic stateMagic03;
    public StateAttackMagic stateMagic04;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        stateMagic01.once1 = false;
        stateMagic01.once2 = false;
        stateMagic01.once3 = false;
        stateMagic01.once4 = true;

        stateMagic02.once1 = false;
        stateMagic02.once2 = false;
        stateMagic02.once3 = false;
        stateMagic02.once4 = true;

        stateMagic03.once1 = false;
        stateMagic03.once2 = false;
        stateMagic03.once3 = false;
        stateMagic03.once4 = true;

        stateMagic04.once1 = false;
        stateMagic04.once2 = false;
        stateMagic04.once3 = false;
        stateMagic04.once4 = true;

        statePursue.once = false;

        stateKnocked.once1 = false;
        stateKnocked.once2 = false;

        stateDeath.once1 = false;
        stateDeath.once2 = false;
        stateDeath.once3 = false;

        if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
        {
            stateMagic01.isPooling = true;
            stateMagic02.isPooling = true;
            stateMagic03.isPooling = true;
            stateMagic04.isPooling = true;
        }
        else
        {
            stateMagic01.isPooling = false;
            stateMagic02.isPooling = false;
            stateMagic03.isPooling = false;
            stateMagic04.isPooling = false;
        }

        return stateWander;
    }
}
