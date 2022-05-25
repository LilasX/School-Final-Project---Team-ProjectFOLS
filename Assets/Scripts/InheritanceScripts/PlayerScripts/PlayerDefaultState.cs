using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultState : IPlayerBaseState
{

    //private GameManager gameManager;
    private PlayerEntity playerEntityInstance;

    private PlayerStateMachine playerState;

    public PlayerDefaultState(PlayerEntity playerEntity)
    {
        this.playerEntityInstance = playerEntity;
    }

    private void Awake()
    {
       // playerEntityInstance = gameManager.player.GetComponent<PlayerEntity>();
    }

    private void Move()
    {
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up, Color.red, 1);

        playerEntityInstance.Animator.SetFloat("Speed", 0f);

        //  Mouvement du joueur
        playerEntityInstance.Move = new Vector3(playerEntityInstance.XAxis, 0, playerEntityInstance.ZAxis); //Update des coordonnées d'emplacement du vecteur
        playerEntityInstance.Move = Quaternion.Euler(0, playerEntityInstance.Cam.transform.eulerAngles.y, 0) * playerEntityInstance.Move; //Décale le vecteur de déplacement en fonction de la l'axe y de la caméra 

        if (playerEntityInstance.Move != Vector3.zero) //Quand j'utilise mon input
        {
            //Debug.Log(move);
            playerEntityInstance.IsMoving = true;
            playerEntityInstance.MyCharacter.transform.forward = playerEntityInstance.Move * Time.deltaTime; //Oriente le joueur vers la direction du mouvement
            playerEntityInstance.Animator.SetFloat("Speed", 0.5f);
        }
        else { playerEntityInstance.IsMoving = false; }
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.Move * playerEntityInstance.Speed * Time.deltaTime); //Déplace le joueur
    }


    private void Run()
    {

        //  Vitesse du joueur en course
        if (playerEntityInstance.IsRunning && playerEntityInstance.IsMoving)
        {
            playerEntityInstance.Speed = playerEntityInstance.RunningSpeed; //Valeur de la vitesse en mode course
            playerEntityInstance.GetCurrentStamina = Mathf.MoveTowards(playerEntityInstance.GetCurrentStamina, 1f, 10f * Time.deltaTime); //Vide la barre d'endurance
            playerEntityInstance.Animator.SetFloat("Speed", 1f);
            if (playerEntityInstance.GetCurrentStamina == 1)
            {
                playerEntityInstance.IsRunning = false;
            }
        }
        else
        {
            playerEntityInstance.Speed = 5f; //Valeur de la vitesse en mode Walk
            playerEntityInstance.GetCurrentStamina = Mathf.MoveTowards(playerEntityInstance.GetCurrentStamina, 100f, 10f * Time.deltaTime); //Remplit la barre d'endurance
        }
    }

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
        // Gravité
        playerEntityInstance.velocity.y -= playerEntityInstance.GravityForce; //Application de la force de gravité
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.velocity * Time.deltaTime);

        Move();

        Run();
    }
}
