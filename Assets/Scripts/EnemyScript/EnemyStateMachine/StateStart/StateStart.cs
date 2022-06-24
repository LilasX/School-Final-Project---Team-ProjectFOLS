using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart : EnemyState
{
    public StateWaiting stateWaiting;
    public StateWander stateWander;
    public StatePursue statePursue;
    public StateKnocked stateKnocked;
    public StateDeath stateDeath;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        //Code to Initialize once, done in children

        return stateWander;
    }
}
