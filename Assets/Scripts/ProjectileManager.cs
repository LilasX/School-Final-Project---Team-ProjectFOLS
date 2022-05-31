using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { sphere, cube, capsule, fire }
public class ProjectileManager : MonoBehaviour
{
    public ProjectileType projectileType;

    private ParticleSystem vfx;
    private bool vfxDestroyed;

    private GameManager gameManager;


    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            if (!other.gameObject.GetComponent<PlayerEntity>().isStealingAttack)
            { 
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5; 
            }
            else if (other.gameObject.GetComponent<PlayerEntity>().isStealingAttack && other.gameObject.GetComponent<PlayerEntity>().CanReturnAttack)
            {
                other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP -= 5;
            }
            //else
            //{
            //    other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP = other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP;
            //}

            //if (other.gameObject.GetComponent<PlayerEntity>().isStealingAttack && other.gameObject.GetComponent<PlayerEntity>().ReturnFireIndex == 1)
            //{
            //    other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP = other.gameObject.GetComponent<PlayerEntity>().GetCurrentHP;
            //}
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        vfxDestroyed = true;
        if (vfxDestroyed)
        {
            Instantiate(gameManager.fireBurstVfx, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
