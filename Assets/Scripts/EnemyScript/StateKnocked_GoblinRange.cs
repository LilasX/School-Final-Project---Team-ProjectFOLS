using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateKnocked_GoblinRange : EnemyState
{
    public StatePursue statePursue;
    //public StateAttack stateAttack; //Need to enter code to reset once and bool
    public StateAttackRange02 stateRange01;
    public StateAttackRange02 stateRange02;
    public StateAttackRange02 stateRange03;
    public bool once1 = false;
    public bool once2 = false;
    public Animator anim;
    public GameObject character;
    //public Material defaultMat;
    public Material knockedMat;

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
            if (enemyBehaviour.gameObject.GetComponent<EnemyRange>())
            {
                if (stateRange01)
                {
                    stateRange01.once1 = false; stateRange01.once2 = false; stateRange01.once3 = false; stateRange01.once4 = true;
                }
                if (stateRange02)
                {
                    stateRange02.once1 = false; stateRange02.once2 = false; stateRange02.once3 = false; stateRange02.once4 = true;
                }
                if (stateRange03)
                {
                    stateRange03.once1 = false; stateRange03.once2 = false; stateRange03.once3 = false; stateRange03.once4 = true;
                }
            }
            once1 = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 4f);
                enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = false;
                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Knocked"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_OutlineWidth", 0f);
                enemyBehaviour.gameObject.GetComponent<EnemyMain>().canHurt = true;
                once1 = false;
                once2 = false;
                anim.SetBool("IsWalking", true);
                return statePursue;
            }
        }

        return this;
    }
}