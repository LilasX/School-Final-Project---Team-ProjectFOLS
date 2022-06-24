using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGateActivate : MonoBehaviour
{
    public GameObject Boss;
    public bool once = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!once)
        {
            once = true;
            Boss.GetComponent<EnemyBehaviour>().StartBossAttack();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
