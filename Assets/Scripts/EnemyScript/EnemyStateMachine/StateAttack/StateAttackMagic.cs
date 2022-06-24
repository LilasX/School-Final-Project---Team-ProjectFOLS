using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackMagic : EnemyState
{
    public StatePursue statePursue;
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
    public bool combo;
    public GameObject character;
    public Material defaultMat;
    public Material signMat;
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        //Look At Player

        //Play Animation
        
        //Use Spell

        //When Anim End

        return this;
    }
}
