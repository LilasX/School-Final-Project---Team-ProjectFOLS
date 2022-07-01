using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    public GameObject player;
    public CharacterController myCharacter;
    public CinemachineVirtualCamera cam;
    public GameObject cameraMain;
    public GameObject bullet;
    public GameObject fireSpell;
    public GameObject fireBurstVfx;
    public Inventory inventoryscript;
    public InputManager inputManager;
    public DialogueManager _dialogueManager;
    public RuntimeAnimatorController defaultController;
    public RuntimeAnimatorController deathController;


    public int pentagramActivatedindex = 0;
    public GameObject elevatorPlateform;


    public GameObject gate;
    private List<GameObject> standardRuneList = new List<GameObject>();
    public GameObject rune1;
    public GameObject rune2;
    public GameObject rune3;
    public GameObject rune4;
    public GameObject rune5;
    public HashSet<GameObject> runesList;
    public List<GameObject> runesListIndex;
    public Material runeDefaultMaterial;
    public Material runeActivatedMaterial;

    public int capacity;

    [SerializeField] private GameObject keyboard;
    [SerializeField] private GameObject gamePad;

    public bool Paused = false;

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
        runesList = new HashSet<GameObject>();
        runesListIndex = new List<GameObject>();
        standardRuneList.Add(rune1);
        standardRuneList.Add(rune2);
        standardRuneList.Add(rune3);
        standardRuneList.Add(rune4);
        standardRuneList.Add(rune5);
    }

    // Update is called once per frame
    void Update()
    {
        if (pentagramActivatedindex >= 3)
        {
            elevatorPlateform.GetComponent<Animator>().enabled = true;
        }

        capacity = runesList.Count;

        if (runesList.Count >= 2 && runesListIndex.IndexOf(rune2) == 0 && runesListIndex.IndexOf(rune1) == 1 && runesListIndex.IndexOf(rune3) == 2 && runesListIndex.IndexOf(rune4) == 3 && runesListIndex.IndexOf(rune5) == 4)
        {
            gate.GetComponent<Animator>().enabled = true;
        }


        foreach (GameObject rune in runesListIndex)
        {
            if (gate.GetComponent<Animator>().enabled)
            {
                Destroy(rune.GetComponent<OnTriggerRune>());
                rune.GetComponent<MeshRenderer>().material = runeActivatedMaterial;
            }
        }


        switch (runesList.Count)
        {
            case 2:
                if (runesListIndex.IndexOf(rune2) != 0)
                {
                    runesList.Clear();
                    runesListIndex.Clear();
                    if (runesList.Count == 0)
                    {
                        foreach (GameObject rune in standardRuneList)
                        {
                            rune.GetComponent<MeshRenderer>().material = runeDefaultMaterial;
                            rune.GetComponent<OnTriggerRune>().runeActivated = false;
                        }
                    }
                }
                break;

            case 3:
                if (runesListIndex.IndexOf(rune2) != 0 || runesListIndex.IndexOf(rune1) != 1)
                {
                    runesList.Clear();
                    runesListIndex.Clear();
                    if (runesList.Count == 0)
                    {
                        foreach (GameObject rune in standardRuneList)
                        {
                            rune.GetComponent<MeshRenderer>().material = runeDefaultMaterial;
                            rune.GetComponent<OnTriggerRune>().runeActivated = false;
                        }
                    }
                }
                break;

            case 4:
                if (runesListIndex.IndexOf(rune2) != 0 || runesListIndex.IndexOf(rune1) != 1 || runesListIndex.IndexOf(rune3) != 2)
                {
                    runesList.Clear();
                    runesListIndex.Clear();
                    if (runesList.Count == 0)
                    {
                        foreach (GameObject rune in standardRuneList)
                        {
                            rune.GetComponent<MeshRenderer>().material = runeDefaultMaterial;
                            rune.GetComponent<OnTriggerRune>().runeActivated = false;
                        }
                    }
                }
                break;

            case 5:
                if (runesListIndex.IndexOf(rune2) != 0 || runesListIndex.IndexOf(rune1) != 1 || runesListIndex.IndexOf(rune3) != 2 || runesListIndex.IndexOf(rune4) != 3)
                {
                    runesList.Clear();
                    runesListIndex.Clear();
                    if (runesList.Count == 0)
                    {
                        foreach (GameObject rune in standardRuneList)
                        {
                            rune.GetComponent<MeshRenderer>().material = runeDefaultMaterial;
                            rune.GetComponent<OnTriggerRune>().runeActivated = false;
                        }
                    }
                }
                break;
        }


        switch (inputManager.GetCurrentScheme())
        {
            case "Keyboard&Mouse":
                keyboard.SetActive(true);
                gamePad.SetActive(false);
                break;

            case "Gamepad":
                keyboard.SetActive(false);
                gamePad.SetActive(true);
                break;
        }
    }

    public void PauseGame()
    {
        if (Paused == false)
        {
            Paused = true;
        }
        else Paused = false;
    }
}