using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart : EnemyState
{
    public Material defaultMat; 
    public GameObject character;
    public Material dissolveMat;
    public float cutoffValue = 3;

    public GameObject spawnVFXInstant;
    public GameObject spawnVFX;
    public float timer;
    public bool once;
    public bool onceI;
    public bool onceP;
    public bool start;

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
