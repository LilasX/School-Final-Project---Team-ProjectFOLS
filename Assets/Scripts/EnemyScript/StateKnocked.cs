using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateKnocked : EnemyState
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool dissolveStart = false;
    public RuntimeAnimatorController deathAnimator;
    public Animator anim;
    public GameObject character;
    public float cutoffValue = 0;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        /*
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); //Have another SetDestination after Death Animation is Done
            enemyBehaviour.enemyAnim.SetBool("IsRunning", false);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            enemyBehaviour.enemyAnim.runtimeAnimatorController = deathAnimator;
            cutoffValue = 0;
            dissolveStart = false;
            once2 = false;
            once3 = false;
            once1 = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                dissolveStart = true;
                once2 = true;
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && !once3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once3 = true;
                enemyBehaviour.gameObject.SetActive(false);
            }
        }

        if (dissolveStart)
        {
            cutoffValue += Time.deltaTime;
            character.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_Cutoff", cutoffValue);
        }
        */
        return this;
    }
}
