using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    [SerializeField] private GameObject player; //Référence au joueur pour accéder à ses variables

    private MyInputAction myInputAction;
    private InputAction meleeAction;

    public static InputManager Instance { get => instance; set => instance = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(Instance);
        }

        myInputAction = new MyInputAction();
        meleeAction = myInputAction.Player.Melee;

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
        player.GetComponent<PlayerMovements>().move = context.ReadValue<Vector2>();
        player.GetComponent<PlayerMovements>().xAxis = player.GetComponent<PlayerMovements>().move.x;
        player.GetComponent<PlayerMovements>().zAxis = player.GetComponent<PlayerMovements>().move.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        player.GetComponent<PlayerMovements>().isJumping = context.performed;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        player.GetComponent<PlayerMovements>().isRunning = context.performed;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        player.GetComponent<PlayerMovements>().isDashing = context.performed;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        player.GetComponent<PlayerMovements>().isFiring = context.performed;
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        //player.GetComponent<PlayerMovements>().isUsingStick = context.performed;
        player.GetComponent<PlayerMovements>().isUsingStick = context.performed;

        //Debug.Log("use melee");
    }

    public void OnMeleePressed(InputAction.CallbackContext context)
    {
        //player.GetComponent<PlayerMovements>().isUsingStick = context.performed;
        player.GetComponent<PlayerMovements>().isUsingStick = true;  

        //Debug.Log("use melee");
    }

    public void OnMeleeReleased(InputAction.CallbackContext context)
    {
        //player.GetComponent<PlayerMovements>().isUsingStick = context.performed;
        player.GetComponent<PlayerMovements>().isUsingStick = false;
        //Debug.Log("use melee");
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        player.GetComponent<PlayerMovements>().isUsingShield = context.performed;
    }

    public void OnPick(InputAction.CallbackContext context)
    {
        player.GetComponent<PlayerMovements>().isPicking = context.performed;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
