using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerRune : MonoBehaviour
{
    private GameManager gameManager;
    public float timer = 0f;
    public bool runeActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerEntity>())
        {
            this.gameObject.GetComponent<MeshRenderer>().material = gameManager.runeActivatedMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            runeActivated = true;
            gameManager.runesList.Add(this.gameObject);
            if(gameManager.runesList.Contains(this.gameObject))
            {
                if(!gameManager.runesListIndex.Contains(this.gameObject))
                {
                gameManager.runesListIndex.Add(this.gameObject);
                }
                else
                {
                    return;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(runeActivated)
        {
            timer += Time.deltaTime;
            if (timer >= 20f)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = gameManager.runeDefaultMaterial;
                timer = 0f;
                runeActivated = false;
                gameManager.runesList.Remove(this.gameObject);
                gameManager.runesListIndex.Remove(this.gameObject);
            }
        }
    }
}
