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
    [SerializeField] private GameObject sword;
    public bool isUsingStick = false;
    public bool canAttack = false;
    [SerializeField] private GameObject shield;
    public bool isUsingShield = false;
    //public bool hasUsedShield = false;

    [SerializeField] private GameObject pickableText;
    [SerializeField] private GameObject coolDownExample;
    [SerializeField] private Slider coolDownBar;
    [SerializeField] private bool isUsingPower = false;
    private float powertimer = 10f;

    #endregion

    [SerializeField] private bool canReturnAttack = false;
    [SerializeField] private bool isReturningAttack = false;

    private GameObject attackToReturn;
    private CapsuleCollider capsuleCollider; // to steal attack
    [SerializeField] private float timercapsule = 0f;

    private Animator animator;
    public bool canDash = true;
    private bool isMoving = false;

    //pick
    public bool isPicking = false;
    private bool hasPickedItem = false;
    private float timeToWait = 0f;

    //hp test
    [SerializeField] private Slider hpbar;
    [SerializeField] private float hp;

    public bool meleePerformed = false;

    // Start is called before the first frame update
    void Start()
    {
        myCharacter = GetComponent<CharacterController>(); //Récupère le component character controller dans le gameobject
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
    }



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

        //MeleeAttack();

        UseShield();

        Stamina();

        PickingItem();


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
            powertimer -= Time.deltaTime;
            if (powertimer <= 0f)
            {
                isUsingPower = false;
                powertimer = 10f;
                coolDownExample.GetComponent<TMPro.TextMeshProUGUI>().text = "power : false";
            }
        }


        if(Input.GetKeyDown(KeyCode.N)) { capsuleCollider.enabled = true; } else { timercapsule += Time.deltaTime; if (timercapsule >= 2f) { capsuleCollider.enabled = false; timercapsule = 0f; } }

        if (Input.GetKeyDown(KeyCode.M))
        {
            isReturningAttack = true;
            //Debug.Log(isReturningAttack);
            OnReturningProjectile();
        } else { isReturningAttack = false; }


        hpbar.value = hp;
        if(Input.GetKeyDown(KeyCode.Y))
        {
            hp -= 10;
        }


        Debug.Log(isUsingStick);
    }

    private void Stamina()
    {
        //Barre d'endurance
        staminaBar.value = stamina;
        if(stamina >= 100) { stamina = 100; } else if(stamina <= 0) { stamina = 0; }
    }

    #region Behaviors

    private void Move()
    {
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up, Color.red, 1);

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
            animator.SetBool("Dive", true);
            speed = dashSpeed;  //Valeur de la vitesse en mode dash
            stamina -= 10f;
            dashTime += Time.deltaTime;
            if (dashTime >= 0.2f)
            {
                isDashing = false;
                dashTime = 0;
            }
        } else { animator.SetBool("Dive", false); }
    }

    private void Jump()
    {
        // Jump
        if (isJumping && myCharacter.isGrounded) //Vérifie si j'utilise
        {
            velocity.y = jumpForce; //Application de la force du saut
            Debug.Log("IsJumping");
        }
    }

    public void MeleeAttack() //changed to public
    {
        // Using Stick
        if (isUsingStick)
        {
            if(stamina >= 5)
            {
                stick.SetActive(true);
                animator.SetBool("Attack", true);
                Debug.Log("Attack");
                stamina -= 5f;
            }
        }
        else
        {
            stick.SetActive(false);
            animator.SetBool("Attack", false);
            meleePerformed = false;
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
            animator.SetBool("Block", true);
            speed = 0f;
        }
        else
        {
            shield.SetActive(false);
            speed = 5f;
            animator.SetBool("Block", false);
        }
    }

    #region Pick
    //Picking up
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject != null)
        {
            if (other.gameObject.GetComponent<Drops>())
            {
                //isCollingWithItem = true;
                if (isPicking)
                {
                    Debug.Log("IsPicking");
                    //speed = 0f;
                    //animator.SetBool("Pick", true);
                    hasPickedItem = true;
                    pickableText.GetComponent<TMPro.TextMeshProUGUI>().text = "Picked";
                    //other.gameObject.SetActive(false);
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void PickingItem()
    {
        if(hasPickedItem)
        {
            speed = 0f;
            animator.SetBool("Pick", true);
            sword.SetActive(false);
            timeToWait += Time.deltaTime;
            Debug.Log(timeToWait);
            if (timeToWait >= 2.2f)
            {
                animator.SetBool("Pick", false);
                sword.SetActive(true);
                speed = 5f;
                timeToWait = 0f;
                hasPickedItem = false;
            }
        }
    }


    #endregion

    #region ReturnAttack



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<BaseProjectile>())
        {
            if (!canReturnAttack)
            {
                collision.gameObject.SetActive(false);
                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                attackToReturn = collision.gameObject;
                canReturnAttack = true;
                Debug.Log("Colliding");
            }
        }
    }

    private void OnReturningProjectile()
    {
        if (attackToReturn != null)
        {
            if (attackToReturn.gameObject.GetComponent<ProjectileManager>().projectileType == ProjectileType.sphere)
            {
                if (canReturnAttack)
                {
                    if (isReturningAttack)
                    {
                        canReturnAttack = false;
                        GameObject gameObj = Instantiate(bullet, transform.position + transform.forward * 2f, Quaternion.identity); //Instantiation du projectile
                        gameObj.GetComponent<Rigidbody>().AddForce((transform.forward * 2f) * bulletVelocity, ForceMode.Impulse); //Application de la physique sur le projectile
                    }
                }
            }
        }
    }

    #endregion


    #endregion

}
