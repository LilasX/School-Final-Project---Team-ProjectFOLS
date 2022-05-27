using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    //[SerializeField] private GameObject player; //Référence au joueur pour accéder à ses variables

    private GameManager gameManager;

    private MyInputAction myInputAction;
    private InputAction meleeAction;

    public static InputManager Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        myInputAction = new MyInputAction();
        meleeAction = myInputAction.Player.Melee;
        gameManager = GameManager.Instance;

    }

    private void OnEnable()
    {

        meleeAction.Enable();
        meleeAction.performed += OnMelee;
        //meleeAction.performed += OnMeleePressed;
        //meleeAction.canceled += OnMeleeReleased;
    }

    private void OnDisable()
    {
        //meleeAction.performed += OnMelee;
        meleeAction.Disable();
    }

    #region Inputs

    public void OnMove(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().Move = context.ReadValue<Vector2>();
        gameManager.player.GetComponent<PlayerEntity>().XAxis = gameManager.player.GetComponent<PlayerEntity>().Move.x;
        gameManager.player.GetComponent<PlayerEntity>().ZAxis = gameManager.player.GetComponent<PlayerEntity>().Move.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsJumping = context.performed;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsRunning = context.performed;
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsDodging = context.performed;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsFiring = context.performed;
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        //player.GetComponent<PlayerMovements>().isUsingMelee = context.performed;
        gameManager.player.GetComponent<PlayerEntity>().IsUsingMelee = context.performed;

        //Debug.Log("use melee");
    }

    public void OnMeleePressed(InputAction.CallbackContext context)
    {
        //player.GetComponent<PlayerMovements>().isUsingMelee = context.performed;
        gameManager.player.GetComponent<PlayerEntity>().IsUsingMelee = true;

        //Debug.Log("use melee");
    }

    public void OnMeleeReleased(InputAction.CallbackContext context)
    {
        //player.GetComponent<PlayerMovements>().isUsingMelee = context.performed;
        gameManager.player.GetComponent<PlayerEntity>().IsUsingMelee = false;
        //Debug.Log("use melee");
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsUsingShield = context.performed;
    }

    public void OnPick(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsPicking = context.performed;
    }

    public void OnReturningAttack(InputAction.CallbackContext context)
    {
        gameManager.player.GetComponent<PlayerEntity>().IsReturningAttack = context.performed;
        gameManager.player.GetComponent<PlayerEntity>().ReturnFireIndex++;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

    }
}
