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
    private Vector3 move;
    private float xAxis;
    private float zAxis;

    [Header("Movement Attributes")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isRunning;
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private bool isDashing;
    private float dashTime = 0f;
    [SerializeField] private float dashSpeed = 20f;

    //  Variables pour l'endurance
    [SerializeField] private Slider staminaBar;
    [SerializeField] private float stamina = 100f;

    //  Variables pour la gravité
    private Vector3 velocity;
    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpForce;
    private bool isJumping = false;

    // Variables pour tirer
    [Header("Attack Attributes")]
    [SerializeField] private GameObject bullet; //Référence au projectile
    public bool isFiring = false;
    private float timer;
    private float fireRate = 0.5f;
    [SerializeField] private int bulletVelocity = 15;

    //Variables pour arme courte portée
    [SerializeField] private GameObject stick;
    private bool isUsingStick = false;

    [SerializeField] private GameObject pickableText;
    [SerializeField] private GameObject coolDownExample;
    [SerializeField] private Slider coolDownBar;
    [SerializeField] private bool isUsingPower = false;
    private float powertimer = 0f;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        myCharacter = GetComponent<CharacterController>(); //Récupère le component character controller dans le gameobject
    }

    #region Inputs

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        xAxis = move.x;
        zAxis = move.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.performed;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.performed;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        isDashing = context.performed;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.performed;
    }

    public void OnShortRange(InputAction.CallbackContext context)
    {
        isUsingStick = context.performed;
    }

    #endregion


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


        coolDownBar.value = powertimer;
        //Cooldown example
        if(Input.GetKeyDown(KeyCode.K))
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



    }

    #region Behaviors

    private void Move()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up, Color.red, 1);

        //  Mouvement du joueur
        move = new Vector3(xAxis, 0, zAxis); //Update des coordonnées d'emplacement du vecteur
        move = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * move; //Décale le vecteur de déplacement en fonction de la l'axe y de la caméra 

        if (move != Vector3.zero) //Quand j'utilise mon input
        {
            Debug.Log(move);
            myCharacter.transform.forward = move * Time.deltaTime; //Oriente le joueur vers la direction du mouvement
        }
        myCharacter.Move(move * speed * Time.deltaTime); //Déplace le joueur
    }
    private void Run()
    {
        staminaBar.value = stamina;

        //  Vitesse du joueur en course
        if (isRunning)
        {
            speed = runningSpeed; //Valeur de la vitesse en mode course
            stamina = Mathf.MoveTowards(stamina, 1f, 10f * Time.deltaTime); //Vide la barre d'endurance
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
            speed = dashSpeed;  //Valeur de la vitesse en mode dash
            dashTime += Time.deltaTime;
            if(dashTime >= 0.2f)
            {
                isDashing = false;
                dashTime = 0;
            }
        } 
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

    private void MeleeAttack()
    {
        // Using Stick
        if (isUsingStick) { stick.SetActive(true); } else { stick.SetActive(false); } //Activation/Désactivation de l'arme
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Drops>())
        {
            pickableText.GetComponent<TMPro.TextMeshProUGUI>().text = "Picked";
            other.gameObject.SetActive(false);
        }
    }

    #endregion

}
