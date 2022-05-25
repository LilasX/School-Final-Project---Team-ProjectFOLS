using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttackState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;

    public PlayerRangedAttackState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }

    private void Awake()
    {
        //playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>();
    }

    private void RangedAttack()
    {
        // Fire
        playerEntityInstance.Timer += Time.deltaTime; //lance le chrono
        if (playerEntityInstance.IsFiring)
        {
            if (playerEntityInstance.Timer >= playerEntityInstance.FireRate)
            {
                GameObject gameObj = Instantiate(gameManager.bullet, transform.position + transform.forward, Quaternion.identity); //Instantiation du projectile
                gameObj.GetComponent<Rigidbody>().AddForce(transform.forward * playerEntityInstance.BulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                playerEntityInstance.Timer = 0; //reset du chrono
                Destroy(gameObj, 5f); //Destruction du projectile
            }
        }
        else
        { 
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void OnUpdate()
    {
        RangedAttack();
    }

    public void ExitState()
    {
        return;
    }
}
