using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    #region Variables


    //  Variables pour le déplacement
    [SerializeField] private CharacterController myCharacter; //Référence au character controller
    [SerializeField] private CinemachineVirtualCamera cam; // Référence à la caméra
    public Vector3 move;
    public float xAxis;
    public float zAxis;

    [Header("Movement Attributes")]
    [SerializeField] private float speed = 5f;
    public bool isRunning;
    [SerializeField] private float runningSpeed = 10f;
    public bool isDashing;
    private float dashTime = 0f;
    [SerializeField] private float dashSpeed = 20f;

    //  Variables pour l'endurance
    [SerializeField] private Slider staminaBar;
    [SerializeField] private float stamina = 100f;

    //  Variables pour la gravité
    private Vector3 velocity;
    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpForce;
    public bool isJumping = false;

    // Variables pour tirer
    [Header("Attack Attributes")]
    [SerializeField] private GameObject bullet; //Référence au projectile
    public bool isFiring = false;
    private float timer;
    private float fireRate = 0.5f;
    [SerializeField] private int bulletVelocity = 15;

    //Variables pour arme courte portée
    [SerializeField] private GameObject stick;
    public bool isUsingStick = false;
    [SerializeField] private GameObject shield;
    public bool isUsingShield = false;

    [SerializeField] private GameObject pickableText;
    [SerializeField] private GameObject coolDownExample;
    [SerializeField] private Slider coolDownBar;
    [SerializeField] private bool isUsingPower = false;
    private float powertimer = 0f;

    #endregion

    private bool canReturnAttack = false;
    private bool isReturningAttack = false;

    private GameObject attackToReturn;

    private Animator animator;
    public bool canDash = true;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        myCharacter = GetComponent<CharacterController>(); //Récupère le component character controller dans le gameobject
        animator = GetComponent<Animator>();
    }

    //#region Inputs

    //public void OnMove(InputAction.CallbackContext context)
    //{
    //    move = context.ReadValue<Vector2>();
    //    xAxis = move.x;
    //    zAxis = move.y;
    //}

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    isJumping = context.performed;
    //}

    //public void OnRun(InputAction.CallbackContext context)
    //{
    //    isRunning = context.performed;
    //}

    //public void OnDash(InputAction.CallbackContext context)
    //{
    //    isDashing = context.performed;
    //}

    //public void OnFire(InputAction.CallbackContext context)
    //{
    //    isFiring = context.performed;
    //}

    //public void OnShortRange(InputAction.CallbackContext context)
    //{
    //    isUsingStick = context.performed;
    //}

    //public void OnShield(InputAction.CallbackContext context)
    //{
    //    isUsingShield = context.performed;
    //}

    //#endregion


    // Update is called once per frame


    void Update()
    {
        // Gravité
        velocity.y -= gravityForce; //Application de la force de gravité
        myCharacter.Move(velocity * Time.deltaTime);

        Move();

        Run();

        Dash();

        Jump();

        RangedAttack();

        MeleeAttack();

        UseShield();


        coolDownBar.value = powertimer;
        //Cooldown example
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isUsingPower)
            {
                isUsingPower = true;
                coolDownExample.GetComponent<TMPro.TextMeshProUGUI>().text = "power : true";
            }
        }

        if (isUsingPower)
        {
            powertimer += Time.deltaTime;
            if (powertimer >= 10f)
            {
                isUsingPower = false;
                powertimer = 0f;
                coolDownExample.GetComponent<TMPro.TextMeshProUGUI>().text = "power : false";
            }
        }



        if (Input.GetKeyDown(KeyCode.M))
        {
            isReturningAttack = true;
            //Debug.Log(isReturningAttack);
            OnReturningProjectile(attackToReturn);

        }

        StaminaBar();

    }

    private void StaminaBar()
    {
        //Barre d'endurance
        staminaBar.value = stamina;
        if(stamina >= 100) { stamina = 100; } else if(stamina <= 0) { stamina = 0; }
    }

    #region Behaviors

    private void Move()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up, Color.red, 1);

        animator.SetFloat("Speed", 0f);

        //  Mouvement du joueur
        move = new Vector3(xAxis, 0, zAxis); //Update des coordonnées d'emplacement du vecteur
        move = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * move; //Décale le vecteur de déplacement en fonction de la l'axe y de la caméra 

        if (move != Vector3.zero) //Quand j'utilise mon input
        {
            //Debug.Log(move);
            isMoving = true;
            myCharacter.transform.forward = move * Time.deltaTime; //Oriente le joueur vers la direction du mouvement
            animator.SetFloat("Speed", 0.5f);
        }
        else { isMoving = false; }
        myCharacter.Move(move * speed * Time.deltaTime); //Déplace le joueur
    }
    private void Run()
    {

        //  Vitesse du joueur en course
        if (isRunning && isMoving)
        {
            speed = runningSpeed; //Valeur de la vitesse en mode course
            stamina = Mathf.MoveTowards(stamina, 1f, 10f * Time.deltaTime); //Vide la barre d'endurance
            animator.SetFloat("Speed", 1f);
            if(stamina == 1)
            {
                isRunning = false;
            }
        }
        else
        {
            speed = 5f; //Valeur de la vitesse en mode Walk
            stamina = Mathf.MoveTowards(stamina, 100f, 10f * Time.deltaTime); //Remplit la barre d'endurance
        }
    }

    private void Dash()
    {
        //  Vitesse du joueur en esquive
        if (isDashing)
        {
            animator.SetTrigger("Dive");
            speed = dashSpeed;  //Valeur de la vitesse en mode dash
            stamina -= 10f;
            dashTime += Time.deltaTime;
            if (dashTime >= 0.2f)
            {
                isDashing = false;
                dashTime = 0;
            }
        }
    }

    //private void Dash()
    //{
    //    //  Vitesse du joueur en esquive
    //    if (canDash)  
    //    {
    //        if (isDashing)
    //        {
    //            canDash = false;
    //            animator.SetBool("Dive", true);
    //            speed = dashSpeed;  //Valeur de la vitesse en mode dash
    //            dashTime += Time.deltaTime;
    //            if (dashTime >= 0.2f)
    //            {
    //                isDashing = false;
    //                dashTime = 0;
    //            }
    //        }
    //    } 
    //    else
    //    {
    //        animator.SetBool("Dive", false);
    //        canDash = true;
    //    }
    //}

    private void Jump()
    {
        // Jump
        if (isJumping && myCharacter.isGrounded) //Vérifie si j'utilise
        {
            velocity.y = jumpForce; //Application de la force du saut
            Debug.Log("IsJumping");
        }
    }

    private void MeleeAttack()
    {
        // Using Stick
        if (isUsingStick) 
        { 
            stick.SetActive(true);
            animator.SetTrigger("Attack");
            stamina -= 5f;
        } 
        else 
        { 
            stick.SetActive(false);
        } 

    }

    private void RangedAttack()
    {
        // Fire
        timer += Time.deltaTime; //lance le chrono
        if (isFiring)
        {
            if (timer >= fireRate)
            {
                GameObject gameObj = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity); //Instantiation du projectile
                gameObj.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                timer = 0; //reset du chrono
                Destroy(gameObj, 5f); //Destruction du projectile
            }
        }
    }

    private void UseShield()
    {
        if(isUsingShield) 
        {   
            shield.SetActive(true); 
            animator.SetTrigger("Block");
            speed = 0f;
        } 
        else { shield.SetActive(false); speed = 5f; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Drops>())
        {
            pickableText.GetComponent<TMPro.TextMeshProUGUI>().text = "Picked";
            other.gameObject.SetActive(false);
        }
    }

    #region ReturnAttack

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<BaseProjectile>())
    //    {
    //        if (!canReturnAttack)
    //        {
    //            collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
    //            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    //            collision.gameObject.SetActive(false);
    //            //canReturnAttack = true;
    //            Debug.Log("Colliding");
    //        }


    //        if (isReturningAttack)
    //        {
    //            //Debug.Log("Input");
    //            //if(canReturnAttack)
    //            //{
    //                collision.gameObject.SetActive(true);
    //                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //                collision.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
    //            //}
    //        }

    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<BaseProjectile>())
        {
            if (!canReturnAttack)
            {
                collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                attackToReturn =  OnDetectProjectile(collision.gameObject);
                collision.gameObject.SetActive(false);
                canReturnAttack = true;
                Debug.Log("Colliding");
            }
        }
    }

    private GameObject OnDetectProjectile(GameObject projectile)
    {
        return projectile;
    }

    private void OnReturningProjectile(GameObject projectile)
    {
        if (projectile.gameObject.GetComponent<ProjectileManager>().projectileType == ProjectileType.sphere)
        {
            if (isReturningAttack)
            {
                if(canReturnAttack)
                {
                    Debug.Log(projectile.gameObject.GetComponent<ProjectileManager>().projectileType);
                    GameObject gameObj = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity); //Instantiation du projectile
                    gameObj.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                }   
            }
        }
    }


    //private IEnumerator OnCollidingWithProjectile(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<BaseProjectile>())
    //    {
    //        if (isReturningAttack)
    //        {
    //            //Debug.Log("Input");
    //            //if(canReturnAttack)
    //            //{
    //            collision.gameObject.SetActive(true);
    //            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    //            collision.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;
    //            //}
    //        }
    //        yield return null;
    //    }
    //}

    #endregion


    #endregion

}
