using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    public GameObject[] warningZone;
    public GameObject[] spellZone;

    public bool start = false;
    public float timer = 0f;

    public abstract void StartSpell();
    public abstract void InitializeSpell();
    public abstract void ShowWarningZone();
    public abstract void ShowSpellEffect(GameObject[] list, GameObject zone);
    
}
