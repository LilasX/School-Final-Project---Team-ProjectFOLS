using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackRangeStart : EnemyState
{
    public StatePursue statePursue;
    public StateAttackRange02 stateRange01;
    public StateAttackRange02 stateRange02;
    public float playerDistance;
    private Vector3 target;
    public bool once = false;
    public Animator anim;
    public bool canDmg;
    public int randNum;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        randNum = Random.Range(0, 3);
        switch (randNum)
        {
            case 2:
                return stateRange02;
            default:
                return stateRange01;
        }

        //return this;
    }
}

