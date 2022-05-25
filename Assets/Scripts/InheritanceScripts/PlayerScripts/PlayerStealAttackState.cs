using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealAttackState : MonoBehaviour, IPlayerBaseState
{
    //private GameManager gameManager;

    private PlayerEntity playerEntityInstance;

    public PlayerStealAttackState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }

    private void Awake()
    {
        //playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>();
    }

    #region ReturnAttack

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<BaseProjectile>())
        {
            if (!playerEntityInstance.CanReturnAttack)
            {
                collision.gameObject.SetActive(false);
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                playerEntityInstance.AttackToReturn = collision.gameObject;
                playerEntityInstance.CanReturnAttack = true;
                Debug.Log("Colliding");
            }
        }
    }

    private void OnReturningAttack()
    {
        if (playerEntityInstance.AttackToReturn != null)
        {
            if (playerEntityInstance.AttackToReturn.gameObject.GetComponent<ProjectileManager>().projectileType == ProjectileType.sphere)
            {
                if (playerEntityInstance.CanReturnAttack)
                {
                    if (playerEntityInstance.IsReturningAttack)
                    {
                        playerEntityInstance.CanReturnAttack = false;
                        GameObject gameObj = Instantiate(playerEntityInstance.Bullet, transform.position + transform.forward * 2f, Quaternion.identity); //Instantiation du projectile
                        gameObj.GetComponent<Rigidbody>().AddForce((transform.forward * 2f) * playerEntityInstance.BulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                        playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
                    }
                }
            }
        }
    }

    #endregion

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void ExitState()
    {
        return;
    }

    public void OnUpdate()
    {

        switch(playerEntityInstance.ReturnFireIndex)
        {
            case 0:

                if (playerEntityInstance.IsReturningAttack)
                {
                    playerEntityInstance.CapsuleCollider.enabled = true;
                    if (playerEntityInstance.CapsuleCollider.enabled == true)
                    {
                        playerEntityInstance.Timercapsule += Time.deltaTime;
                        if (playerEntityInstance.Timercapsule >= 2f)
                        {
                            playerEntityInstance.CapsuleCollider.enabled = false;
                            playerEntityInstance.Timercapsule = 0f;
                            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
                        }
                    }
                }

                break;

            case 1:

                if (playerEntityInstance.IsReturningAttack)
                {
                    OnReturningAttack();
                    playerEntityInstance.ReturnFireIndex = 0;
                    playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);

                }

                break;
        }

    }
}
