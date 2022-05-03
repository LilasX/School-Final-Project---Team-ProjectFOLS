using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IEnemyState
{
    private static StateAttack instance;

    public StateAttack() { }

    public static StateAttack GetInstance()
    {
        if (instance == null)
            instance = new StateAttack();
        return instance;
    }

    public bool Attacking()
    {
        return true;
    }

    public bool Wandering()
    {
        return false;
    }

    public bool Escaping()
    {
        return false;
    }

}
