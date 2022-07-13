using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArmoryShopMenu : MonoBehaviour, IBaseMenu
{
    private GameManager _gameManager;

    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;
    [SerializeField] private GameObject ArmoryMenu;

    public Selectable BackBtn;

    Inventory inventory;

    public GameObject[] buyButtons = null;
    //public GameObject[] equipButtons = null;
    public GameObject[] GemsImg = null;
    public GameObject[] activeWeaponCheckImg = null;

    //private float Timer = 0;
    //private bool insideTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _interactionButtonText.SetActive(false);
        // AchievementMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (ArmoryMenu.activeInHierarchy && !insideTrigger)
        //{
        //    Timer += Time.deltaTime;
        //}

        //if (Timer >= 10f && !insideTrigger)
        //{
        //    ArmoryMenu.SetActive(false);
        //    Timer = 0;
        //}

        if (_gameManager.player.GetComponent<PlayerEntity>().isCanceling)
        {
            //_interactionButtonText.SetActive(false);
            ArmoryMenu.SetActive(false);
            MenuOFF();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(false);
            ArmoryMenu.SetActive(false);
            MenuOFF();
            //insideTrigger = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            //insideTrigger = true;
            _interactionButtonText.SetActive(true);

            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                Debug.Log("ArmoryMenu");
                ArmoryMenu.SetActive(true);
                BackBtn.Select();
                MenuON();

                switch (_gameManager.inputHub.GetCurrentScheme())
                {
                    case "Keyboard&Mouse":
                        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                        }
                        else
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                        }
                        break;

                    case "Gamepad":
                        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                        }
                        else
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                        }
                        break;
                }
            }
        }

        //switch (_gameManager.inputManager.GetCurrentScheme())
        //{
        //    case "Keyboard&Mouse":
        //        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
        //        break;

        //    case "Gamepad":
        //        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
        //        break;
        //}
    }

    public void Buy(int index)
    {
        buyButtons[index].SetActive(false);
        GemsImg[index].SetActive(false);
        _gameManager.inventoryscript.gems -= ((index+1)*2);
    }

    public void Equip(int index)
    {
        //Player equip weapon
        activeWeaponCheckImg[index].SetActive(true);
    }

    public void Back()
    {
        ArmoryMenu.SetActive(false);
    }

    public void MenuON()
    {
        _gameManager.player.GetComponent<CharacterController>().enabled = false;
        _gameManager.player.GetComponent<Animator>().enabled = false;
        _gameManager.menuOpened = true;
    }

    public void MenuOFF()
    {
        _gameManager.player.GetComponent<CharacterController>().enabled = true;
        _gameManager.player.GetComponent<Animator>().enabled = true;
        _gameManager.menuOpened = false;
    }
}