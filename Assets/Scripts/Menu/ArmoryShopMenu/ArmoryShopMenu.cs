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
    public Selectable[] equipButtons = null;
    public GameObject[] GemsImg = null;
    public GameObject[] activeWeaponCheckImg = null;

    private int currentWeapon;
    private int lastIndex;

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
          
        //activeWeaponCheckImg[currentWeapon].SetActive(true);

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
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    }
                    else
                    {
                        _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    }
                    break;
            }

            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                switch (_gameManager.inputHub.GetCurrentScheme())
                {
                    case "Keyboard&Mouse":
                        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Cancel.bindings[0].ToDisplayString().ToUpper();
                        }
                        else
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Cancel.bindings[0].ToDisplayString().ToUpper();
                        }
                        break;

                    case "Gamepad":
                        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputHub.myInputAction.Player.Cancel.bindings[1].ToDisplayString().ToUpper();
                        }
                        else
                        {
                            _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Cancel.bindings[1].ToDisplayString().ToUpper();
                        }
                        break;
                }
                Debug.Log("ArmoryMenu");
                ArmoryMenu.SetActive(true);
                BackBtn.Select();
                MenuON();
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
        //if(_gameManager.inventoryscript.gems >= ((index + 1) * 2)) 
        //{
            buyButtons[index].SetActive(false);
            equipButtons[index].gameObject.SetActive(true);
            equipButtons[index].Select();
            GemsImg[index].SetActive(false);
            _gameManager.inventoryscript.gems -= ((index + 1) * 2); 
        //}
    }

    public void Equip(int index)
    {
        currentWeapon = index;
        _gameManager.player.GetComponent<PlayerEntity>().SetCurrentWeapon(index);
        activeWeaponCheckImg[index].SetActive(true);
        if(currentWeapon != lastIndex)
        {
            activeWeaponCheckImg[lastIndex].SetActive(false);
        }
        lastIndex = index;
    }

    public void Back()
    {
        ArmoryMenu.SetActive(false);
        MenuOFF();
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
