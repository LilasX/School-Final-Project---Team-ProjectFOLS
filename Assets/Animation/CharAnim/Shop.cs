using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private IShopCustomer shopCustomer;

    public AudioClip buy;
    public AudioSource audioCoin;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateItemButton(Item.ItemType.HealthPotion, Item.GetSprite(Item.ItemType.HealthPotion), "Health Potion", Item.GetPrice(Item.ItemType.HealthPotion), 0);
        CreateItemButton(Item.ItemType.ManaPotion, Item.GetSprite(Item.ItemType.ManaPotion), "Mana Potion", Item.GetPrice(Item.ItemType.ManaPotion), 1);
        audioCoin = GetComponent<AudioSource>();
        HideShop();
    }

    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemPrice, int positionIndex)
    {
        Transform shopItemT = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRT = shopItemT.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRT.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemT.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemT.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(itemPrice.ToString());

        shopItemT.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemT.GetComponent<Button>().onClick.AddListener(delegate { BuyShopItem(itemType); });

    }

    private void BuyShopItem(Item.ItemType itemType)
    {
        if (shopCustomer.SpendCoin(Item.GetPrice(itemType)))
        {
            shopCustomer.BuyItem(itemType);
            audioCoin.PlayOneShot(buy);
        }
        
    }

    public void ShowShop(IShopCustomer shopCustomer)
    {
        this.shopCustomer = shopCustomer;
        gameObject.SetActive(true);
    }

    public void HideShop()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
