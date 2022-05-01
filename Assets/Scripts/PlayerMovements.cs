using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    //  Variables pour le d�placement
    [SerializeField] private CharacterController myCharacter; //R�f�rence au character controller
    private Vector3 move;
    private float xAxis;
    private float zAxis;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isDashing;

    //  Variables pour la gravit�
    private Vector3 velocity;
    [SerializeField] private float gravityForce;

    // Variables pour le jump
    [SerializeField] private float jumpForce;
    private bool isJumping = false;

    // Variables pour tirer
    [SerializeField] private GameObject bullet; //R�f�rence au projectile
    public bool isFiring = false;
    private float timer;
    private float fireRate = 0.5f;

    //Variables pour arme courte port�e
    [SerializeField] private GameObject stick;
    private bool isUsingStick = false;

    // Start is called before the first frame update
    void Start()
    {
        myCharacter = GetComponent<CharacterController>(); //R�cup�re le component character controller dans le gameobject
    }

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

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), -transform.up, Color.red, 1);

        //  Mouvement du joueur
        move = new Vector3(xAxis, 0, zAxis); //Update des coordonn�es d'emplacement du vecteur
        if(move != Vector3.zero) //Quand j'utilise mon input
        {
            Debug.Log(move);
            myCharacter.transform.forward = move * Time.deltaTime; //Oriente le joueur vers la direction du mouvement
        }
        myCharacter.Move(move * speed * Time.deltaTime); //D�place le joueur

        //  Vitesse du joueur en course
        if (isDashing) { speed = 15; } else { speed = 5; }

        // Gravit�
        velocity.y -= gravityForce; //Application de la force de gravit�
        myCharacter.Move(velocity * Time.deltaTime);

        // Jump
        if(isJumping && myCharacter.isGrounded) //V�rifie si j'utilise
        {
            velocity.y = jumpForce; //Application de la force du saut
            Debug.Log("IsJumping");
        }

        // Fire
        timer += Time.deltaTime; //lance le chrono
        if(isFiring)
        {
            if (timer >= fireRate)
            {
                GameObject gameObj = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity); //Instantiation du projectile
                gameObj.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse); //Application de la physique sur le projectile
                timer = 0; //reset du chrono
                Destroy(gameObj, 5f); //Destruction du projectile
            }
        }

        // Using Stick
        if (isUsingStick) { stick.SetActive(true); } else { stick.SetActive(false); } //Activation/D�sactivation de l'arme

    }
}
