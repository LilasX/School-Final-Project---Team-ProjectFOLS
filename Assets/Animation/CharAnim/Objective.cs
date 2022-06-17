using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objective : MonoBehaviour
{

    private GameManager manager;
    public bool objActive;
    public TextMeshProUGUI objectiveText;
    private int collision;
    public string mission;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        objActive = false;
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == manager.player && collision == 0)
        {
            objActive = true;
            collision = 1;
            this.gameObject.SetActive(false);
            objectiveText.text = mission;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
