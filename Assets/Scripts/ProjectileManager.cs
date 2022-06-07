using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { sphere, cube, capsule, fire }
public class ProjectileManager : MonoBehaviour
{
    public ProjectileType projectileType;

    private ParticleSystem vfx;
    private bool vfxDestroyed;
    private bool instantiateOnce = false;

    private GameManager gameManager;


    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.Instance;
        instantiateOnce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            if (!other.gameObject.GetComponent<PlayerEntity>().isStealingAttack)
            { 
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5; 
            }
            
            if (other.gameObject.GetComponent<PlayerEntity>().isStealingAttack && !other.gameObject.GetComponent<PlayerEntity>().hasReturnedAttack) // A REVOIR
            {
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5;
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
        vfxDestroyed = true;
        if (vfxDestroyed && !instantiateOnce)
        {
            instantiateOnce = true;
            Instantiate(gameManager.fireBurstVfx, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
