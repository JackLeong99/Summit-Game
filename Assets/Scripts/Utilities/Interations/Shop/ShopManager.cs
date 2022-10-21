using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("Display Information")]
    public GameObject displayParent;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI displayCost;
    public TextMeshProUGUI displayDescription;
    public Sprite purchaseIcon;

    [Header("Shop Stand Prefab")]
    public GameObject shopPrefab;
    public List<GameObject> shopStands;

    [Header("Values")]
    public int standCount;
    public Vector3 spacing;

    [Header("Shop Refresh")]
    public int cost;
    public float multiplier;

    public ItemBase[] itemList;
    public ItemBase storyItem;

    public void Start()
    {
        instance = this;

        SpawnStands();
    }

    public void SpawnStands()
    {
        for (int i = 0; i < standCount; i++)
        {
            shopStands.Add(Instantiate(shopPrefab, gameObject.transform.position + (spacing * i), Quaternion.identity, gameObject.transform));
            shopStands[i].GetComponent<ShopHandler>().item = itemList[Random.Range(0, itemList.Length)];
            shopStands[i].GetComponent<ShopHandler>().SetIcon();
        }

        switch (GameManager.instance.finalReady)
        {
            case true:
                shopStands[0].GetComponent<ShopHandler>().item = storyItem;
                shopStands[0].GetComponent<ShopHandler>().SetIcon();
                break;
        }
    }

    public void DisplayItem(ItemBase selected)
    {
        displayName.text = selected.itemName;
        displayDescription.text = selected.description;
        displayCost.text = selected.cost.ToString();

        EnableDisplay(true);
    }

    public void EnableDisplay(bool state)
    {
        switch (displayParent.activeInHierarchy)
        {
            case bool x when x != state:
                displayParent.SetActive(state);
                break;
        }
    }

    public void Refresh()
    {
        switch (Inventory.instance.CanPurchase(cost))
        {
            case true:
                foreach (var item in shopStands)
                {
                    Destroy(item);
                }

                shopStands.Clear();
                SpawnStands();

                cost = (int)(cost * multiplier);
                break;
        }
    }
}