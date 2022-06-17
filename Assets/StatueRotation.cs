using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueRotation : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] GameObject flames;
    Vector3 _newPos;
    Quaternion _newRot;

    private void Awake()
    {
        flames.SetActive(false);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        flames.SetActive(true);
        _newPos = (gameManager.player.transform.position - this.transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(_newPos);
        _newRot = Quaternion.Lerp(this.transform.rotation, lookRot, 0.2f * Time.deltaTime);
        this.transform.rotation = _newRot;
    }

    private void OnTriggerExit(Collider other)
    {
        flames.SetActive(false);
    }

}
