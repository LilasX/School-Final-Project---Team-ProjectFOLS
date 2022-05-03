using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    public bool Attacking();
    public bool Wandering();
    public bool Escaping();
}
