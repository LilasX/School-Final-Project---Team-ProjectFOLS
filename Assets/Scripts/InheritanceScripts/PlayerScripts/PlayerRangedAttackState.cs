using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttackState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerRangedAttackState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void RangedAttack()
    {
        playerEntityInstance.Animator.SetBool("Spell", true);
        // Fire
        playerEntityInstance.Timer += Time.deltaTime; //lance le chrono
        if (playerEntityInstance.IsFiring)
        {
            if (playerEntityInstance.Timer >= playerEntityInstance.FireRate)
            {
                GameObject gameObj = Instantiate(gameManager.bullet, playerEntityInstance.transform.position + playerEntityInstance.transform.forward, Quaternion.identity); //Instantiation du projectile
                gameObj.GetComponent<Rigidbody>().AddForce(playerEntityInstance.transform.forward * playerEntityInstance.BulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                playerEntityInstance.Timer = 0; //reset du chrono
                playerEntityInstance.hasFired = true;
                Destroy(gameObj, 5f); //Destruction du projectile
            }
        }
        else
        {
            //playerEntityInstance.Animator.SetBool("Spell", true);
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
