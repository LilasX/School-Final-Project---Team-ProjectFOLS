using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMelee01 : EnemyState
{
    public StatePursue statePursue;
    private Vector3 target;
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public float playerDistance = 0;
    public Animator anim;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //Follow Player
        if (!once1)
        {
            Debug.Log("Melee Once1");
            enemyBehaviour.agent.SetDestination(enemyBehaviour.player.transform.position);
        }

        //Look At Player and Attack Animation
        if (playerDistance >= 3f && !once2)
        {
            Debug.Log("Melee Once2");
            target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
            enemyBehaviour.gameObject.transform.LookAt(target);
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once1 = true;
            once2 = true;
            anim.SetTrigger("IsAttacking");
        }

        //When Attack Animation almost Start
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MSword And Shield Slash") && !once3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1)
            {
                Debug.Log("Melee Once3");
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CanDamage();
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack(); //Remove anim.SetTriggger in EnemyMain
                once3 = true;
            }
        }

        //When Attack Animation almost End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MSword And Shield Slash"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9)
            {
                Debug.Log("Melee Anim End");
                enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                enemyBehaviour.gameObject.GetComponent<EnemyMelee>().CannotDamage();
                once1 = false;
                once2 = false;
                once3 = false;
                return statePursue;
            }
        }

        return this;
    }
}
