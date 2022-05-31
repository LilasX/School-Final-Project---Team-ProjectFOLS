using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXTriggerTest : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyMain>())
        {
            other.gameObject.GetComponent<EnemyMain>().GetCurrentHP -= 20;
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
