using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;
    public static GameManager Instance { get => instance; set => instance = value; }

    public GameObject player;
    public CharacterController myCharacter;
    public CinemachineVirtualCamera cam;
    public GameObject bullet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
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