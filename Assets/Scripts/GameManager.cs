using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    public GameObject player;
    public CharacterController myCharacter;
    public CinemachineVirtualCamera cam;
    public GameObject bullet;
    public GameObject fireSpell;
    public GameObject fireBurstVfx;
    public Inventory inventoryscript;
    public InputManager inputManager;
    public RuntimeAnimatorController defaultController;
    public RuntimeAnimatorController deathController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}