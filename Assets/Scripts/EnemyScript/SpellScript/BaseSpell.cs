using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpell : MonoBehaviour
{
    public GameObject[] warningZone;
    public GameObject[] spellZone;

    public bool start = false;
    public float timer = 0f;

    public Transform posOrigin;
    public bool isPooling;

    private void Awake()
    {
        if (isPooling)
        {
            posOrigin = this.gameObject.transform;
        }
    }

    public void ReturnOrigin()
    {
        if (isPooling)
        {
            this.gameObject.transform.position = posOrigin.position;
        }
        this.gameObject.SetActive(false);
    }

    public abstract void StartSpell();
    public abstract void InitializeSpell();
    public abstract void ShowSpellEffect(GameObject[] list, GameObject zone);
    
}
