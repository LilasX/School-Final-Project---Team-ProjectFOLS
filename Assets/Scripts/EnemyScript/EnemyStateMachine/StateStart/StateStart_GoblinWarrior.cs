using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinWarrior : StateStart
{
    public StateAttackMelee01 stateMeleeLight;
    public StateAttackMelee02 stateMeleeHeavy;
    public StateAttackRange02 stateRangeThrow;
    public StateAttackMagic stateMagicWave;
    public StateAttackMagic stateMagicQuake;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        stateMeleeLight.once1 = false;
        stateMeleeLight.once2 = false;
        stateMeleeLight.once3 = false;

        stateMeleeHeavy.once1 = false;
        stateMeleeHeavy.once2 = false;
        stateMeleeHeavy.once3 = false;

        stateRangeThrow.once1 = false;
        stateRangeThrow.once2 = false;
        stateRangeThrow.once3 = false;
        stateRangeThrow.once4 = true;

        stateMagicWave.once1 = false;
        stateMagicWave.once2 = false;
        stateMagicWave.once3 = false;
        stateMagicWave.once4 = true;

        stateMagicQuake.once1 = false;
        stateMagicQuake.once2 = false;
        stateMagicQuake.once3 = false;
        stateMagicQuake.once4 = true;

        statePursue.once = false;

        stateKnocked.once1 = false;
        stateKnocked.once2 = false;

        stateDeath.once1 = false;
        stateDeath.once2 = false;
        stateDeath.once3 = false;

        enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CannotDamage();

        return stateWaiting;
    }
}
