using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDeath : EnemyState
{
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool dissolveStart = false;
    public RuntimeAnimatorController deathAnimator;
    public Animator anim;
    public GameObject character;
    public Material dissolveMat;
    public float cutoffValue = -1;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            enemyBehaviour.enemyAnim.runtimeAnimatorController = deathAnimator;
            character.GetComponent<Renderer>().material = dissolveMat;
            cutoffValue = -1;
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
                enemyBehaviour.gameObject.GetComponent<EnemyMain>().ReturnOrigin();
                enemyBehaviour.gameObject.SetActive(false);
            }
        }

        if (dissolveStart && cutoffValue <= 1f)
        {
            cutoffValue += Time.deltaTime;
            character.GetComponent<Renderer>().material.SetFloat("_CutoffHeight", cutoffValue);
        }

        return this;
    }
}
