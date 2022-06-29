using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMagic : EnemyState
{
    public StatePursue statePursue;
    public StateWaiting stateWaiting;
    public Vector3 target;
    public GameObject magic;
    public GameObject spell;
    public GameObject spellSpawn;
    public GameObject spellSign;
    public bool once1 = false;
    public bool once2 = false;
    public bool once3 = false;
    public bool once4 = true; //Prevent Return Instantaneous. In short, Upon Entering the Second StateRange, it immediately goes to return, thus breaking the combo.
    public int randNum;
    public Animator anim;
    public bool isProjectile;
    public float projectileSpeed;
    public bool combo;
    public bool coolDown;
    public float coolTime;
    public GameObject character;
    public Material defaultMat;
    public Material signMat;
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        //Look At Player
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && once1)
        {
            target = new Vector3(enemyBehaviour.player.transform.position.x, enemyBehaviour.gameObject.transform.position.y, enemyBehaviour.player.transform.position.z);
            enemyBehaviour.gameObject.transform.LookAt(target);
        }

        //Play Animation
        if (!once1)
        {
            enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
            enemyBehaviour.enemyAnim.SetBool("IsWalking", false);
            once1 = true;
            anim.SetTrigger("IsThrowing");
            if (signMat)
            {
                character.GetComponent<SkinnedMeshRenderer>().material = signMat;
            }
            /*spell.GetComponent<Rigidbody>().velocity = Vector3.zero;
                spellSign.transform.position = spellSpawn.transform.position;
                spellSign.transform.rotation = spellSpawn.transform.rotation;
                spellSign.SetActive(true);
                //Spell Script Activate*/
        }

        //When Animation reached Halfway
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once2)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                once4 = false;
                if (defaultMat)
                {
                    character.GetComponent<SkinnedMeshRenderer>().material = defaultMat;
                }
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position); 
                //enemyBehaviour.GetComponent<EnemyMain>().OnAttack();

                // Code for launching rocks
                /*projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
                projectile.transform.position = projectileSpawn.transform.position;
                projectile.transform.rotation = projectileSpawn.transform.rotation;
                projectile.SetActive(true);*/

                //ranged = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                //ranged.GetComponent<BaseProjectile>().dmg = 20;
                //ranged.GetComponent<Rigidbody>().AddForce(transform.forward * 24, ForceMode.Impulse);

                magic = Instantiate(spell, spellSpawn.transform.position, spellSpawn.transform.rotation);
                magic.GetComponent<BaseSpell>().StartSpell();
                if (isProjectile)
                {
                    magic.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
                }
                //magic.GetComponent<BaseProjectile>().dmg = 30;


                //spellSign.SetActive(false);
                /*spell.GetComponent<Rigidbody>().velocity = Vector3.zero;
                spell.transform.position = spellSpawn.transform.position;
                spell.transform.rotation = spellSpawn.transform.rotation;
                spell.SetActive(true);
                //Spell Script Activate*/

                once2 = true;
            }
        }

        //When Animation End
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Spell") && !once4)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) //0.9
            {
                //enemyBehaviour.agent.SetDestination(enemyBehaviour.gameObject.transform.position);
                once1 = false;
                once2 = false;
                once3 = false;
                once4 = true;

                if (coolDown)
                {
                    stateWaiting.magicCoolDown = true;
                    stateWaiting.coolTime = coolTime;
                    return stateWaiting;
                }
                else
                {
                    enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                    return statePursue;
                }
                /*if (combo)
                {
                    randNum = Random.Range(0, 3);
                    switch (randNum)
                    {
                        case 2:
                            enemyBehaviour.enemyAnim.SetBool("IsWalking", true);
                            //stateRange02.once1 = true;
                            //stateRange02.rangeDistance = 0;
                            //return stateRange02;
                            return this;
                        default:
                            return statePursue;
                    }
                }
                else
                {
                    return statePursue;
                }*/
            }
        }

        return this;
    }
}
