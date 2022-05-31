using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public int coins = 0;
    public GameObject coinsO;
    private TMPro.TextMeshProUGUI coinsText;

    // Start is called before the first frame update
    void Start()
    {
        coinsO.AddComponent<TMPro.TextMeshProUGUI>();
        coinsText = coinsO.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = coins.ToString();
    }
}
