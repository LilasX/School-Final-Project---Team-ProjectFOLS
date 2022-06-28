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
    [SerializeField] GameObject _cineCam;
    [SerializeField] Transform _lookAt;

    private Material defaultMaterial;
    [SerializeField] private bool wallHit;
    [SerializeField] private GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        //wallHit = Physics.Raycast(_cineCam.transform.position, _cineCam.transform.forward, out hit, _maxDistance, _mask);
        wallHit = Physics.Raycast(_cineCam.transform.position, _lookAt.transform.position, out hit, _maxDistance, _mask);

        if(wallHit)
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Shader Graphs/PhysicalTransparent");
            wall = hit.transform.gameObject;
        }
        else
        {
            //wall.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");
            wall.GetComponent<MeshRenderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
        }
    }
}
