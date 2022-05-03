using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWander : IEnemyState
{
    private static StateWander instance;

    public StateWander() { }

    public static StateWander GetInstance()
    {
        if (instance == null)
            instance = new StateWander();
        return instance;
    }

    public bool Attacking()
    {
        return false;
    }

    public bool Wandering()
    {
        return true;
    }

    public bool Escaping()
    {
        return false;
    }

}
