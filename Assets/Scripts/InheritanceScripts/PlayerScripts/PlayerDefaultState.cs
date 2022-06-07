using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDefaultState : MonoBehaviour, IPlayerBaseState
{

    private GameManager gameManager;
    private UIManager uiManager;
    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    float resetDodgeInputTimer = 0f;
    //float resetMeleeInputTimer = 0f;
    //float resetSlashInputTimer = 0f;


    public PlayerDefaultState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        uiManager = UIManager.Instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void Move()
    {
        playerEntityInstance.Animator.SetFloat("Speed", 0f, 150f, Time.time);

        //  Mouvement du joueur
        playerEntityInstance.Move = new Vector3(playerEntityInstance.XAxis, 0, playerEntityInstance.ZAxis); //Update des coordonnées d'emplacement du vecteur
        playerEntityInstance.Move = Quaternion.Euler(0, playerEntityInstance.Cam.transform.eulerAngles.y, 0) * playerEntityInstance.Move; //Décale le vecteur de déplacement en fonction de la l'axe y de la caméra 

        if (playerEntityInstance.Move != Vector3.zero) //Quand j'utilise mon input
        {
            //Debug.Log(move);
            playerEntityInstance.IsMoving = true;
            playerEntityInstance.MyCharacter.transform.forward = playerEntityInstance.Move * Time.deltaTime; //Oriente le joueur vers la direction du mouvement
            playerEntityInstance.Animator.SetFloat("Speed", 1f, 20f, Time.time);
        }
        else 
        { 
            playerEntityInstance.IsMoving = false; 
        }

        playerEntityInstance.MyCharacter.Move(playerEntityInstance.Move * playerEntityInstance.Speed * Time.deltaTime); //Déplace le joueur
    }


    private void Run()
    {

        //  Vitesse du joueur en course
        if (playerEntityInstance.IsRunning && playerEntityInstance.IsMoving)
        {
            playerEntityInstance.Speed = playerEntityInstance.RunningSpeed; //Valeur de la vitesse en mode course
            playerEntityInstance.GetCurrentStamina = Mathf.MoveTowards(playerEntityInstance.GetCurrentStamina, 1f, 10f * Time.deltaTime); //Vide la barre d'endurance
            playerEntityInstance.Animator.SetFloat("Speed", 1.2f, 25f, Time.time);
            if (playerEntityInstance.GetCurrentStamina == 1)
            {
                playerEntityInstance.IsRunning = false;
            }
        }
        else
        {
            playerEntityInstance.Speed = playerEntityInstance.ResetSpeedValue; //Valeur de la vitesse en mode Walk
            playerEntityInstance.GetCurrentStamina = Mathf.MoveTowards(playerEntityInstance.GetCurrentStamina, playerEntityInstance.GetMaxStamina, 10f * Time.deltaTime); //Remplit la barre d'endurance
        }
    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
        playerEntityInstance.hasExecutedDodge = false;
    }

    public void ExitState()
    {
        return;
    }


    public void OnUpdate()
    {

        Move();

        Run();

        //DODGE
        if (playerEntityInstance.IsDodging && playerEntityInstance.IsGrounded && playerEntityInstance.GetCurrentStamina > 20f && playerEntityInstance.Move != Vector3.zero) //DONE
        {
            playerEntityInstance.GetCurrentStamina -= 20f;
            playerEntityInstance.DodgeVelocity = playerEntityInstance.Move;
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DodgeState);
        }


        if (playerEntityInstance.hasExecutedDodge)
        {
            resetDodgeInputTimer += Time.deltaTime;
            if (resetDodgeInputTimer >= 0.6f)
            {
                gameManager.inputManager.OnEnable();
                resetDodgeInputTimer = 0f;
                playerEntityInstance.hasExecutedDodge = false;
            }
        }

        if (!playerEntityInstance.hasExecutedDodge)
        {
            resetDodgeInputTimer += Time.deltaTime;
            if(resetDodgeInputTimer >= 0.35f)
            {
                gameManager.inputManager.OnEnable();
                resetDodgeInputTimer = 0f;
            }
        }

        //SHIELD
        if (playerEntityInstance.IsUsingShield && playerEntityInstance.IsGrounded && playerEntityInstance.shieldTimer >= 5) //DONE
        {
            if(playerEntityInstance.GetCurrentMana >= 10)
            {
                playerEntityInstance.OnUsingMana(10);
                uiManager.ShieldImage.SetActive(false);
                playerEntityInstance.playerState.ChangeState(playerEntityInstance.BlockState);
            }
        }

        //FIRE
        if (playerEntityInstance.IsFiring && playerEntityInstance.IsGrounded) //DONE
        {
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.RangedAttackState);
        }

        //MELEE
        if (playerEntityInstance.IsUsingMelee && playerEntityInstance.IsGrounded && playerEntityInstance.Move != Vector3.zero) //A REVOIR
        {
            //Debug.Log("IS ATTACKING"); //MAKE A TIMER ?
            playerEntityInstance.MeleeVelocity = playerEntityInstance.Move;
            gameManager.inputManager.OnDisable();
            //playerEntityInstance.meleeSpeed = 10f;
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.MeleeState);
        }

        if(playerEntityInstance.HasUsedMelee)
        {
            playerEntityInstance.resetMeleeInputTimer += Time.deltaTime;
            if(playerEntityInstance.resetMeleeInputTimer >= 1.2f)
            {
                gameManager.inputManager.OnEnable();
                playerEntityInstance.HasUsedMelee = false;
                playerEntityInstance.resetMeleeInputTimer = 0f;
            }
        }

        //RETURN ATTACK
        if (playerEntityInstance.IsReturningAttack && playerEntityInstance.IsGrounded) // A REVOIR
        {
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.StealAttackState);
        }



        if (playerEntityInstance.hasReturnedAttack)
        {
            playerEntityInstance.Animator.SetBool("Spell", false);
            //playerEntityInstance.hasReturnedAttack = false;
            playerEntityInstance.changeStateDelay += Time.deltaTime;
            if (playerEntityInstance.changeStateDelay >= 1f)
            {
                playerEntityInstance.ReturnFireIndex = 0;
                playerEntityInstance.changeStateDelay = 0f;
                playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
            }
        }

        if (playerEntityInstance.hasFired)
        {
            playerEntityInstance.Animator.SetBool("Spell", false);
            playerEntityInstance.hasFired = false;
        }

        if(playerEntityInstance.hasBlockedAttack)
        {
            playerEntityInstance.BlockCoolDown -= Time.deltaTime;
            if (playerEntityInstance.BlockCoolDown <= 0f)
            {
                playerEntityInstance.hasBlockedAttack = false;
                playerEntityInstance.BlockCoolDown = 10f;
                playerEntityInstance.shieldTimer = 5f;

                if (playerEntityInstance.BlockCoolDown == 10)
                {
                    uiManager.ShieldImage.SetActive(true);
                }
            }
        }



        if (playerEntityInstance.GetCurrentHP <= 0)
        {
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DeathState);
        }

        //SLASH
        if (playerEntityInstance.IsSlashing && playerEntityInstance.IsGrounded)
        {
            playerEntityInstance.SlashVelocity = playerEntityInstance.Move;
            gameManager.inputManager.OnDisable();
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.SlashState);
        }

        if (playerEntityInstance.hasRequestedSlash)
        {
            playerEntityInstance.resetSlashInputTimer += Time.deltaTime;
            if (playerEntityInstance.resetSlashInputTimer >= 0.75f)
            {
                playerEntityInstance.hasRequestedSlash = false;
                //playerEntityInstance.Animator.SetLayerWeight(playerEntityInstance.Animator.GetLayerIndex("UpperBody"), 0f);
                playerEntityInstance.resetSlashInputTimer = 0f;
                gameManager.inputManager.OnEnable();
            }   
        }
    }


    ////PICK
    //if (playerEntityInstance.IsPicking && playerEntityInstance.IsGrounded && playerEntityInstance.IsCollidingWithItem) //DONE
    //{
    //    playerEntityInstance.playerState.ChangeState(playerEntityInstance.PickState);
    //}

    ////JUMP
    //if (playerEntityInstance.IsJumping && playerEntityInstance.IsGrounded) 
    //{
    //    playerEntityInstance.playerState.ChangeState(playerEntityInstance.JumpState);
    //}

}
