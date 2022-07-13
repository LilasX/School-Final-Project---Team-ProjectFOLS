using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinWarrior : StateStart
{
    public StateAttackMelee01 stateMeleeLight;
    public StateAttackMelee02 stateMeleeHeavy;
    public StateAttackMeleeCharge stateMeleeCharge;
    public StateAttackRange02 stateRangeThrow;
    public StateAttackRangeBoulderSpread stateRangeSpreadF;
    public StateAttackRangeBoulderSpread stateRangeSpreadW;
    public StateAttackMagic stateMagicWave;
    public StateAttackMagic stateMagicQuake;
    public StateAttackMagic stateMagicQuakeV2;
    public StateAttackMagic stateMagicFallRandom;
    public StateAttackMagic stateMagicFallPrecise;
    public StateAttackMagic stateMagicBlast;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            stateMeleeLight.once1 = false;
            stateMeleeLight.once2 = false;
            stateMeleeLight.once3 = false;

            stateMeleeHeavy.once1 = false;
            stateMeleeHeavy.once2 = false;
            stateMeleeHeavy.once3 = false;

            stateMeleeCharge.once1 = false;
            stateMeleeCharge.once2 = false;

            stateRangeThrow.once1 = false;
            stateRangeThrow.once2 = false;
            stateRangeThrow.once3 = false;
            stateRangeThrow.once4 = true;

            stateRangeSpreadF.once1 = false;
            stateRangeSpreadF.once2 = false;
            stateRangeSpreadF.once3 = false;
            stateRangeSpreadF.once4 = true;
            stateRangeSpreadF.iSPOnce1 = false;
            stateRangeSpreadF.iSPOnce2 = false;
            stateRangeSpreadF.iSIOnce1 = false;
            stateRangeSpreadF.iSIOnce2 = false;

            stateRangeSpreadW.once1 = false;
            stateRangeSpreadW.once2 = false;
            stateRangeSpreadW.once3 = false;
            stateRangeSpreadW.once4 = true;
            stateRangeSpreadW.iSPOnce1 = false;
            stateRangeSpreadW.iSPOnce2 = false;
            stateRangeSpreadW.iSIOnce1 = false;
            stateRangeSpreadW.iSIOnce2 = false;

            stateMagicWave.once1 = false;
            stateMagicWave.once2 = false;
            stateMagicWave.once3 = false;
            stateMagicWave.once4 = true;

            stateMagicQuake.once1 = false;
            stateMagicQuake.once2 = false;
            stateMagicQuake.once3 = false;
            stateMagicQuake.once4 = true;

            stateMagicQuakeV2.once1 = false;
            stateMagicQuakeV2.once2 = false;
            stateMagicQuakeV2.once3 = false;
            stateMagicQuakeV2.once4 = true;

            stateMagicFallRandom.once1 = false;
            stateMagicFallRandom.once2 = false;
            stateMagicFallRandom.once3 = false;
            stateMagicFallRandom.once4 = true;

            stateMagicFallPrecise.once1 = false;
            stateMagicFallPrecise.once2 = false;
            stateMagicFallPrecise.once3 = false;
            stateMagicFallPrecise.once4 = true;

            stateMagicBlast.once1 = false;
            stateMagicBlast.once2 = false;
            stateMagicBlast.once3 = false;
            stateMagicBlast.once4 = true;

            statePursue.once = false;

            stateWaiting.timer = 0;
            stateWaiting.once = false;
            stateWaiting.magicCoolDown = false;
            stateWaiting.start = false;

            stateKnocked.once1 = false;
            stateKnocked.once2 = false;

            stateDeath.once1 = false;
            stateDeath.once2 = false;
            stateDeath.once3 = false;

            if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
            {
                stateRangeThrow.isPooling = true;
                stateRangeSpreadF.isPooling = true;
                stateRangeSpreadW.isPooling = true;
                stateMagicWave.isPooling = true;
                stateMagicQuake.isPooling = true;
                stateMagicFallRandom.isPooling = true;
                stateMagicFallPrecise.isPooling = true;
                stateMagicBlast.isPooling = true;
                onceP = true;
            }
            else
            {
                stateRangeThrow.isPooling = false;
                stateRangeSpreadF.isPooling = false;
                stateRangeSpreadW.isPooling = false;
                stateMagicWave.isPooling = false;
                stateMagicQuake.isPooling = false;
                stateMagicFallRandom.isPooling = false;
                stateMagicFallPrecise.isPooling = false;
                stateMagicBlast.isPooling = false;
                onceI = true;
            }

            enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CannotDamage();
            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = false;
            character.GetComponent<Renderer>().material = dissolveMat;
            cutoffValue = 3;

            once = true;
        }

        if (onceI)
        {
            spawnVFX = Instantiate(spawnVFXInstant, enemyBehaviour.gameObject.transform.position, enemyBehaviour.gameObject.transform.rotation);
            spawnVFX.GetComponent<EnemySpawnScript>().StartVFX();
            onceI = false;
        }

        if (onceP)
        {
            spawnVFX = enemyBehaviour.poolingManager.callSpawnVFX();
            spawnVFX.SetActive(true);
            spawnVFX.transform.position = enemyBehaviour.gameObject.transform.position;
            spawnVFX.transform.rotation = enemyBehaviour.gameObject.transform.rotation;
            spawnVFX.GetComponent<EnemySpawnScript>().StartVFX();
            spawnVFX.GetComponent<EnemySpawnScript>().isPooling = true;
            onceP = false;
        }

        timer += Time.deltaTime;

        if (timer >= 6f)
        {
            once = false;
            timer = 0;
            character.GetComponent<Renderer>().material = defaultMat;
            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
            return stateWaiting;
        }
        else
        {
            timer += Time.deltaTime;
            cutoffValue -= Time.deltaTime;
            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValue);
            return this;
        }
    }
}
