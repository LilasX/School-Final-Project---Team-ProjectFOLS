using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { sphere, cube, capsule }
public class ProjectileManager : MonoBehaviour
{
    public ProjectileType projectileType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            if (!other.gameObject.GetComponent<PlayerEntity>().isStealingAttack)
            { other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5; }
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
