using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            health -= 5;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Stick"))
        {
            health -= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
    }
}
