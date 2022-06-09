using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
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
    public RuntimeAnimatorController defaultController;
    public RuntimeAnimatorController deathController;

    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultBtn = null;

    public bool isPaused;

    public void PanelToggle(int position)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            if (position == i)
            {
                StartCoroutine(Wait(0.1f, i));
            }
        }
    }

    IEnumerator Wait(float seconds, int index)
    {
        yield return new WaitForSeconds(seconds);
        defaultBtn[index].Select();
    }

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
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        panels[0].SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        PanelToggle(0);
        UnlockCursor();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}