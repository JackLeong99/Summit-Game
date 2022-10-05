using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public TextMeshProUGUI inspectDisplay;

    [Header("Shop Stand Prefab")]
    public GameObject shopPrefab;
    public List<GameObject> shopStands;

    [Header("Values")]
    public int standCount;
    public Vector3 spacing;

    public ItemBase[] itemList;
    public ItemBase storyItem;

    public void Start()
    {
        instance = this;
        //itemList = GetAllInstancesOfType("Assets", typeof(ItemBase));

        SpawnStands();
    }

    public void SpawnStands()
    {
        for (int i = 0; i < standCount; i++)
        {
            shopStands.Add(Instantiate(shopPrefab, gameObject.transform.position + (spacing * i), Quaternion.identity, gameObject.transform));
            shopStands[i].GetComponent<ShopHandler>().item = itemList[Random.Range(0, itemList.Length)];
            shopStands[i].GetComponent<ShopHandler>().setCost();
        }

        switch (GameManager.instance.finalReady)
        {
            case true:
                shopStands[0].GetComponent<ShopHandler>().item = storyItem;
                shopStands[0].GetComponent<ShopHandler>().setCost();
                break;
        }
    }

    public void DisplayText(bool state)
    {
        inspectDisplay.gameObject.SetActive(state);
    }

    /*public static ScriptableObject[] GetAllInstancesOfType(string activePath, System.Type activeType)
    {
        string[] guids = AssetDatabase.FindAssets("t:" + activeType.Name, new[] { activePath });
        ScriptableObject[] a = new ScriptableObject[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = (ScriptableObject)AssetDatabase.LoadAssetAtPath(path, activeType);
        }
        return a;
    }*/
}
