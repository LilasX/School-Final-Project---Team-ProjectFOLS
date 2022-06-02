using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerEntity : PhysicalEntity
{

    #region Variables


    //  Variables pour le déplacement
    private CharacterController myCharacter; //Référence au character controller
    [SerializeField] private CinemachineVirtualCamera cam; // Référence à la caméra
    private Vector3 move;
    private float xAxis;
    private float zAxis;

    [Header("Movement Attributes")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float resetSpeedValue = 3f;
    private bool isRunning;
    [SerializeField] private float runningSpeed = 10f;
    private bool isDodging;
    private float dodgeTime = 0f;
    [SerializeField] private float dodgeSpeed = 20f;

    //  Variables pour l'endurance, points de magie et point de vie
    [SerializeField] private Slider staminaBar;
    [SerializeField] private float currentStamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private Slider manaBar;
    [SerializeField] private float currentMana;
    [SerializeField] private float maxMana;
    [SerializeField] private Slider hpBar;

    //  Variables pour la gravité
    public Vector3 velocity;
    [SerializeField] private float gravityForce;
    public float jumpForce;
    private bool isJumping = false;

    // Variables pour tirer
    [Header("Attack Attributes")]
    [SerializeField] private GameObject bullet; //Référence au projectile
    private bool isFiring = false;
    public bool hasFired = false;
    private float timer;
    private float fireRate = 0.5f;
    [SerializeField] private int bulletVelocity = 15;


    //Variables pour arme courte portée
    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject sword;
    private bool isUsingMelee = false;
    private bool hasUsedMelee = false;
    //private bool canAttack = false;
    [SerializeField] private GameObject shield;
    private bool isUsingShield = false;
    //public bool hasUsedShield = false;

    [SerializeField] private GameObject pickableText;
    [SerializeField] private GameObject coolDownExample;
    [SerializeField] private Slider coolDownBar;
    [SerializeField] private bool isUsingPower = false;
    private float powertimer = 10f;


    [SerializeField] private bool canReturnAttack = false;
    [SerializeField] private bool isReturningAttack = false;
    //public bool hasRequestedAttackReturn = false;
    [SerializeField] private int returnFireIndex = 0;

    private GameObject attackToReturn;
    private CapsuleCollider capsuleCollider; // to steal attack
    [SerializeField] private float timercapsule = 0f;

    private Animator animator;
    private bool canDash = true;
    private bool isMoving = false;

    //pick
    private bool isPicking = false;
    private bool hasPickedItem = false;
    private float timeToWait = 0f;


    private bool meleePerformed = false;

    private PlayerEntity playerEntityInstance;


    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float checkGroundDistance = 0.1f;

    [SerializeField] private bool isCollidingWithItem = false;

    public bool hasReturnedAttack = false;
    public float changeStateDelay = 0;

    public bool isStealingAttack = false;
    public bool hasStolenAttack = false;


    #endregion


    #region Encapsulation
    public CharacterController MyCharacter { get => myCharacter; set => myCharacter = value; }
    public CinemachineVirtualCamera Cam { get => cam; set => cam = value; }
    public new Vector3 Move { get => move; set => move = value; }
    public float XAxis { get => xAxis; set => xAxis = value; }
    public float ZAxis { get => zAxis; set => zAxis = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public float RunningSpeed { get => runningSpeed; set => runningSpeed = value; }
    public bool IsDodging { get => isDodging; set => isDodging = value; }
    public float DodgeTime { get => dodgeTime; set => dodgeTime = value; }
    public float DodgeSpeed { get => dodgeSpeed; set => dodgeSpeed = value; }
    public Slider StaminaBar { get => staminaBar; set => staminaBar = value; }
    public float GravityForce { get => gravityForce; set => gravityForce = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public bool IsFiring { get => isFiring; set => isFiring = value; }
    public float Timer { get => timer; set => timer = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int BulletVelocity { get => bulletVelocity; set => bulletVelocity = value; }
    public GameObject Stick { get => stick; set => stick = value; }
    public GameObject Sword { get => sword; set => sword = value; }
    public bool IsUsingMelee { get => isUsingMelee; set => isUsingMelee = value; }
    public bool HasUsedMelee { get => hasUsedMelee; set => hasUsedMelee = value; }

    //public bool CanAttack { get => canAttack; set => canAttack = value; }
    public GameObject Shield { get => shield; set => shield = value; }
    public bool IsUsingShield { get => isUsingShield; set => isUsingShield = value; }
    public GameObject PickableText { get => pickableText; set => pickableText = value; }
    public GameObject CoolDownExample { get => coolDownExample; set => coolDownExample = value; }
    public Slider CoolDownBar { get => coolDownBar; set => coolDownBar = value; }
    public bool IsUsingPower { get => isUsingPower; set => isUsingPower = value; }
    public float Powertimer { get => powertimer; set => powertimer = value; }
    public bool CanReturnAttack { get => canReturnAttack; set => canReturnAttack = value; }
    public bool IsReturningAttack { get => isReturningAttack; set => isReturningAttack = value; }
    public GameObject AttackToReturn { get => attackToReturn; set => attackToReturn = value; }
    public CapsuleCollider CapsuleCollider { get => capsuleCollider; set => capsuleCollider = value; }
    public float Timercapsule { get => timercapsule; set => timercapsule = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public bool CanDash { get => canDash; set => canDash = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsPicking { get => isPicking; set => isPicking = value; }
    public bool HasPickedItem { get => hasPickedItem; set => hasPickedItem = value; }
    public float TimeToWait { get => timeToWait; set => timeToWait = value; }
    public Slider HpBar { get => hpBar; set => hpBar = value; }
    public bool MeleePerformed { get => meleePerformed; set => meleePerformed = value; }
    public float GetCurrentMana { get => currentMana; set => currentMana = value; }
    public float GetMaxMana { get => maxMana; set => maxMana = value; }
    public float GetCurrentStamina { get => currentStamina; set => currentStamina = value; }
    public float GetMaxStamina { get => maxStamina; set => maxStamina = value; }
    public int ReturnFireIndex { get => returnFireIndex; set => returnFireIndex = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsCollidingWithItem { get => isCollidingWithItem; set => isCollidingWithItem = value; }
    public PlayerEntity PlayerEntityInstance { get => playerEntityInstance; set => playerEntityInstance = value; }


    #endregion


    #region StateMachineReferences

    public PlayerStateMachine playerState;
    private PlayerDefaultState defaultState;
    private PlayerJumpState jumpState;
    private PlayerDodgeState dodgeState;
    private PlayerPickState pickState;
    private PlayerBlockState blockState;
    private PlayerRangedAttackState rangedAttackState;
    private PlayerMeleeState meleeState;
    private PlayerStealAttackState stealAttackState;
    private PlayerDeathState deathState;
    private PlayerKnockedState knockedState;

    public PlayerDefaultState DefaultState { get => defaultState; }
    public PlayerJumpState JumpState { get => jumpState; }
    public PlayerDodgeState DodgeState { get => dodgeState; }
    public PlayerPickState PickState { get => pickState; }
    public PlayerBlockState BlockState { get => blockState; }
    public PlayerRangedAttackState RangedAttackState { get => rangedAttackState; }
    public PlayerMeleeState MeleeState { get => meleeState; }
    public PlayerStealAttackState StealAttackState { get => stealAttackState; set => stealAttackState = value; }
    public PlayerDeathState DeathState { get => deathState; set => deathState = value; }
    public PlayerKnockedState KnockedState { get => knockedState; set => knockedState = value; }
    public float ResetSpeedValue { get => resetSpeedValue; set => resetSpeedValue = value; }
    #endregion



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        MyCharacter = GetComponent<CharacterController>(); //Récupère le component character controller dans le gameobject
        Animator = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        CapsuleCollider.enabled = false;

        defaultState = new PlayerDefaultState(this, playerState);
        jumpState = new PlayerJumpState(this, playerState);
        dodgeState = new PlayerDodgeState(this, playerState);
        pickState = new PlayerPickState(this, playerState);
        blockState = new PlayerBlockState(this, playerState);
        rangedAttackState = new PlayerRangedAttackState(this, playerState);
        meleeState = new PlayerMeleeState(this, playerState);
        stealAttackState = new PlayerStealAttackState(this, playerState);
        deathState = new PlayerDeathState(this, playerState);
        knockedState = new PlayerKnockedState(this, playerState);

        playerState = new PlayerStateMachine(DefaultState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        OnManagingSliders();

        OnManagingGravity();

        playerState.Update(); // Excute the running state update
    }

    #region Actions
    public override void OnDeath()
    {
        //Death animation maybe then Respawn to Hub, maybe get health, mana and stamina to full by default?
        animator.SetBool("Dead", true);
        float waitTime = 0f;
        waitTime += Time.deltaTime;
        if (waitTime >= 2.5f)
        {
            animator.SetBool("Dead", false);
        }
    }

    public override void OnHurt(int damage)
    {
        //Player taking damage
        GetCurrentHP -= damage;
        if (GetCurrentHP <= 0)
        {
            OnDeath();
        }
    }

    public override void OnHeal(int hp)
    {
        //Player healing their health points
        if (GetCurrentHP < GetMaxHP)
        {
            GetCurrentHP += hp;
        }
    }

    //Not sure if it's the player that should manage the following methods below...
    public void OnInteract()
    {
        //How the player interacts with the NPC or objects in the hub?
    }

    public void OnBuy()
    {
        //Define how and what happens when player buys an item (potion for example from the shop)
    }

    public void OnSell()
    {
        //If the player can sell items, Define how and what happens when player sells an item (equipment if implementing the concept for example to the shop)
    }

    #endregion

    #region Sliders

    private void OnManagingSliders()
    {
        OnManagingStamina(); // Manage the stamina bar

        OnManagingHealth();

        OnManagingMana();
    }
    private void OnManagingStamina()
    {
        //Barre d'endurance
        staminaBar.value = currentStamina;
        if (currentStamina >= GetMaxStamina) { currentStamina = maxStamina; } else if (currentStamina <= 0) { currentStamina = 0; }
    }

    private void OnManagingHealth()
    {
        HpBar.value = GetCurrentHP;
        if (GetCurrentHP >= GetMaxHP) { GetCurrentHP = GetMaxHP; }
        else if (GetCurrentHP <= 0)
        {
            GetCurrentHP = 0;
            //animator.SetBool("Dead", true);
            //float waitTime = 0f;
            //waitTime += Time.deltaTime;
            //if (waitTime >= 2.5f)
            //{
            //    animator.SetBool("Dead", false);
            //}
        }
    }

    private void OnManagingMana()
    {
        //Barre d'endurance
        manaBar.value = GetCurrentMana;
        if (GetCurrentMana >= GetMaxMana) { GetCurrentMana = GetMaxMana; } else if (GetCurrentMana <= 0) { GetCurrentMana = 0; }
    }
    #endregion

    #region Gravity + Gounded

    private void OnManagingGravity()
    {
        OnApplyingGravity();
        IsPlayerGrounded();
    }

    private void OnApplyingGravity()
    {
        // Gravité
        velocity.y -= GravityForce; //Application de la force de gravité
        MyCharacter.Move(velocity * Time.deltaTime);
    }

    private bool IsPlayerGrounded()
    {
        RaycastHit hit;

        IsGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, checkGroundDistance, groundLayerMask);

        return IsGrounded || MyCharacter.isGrounded;
    }

    #endregion

    #region Collisions and Triggers

    private void OnCollisionStay(Collision collision) //OnStealingAttack
    {
        //if (collision.gameObject.GetComponent<ProjectileManager>())
        //{
        //    if (!CanReturnAttack)
        //    {
        //        collision.gameObject.SetActive(false);
        //        collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //        AttackToReturn = collision.gameObject;
        //        CanReturnAttack = true;
        //        Debug.Log("Colliding");
        //    }
        //}

        //if (collision.gameObject.GetComponent<BaseProjectile>())
        //{
        //    if (!CanReturnAttack)
        //    {
        //        collision.gameObject.SetActive(false);
        //        collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //        AttackToReturn = collision.gameObject;
        //        CanReturnAttack = true;
        //        Debug.Log("Colliding");
        //    }
        //}
    }

    private void OnTriggerStay(Collider other) //OnPickingItem
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.GetComponent<Drops>())
            {
                IsCollidingWithItem = true;
                if (IsPicking)
                {
                    HasPickedItem = true;
                    //playerEntityInstance.PickableText.GetComponent<TMPro.TextMeshProUGUI>().text = "Picked";
                    //other.gameObject.SetActive(false);
                    Destroy(other.gameObject);
                }
            }

            if (other.gameObject.GetComponent<ProjectileManager>())
            {
                if (!CanReturnAttack)
                {
                    other.gameObject.SetActive(false);
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    AttackToReturn = other.gameObject;
                    CanReturnAttack = true;
                    Debug.Log("Colliding");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.GetComponent<Drops>())
            {
                IsCollidingWithItem = false;
            }
        }
    }

    #endregion



}
