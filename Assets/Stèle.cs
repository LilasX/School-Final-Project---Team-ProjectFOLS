using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stèle : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _vfx;
    [SerializeField] private GameObject _interactionButtonText;
    [SerializeField] private GameObject _tipsPanel;



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(true);
            if(_gameManager.player.GetComponent<PlayerEntity>().isInteracting)
            {
                _tipsPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            _interactionButtonText.SetActive(false);
            _tipsPanel.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _interactionButtonText.SetActive(false);
        _tipsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
