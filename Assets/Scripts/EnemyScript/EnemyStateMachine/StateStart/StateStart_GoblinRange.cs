using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart_GoblinRange : StateStart
{
    public StateAttackRange02 stateRange01;
    public StateAttackRange02 stateRange02;
    public StateAttackRange02 stateRange03;
    //public StateAttackRange02 stateRange04;
    //public StateAttackRange02 stateRange05;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once)
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
            /*
            stateRange04.once1 = false;
            stateRange04.once2 = false;
            stateRange04.once3 = false;
            stateRange04.once4 = true;

            stateRange05.once1 = false;
            stateRange05.once2 = false;
            stateRange05.once3 = false;
            stateRange05.once4 = true;
            */
            statePursue.once = false;

            stateKnocked.once1 = false;
            stateKnocked.once2 = false;

            stateDeath.once1 = false;
            stateDeath.once2 = false;
            stateDeath.once3 = false;

            if (enemyBehaviour.GetComponent<EnemyMain>().isPooling)
            {
                stateRange01.isPooling = true;
                stateRange02.isPooling = true;
                stateRange03.isPooling = true;
                //stateRange04.isPooling = true;
                //stateRange05.isPooling = true;
                onceI = true;
            }
            else
            {
                stateRange01.isPooling = false;
                stateRange02.isPooling = false;
                stateRange03.isPooling = false;
                //stateRange04.isPooling = false;
                //stateRange05.isPooling = false;
                onceP = true;
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
            onceP = false;
        }

        timer += Time.deltaTime;

        if (timer >= 6f)
        {
            once = false;
            timer = 0;
            character.GetComponent<Renderer>().material = defaultMat;
            enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
            return stateWander;
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
