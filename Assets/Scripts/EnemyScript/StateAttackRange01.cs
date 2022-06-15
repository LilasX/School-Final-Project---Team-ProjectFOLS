using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackRange01 : EnemyState
{
    public StatePursue statePursue;
    public Vector3 target;
    public Vector3 rangePos;
    public GameObject ranged;
    public GameObject projectile;
    public GameObject projectileSpawn;
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public float randomX;
    public float randomZ;
    public float rangeDistance = 0;
    public float playerDistance = 0;
    public Animator anim;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        rangeDistance = Vector3.Distance(rangePos, transform.position);
        playerDistance = Vector3.Distance(transform.position, enemyBehaviour.player.transform.position);

        //Go to a Random Position for Range Attack
        if (!once1)
        {
            randomX = Random.Range(enemyBehaviour.boundBox.center.x - enemyBehaviour.boundBox.extents.x + enemyBehaviour.agent.radius,
                    enemyBehaviour.boundBox.center.x + enemyBehaviour.boundBox.extents.x - enemyBehaviour.agent.radius);
            randomZ = Random.Range(enemyBehaviour.boundBox.center.z - enemyBehaviour.boundBox.extents.z + enemyBehaviour.agent.radius,
                enemyBehaviour.boundBox.center.z + enemyBehaviour.boundBox.extents.z - enemyBehaviour.agent.radius);
            rangePos = new Vector3(randomX, transform.position.y, randomZ);

            enemyBehaviour.agent.SetDestination(rangePos);
            enemyBehaviour.agent.isStopped = false;
            once1 = true;
        }

        //Look At Player & Shoot Animation
        if ((playerDistance >= 7f || rangeDistance <= enemyBehaviour.agent.stoppingDistance) && !once2)
        {
            target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
            enemyBehaviour.gameObject.transform.LookAt(target);
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once2 = true;
            anim.SetTrigger("IsThrowing");
        }

        //When Shoot Animation reached Halfway (Hand goes Forward to Shoot)
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

                // Code for launching rocks
                /*projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                projectile.transform.position = projectileSpawn.transform.position;
                projectile.transform.rotation = projectileSpawn.transform.rotation;
                projectile.SetActive(true);*/

                ranged = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                ranged.GetComponent<Rigidbody>().AddForce(transform.forward * 16, ForceMode.Impulse);
                once3 = true;
            }
        }

        //When Shoot Animation almost End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) //0.9
            {
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once2 = false;
                once3 = false;
                return statePursue;
            }
        }

        return this;
    }
}