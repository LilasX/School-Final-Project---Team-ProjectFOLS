using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCulling : MonoBehaviour
{
    private GameManager _gameManager;
    RaycastHit hit;
    [SerializeField] LayerMask _mask;
    [SerializeField] float _maxDistance;
    [SerializeField] GameObject _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance)) //, mask))
        {
            //if(!hit.transform.gameObject.GetComponent<PlayerEntity>())
            //{
            Debug.DrawRay(transform.position, transform.forward, Color.black);
            Debug.Log(hit.transform.gameObject.layer);
            if(hit.transform.gameObject.layer != 7)
            {
                _mainCamera.GetComponent<Camera>().cullingMask &= ~(1 << 10);
                _gameManager.cam.m_Lens.FieldOfView = Mathf.Lerp(_gameManager.cam.m_Lens.FieldOfView, 20f, 1f * Time.deltaTime);

            }
            //}
            else 
            {
                _mainCamera.GetComponent<Camera>().cullingMask |= ~(1 << _mask);
                _gameManager.cam.m_Lens.FieldOfView = Mathf.Lerp(_gameManager.cam.m_Lens.FieldOfView, 40f, 1f * Time.deltaTime);
            }
        }
    }
}
