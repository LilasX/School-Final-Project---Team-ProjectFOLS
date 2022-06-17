using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatePentagram : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] GameObject pentagram;
    [SerializeField] private Slider _activationProgressBar;
    [SerializeField] private Material _pentagramActivatedMaterial;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject _spiralVfx;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        _activationProgressBar.value = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _activationProgressBar.gameObject.SetActive(true);

            if (gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                _spiralVfx.gameObject.SetActive(true);

                _activationProgressBar.value = Mathf.MoveTowards(_activationProgressBar.value, _activationProgressBar.maxValue, 5f * Time.deltaTime);

                if (_activationProgressBar.value == _activationProgressBar.maxValue)
                {
                    pentagram.GetComponent<MeshRenderer>().material = _pentagramActivatedMaterial;
                    progressBar.GetComponent<Image>().color = Color.green;
                    gameManager.pentagramActivatedindex += 1;
                    _activationProgressBar.gameObject.SetActive(false);
                    _spiralVfx.gameObject.SetActive(false);
                    Destroy(pentagram.GetComponent<SphereCollider>());
                }
            }
            else
            {
                _spiralVfx.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _activationProgressBar.gameObject.SetActive(false);
            _spiralVfx.gameObject.SetActive(false);
        }
    }

}
