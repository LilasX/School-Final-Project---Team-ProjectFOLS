using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePentagram : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] GameObject pentagram;
    [SerializeField] private Slider _activationProgressBar;
    [SerializeField] private Material _pentagramActivatedMaterial;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject _spiralVfx;
    //[SerializeField] private GameObject circleImage;
    [SerializeField] private GameObject _buttonNameText;
    [SerializeField] private GameObject _interactionButtonText;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _activationProgressBar.value = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _activationProgressBar.gameObject.SetActive(true);
            _interactionButtonText.SetActive(true);

            if (_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                _spiralVfx.gameObject.SetActive(true);
                _interactionButtonText.SetActive(false);

                _activationProgressBar.value = Mathf.MoveTowards(_activationProgressBar.value, _activationProgressBar.maxValue, 5f * Time.deltaTime);

                if (_activationProgressBar.value == _activationProgressBar.maxValue)
                {
                    pentagram.GetComponent<MeshRenderer>().material = _pentagramActivatedMaterial;
                    progressBar.GetComponent<Image>().color = Color.green;
                    _gameManager.pentagramActivatedindex += 1;
                    _activationProgressBar.gameObject.SetActive(false);
                    _spiralVfx.gameObject.SetActive(false);
                    Destroy(pentagram.GetComponent<SphereCollider>());
                }
            }
            else
            {
                _spiralVfx.gameObject.SetActive(false);
            }


            switch (_gameManager.inputManager.GetCurrentScheme())
            {
                case "Keyboard&Mouse":
                    _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[1].ToDisplayString().ToUpper();
                    break;

                case "Gamepad":
                    _buttonNameText.GetComponent<TMPro.TextMeshProUGUI>().text = _gameManager.inputManager.myInputAction.Player.Interact.bindings[0].ToDisplayString().ToUpper();
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _activationProgressBar.gameObject.SetActive(false);
            _spiralVfx.gameObject.SetActive(false);
            _interactionButtonText.SetActive(false);
            //circleImage.gameObject.SetActive(false);
        }
    }

}
