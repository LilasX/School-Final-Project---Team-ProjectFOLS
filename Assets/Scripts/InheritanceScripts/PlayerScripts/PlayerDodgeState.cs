using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;

    public PlayerDodgeState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>(); 
    }

    private void Dodge()
    {
        playerEntityInstance.Animator.SetBool("Dive", true);
        playerEntityInstance.Speed = playerEntityInstance.DodgeSpeed;  //Valeur de la vitesse en mode esquive
        playerEntityInstance.GetCurrentStamina -= 10f;
        playerEntityInstance.DodgeTime += Time.deltaTime;
        if (playerEntityInstance.DodgeTime >= 0.2f)
        {
            playerEntityInstance.IsDodging = false;
            playerEntityInstance.DodgeTime = 0;
            playerEntityInstance.Animator.SetBool("Dive", false);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void OnUpdate()
    {
        Dodge();
    }

    public void ExitState()
    {

    }
}
