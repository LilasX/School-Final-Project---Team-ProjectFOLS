using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject hpText;
    [SerializeField] private Slider manaBar;
    [SerializeField] private GameObject manaText;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private GameObject staminaText;
    [SerializeField] private GameObject swordImage;
    [SerializeField] private GameObject shieldImage;

    public static UIManager Instance { get => instance; set => instance = value; }
    public Slider HpBar { get => hpBar; set => hpBar = value; }
    public GameObject HpText { get => hpText; set => hpText = value; }
    public Slider ManaBar { get => manaBar; set => manaBar = value; }
    public GameObject ManaText { get => manaText; set => manaText = value; }
    public Slider StaminaBar { get => staminaBar; set => staminaBar = value; }
    public GameObject StaminaText { get => staminaText; set => staminaText = value; }
    public GameObject SwordImage { get => swordImage; set => swordImage = value; }
    public GameObject ShieldImage { get => shieldImage; set => shieldImage = value; }

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
