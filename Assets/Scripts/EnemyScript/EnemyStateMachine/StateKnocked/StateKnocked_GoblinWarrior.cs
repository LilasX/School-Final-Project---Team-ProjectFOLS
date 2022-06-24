using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateKnocked_GoblinWarrior : StateKnocked
{
    public StateAttackMelee01 stateMeleeLight;
    public StateAttackMelee02 stateMeleeHeavy;
    public StateAttackRange02 stateRangeThrow;
    public StateAttackMagic stateMagicWave;
    public StateAttackMagic stateMagicQuake;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            anim.SetTrigger("IsKnocked");
            character.GetComponent<SkinnedMeshRenderer>().material = knockedMat;
            //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 0f);

            //stateAttack.once = false;
            //stateAttack.canDmg = false;
            if (enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>())
            {
                if (stateMeleeLight)
                {
                    stateMeleeLight.once1 = false;
                    stateMeleeLight.once2 = false;
                    stateMeleeLight.once3 = false;
                }
                if (stateMeleeHeavy)
                {
                    stateMeleeHeavy.once1 = false;
                    stateMeleeHeavy.once2 = false;
                    stateMeleeHeavy.once3 = false;
                }
                if (stateRangeThrow)
                {
                    stateRangeThrow.once1 = false;
                    stateRangeThrow.once2 = false;
                    stateRangeThrow.once3 = false;
                    stateRangeThrow.once4 = true;
                }
                if (stateMagicWave)
                {
                    stateMagicWave.once1 = false;
                    stateMagicWave.once2 = false;
                    stateMagicWave.once3 = false;
                    stateMagicWave.once4 = true;
                }
                if (stateMagicQuake)
                {
                    stateMagicQuake.once1 = false;
                    stateMagicQuake.once2 = false;
                    stateMagicQuake.once3 = false;
                    stateMagicQuake.once4 = true;
                }

                enemyBehaviour.gameObject.GetComponent<EnemyBossWarrior>().CannotDamage(); //Only Usable if this enemy gameobject has script enemymelee
            }
            once1 = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 4f);
                //enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = false;
                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                //character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 0f);
                //enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
                once1 = false;
                once2 = false;
                anim.SetBool("IsWalking", true);
                return statePursue;
            }
        }

        return this;
    }
}
