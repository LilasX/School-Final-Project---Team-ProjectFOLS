using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinShaman : StateStart
{
    public StateAttackMagic stateMagicFireBall;
    public StateAttackMagic stateMagicFireArrow;
    public StateAttackMagic stateMagicFireLance;
    public StateAttackMagic stateMagicFireBomb;
    public StateAttackMagic stateMagicFireFloor;
    public StateAttackMagic stateMagicFireWall;
    public StateAttackMagic stateMagicFireWave;
    public StateAttackMagic stateMagicFireMeteor;
    public StateAttackMagic stateMagicLightningField;
    public StateAttackMagic stateMagicLightningStrike;
    public StateAttackMagic stateMagicLightningWave;
    public StateAttackMagic stateMagicLightningStorm;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
        {
            stateMagicFireBall.once1 = false;
            stateMagicFireBall.once2 = false;
            stateMagicFireBall.once3 = false;
            stateMagicFireBall.once4 = true;

            stateMagicFireArrow.once1 = false;
            stateMagicFireArrow.once2 = false;
            stateMagicFireArrow.once3 = false;
            stateMagicFireArrow.once4 = true;

            stateMagicFireLance.once1 = false;
            stateMagicFireLance.once2 = false;
            stateMagicFireLance.once3 = false;
            stateMagicFireLance.once4 = true;

            stateMagicFireBomb.once1 = false;
            stateMagicFireBomb.once2 = false;
            stateMagicFireBomb.once3 = false;
            stateMagicFireBomb.once4 = true;

            stateMagicFireFloor.once1 = false;
            stateMagicFireFloor.once2 = false;
            stateMagicFireFloor.once3 = false;
            stateMagicFireFloor.once4 = true;

            stateMagicFireWall.once1 = false;
            stateMagicFireWall.once2 = false;
            stateMagicFireWall.once3 = false;
            stateMagicFireWall.once4 = true;

            stateMagicFireWave.once1 = false;
            stateMagicFireWave.once2 = false;
            stateMagicFireWave.once3 = false;
            stateMagicFireWave.once4 = true;

            stateMagicFireMeteor.once1 = false;
            stateMagicFireMeteor.once2 = false;
            stateMagicFireMeteor.once3 = false;
            stateMagicFireMeteor.once4 = true;

            stateMagicLightningField.once1 = false;
            stateMagicLightningField.once2 = false;
            stateMagicLightningField.once3 = false;
            stateMagicLightningField.once4 = true;

            stateMagicLightningStrike.once1 = false;
            stateMagicLightningStrike.once2 = false;
            stateMagicLightningStrike.once3 = false;
            stateMagicLightningStrike.once4 = true;

            stateMagicLightningWave.once1 = false;
            stateMagicLightningWave.once2 = false;
            stateMagicLightningWave.once3 = false;
            stateMagicLightningWave.once4 = true;

            stateMagicLightningStorm.once1 = false;
            stateMagicLightningStorm.once2 = false;
            stateMagicLightningStorm.once3 = false;
            stateMagicLightningStorm.once4 = true;

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
                stateMagicFireMeteor.isPooling = true;
                stateMagicLightningStorm.isPooling = true;
                onceP = true;
            }
            else
            {
                stateMagicFireMeteor.isPooling = false;
                stateMagicLightningStorm.isPooling = false;
                onceI = true;
            }

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
