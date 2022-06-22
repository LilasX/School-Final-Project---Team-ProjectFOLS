using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private Shop shop;
    private GameManager _gameManager;
    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;
    private int _index;
    private bool _insideTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _index = 0;
        _insideTrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (_gameManager.inputManager.GetCurrentScheme())
        {
            case "Keyboard&Mouse":
                _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                break;

            case "Gamepad":
                _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                break;
        }

        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if(shopCustomer != null || other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(true);

            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting && _index == 0)
            {
                shop.ShowShop(shopCustomer);
                _index = 1;
            }

            else if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting && _index == 1)
            {
                shop.HideShop();
                _index = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IShopCustomer shopCustomer = other.GetComponent<IShopCustomer>();
        if (shopCustomer != null || other.gameObject.GetComponent<PlayerEntity>())
        {
            shop.HideShop();
            _interactionButtonText.SetActive(false);
            _index = 0;
        }
    }
}
