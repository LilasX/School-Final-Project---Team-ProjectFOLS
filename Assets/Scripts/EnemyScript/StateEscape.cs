using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEscape : IEnemyState
{
    private static StateEscape instance;

    public StateEscape() { }

    public static StateEscape GetInstance()
    {
        if (instance == null)
            instance = new StateEscape();
        return instance;
    }

    public bool Attacking()
    {
        return false;
    }

    public bool Wandering()
    {
        return false;
    }

    public bool Escaping()
    {
        return true;
    }

}
