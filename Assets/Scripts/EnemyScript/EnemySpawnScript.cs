using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public Transform posOrigin;
    public bool isPooling;
    public bool start = false;
    public float timer = 0f;


    private void Awake()
    {
        if (isPooling)
        {
            posOrigin = this.gameObject.transform;
        }
    }

    public void ReturnOrigin()
    {
        if (isPooling)
        {
            this.gameObject.transform.position = posOrigin.position;
        }
        this.gameObject.SetActive(false);
    }

    public void StartVFX()
    {
        start = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;

            if(timer>= 4f)
            {
                start = false;
                timer = 0;
                ReturnOrigin();
            }
        }
    }
}
