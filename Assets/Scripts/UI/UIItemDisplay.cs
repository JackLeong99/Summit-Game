using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class UIItemDisplay : MonoBehaviour
{
    public static UIItemDisplay instance;
    private string goldFormat = "Gold: {0}";
    public TMP_Text gold;
    private string activeItemFormat = "Active Item: {0}";
    public TMP_Text activeItem;
    private string passiveItemFormat = "{0}: {1}";
    //public TMP_Text passiveItem1;
    
    private List<String> itemNamesList = new List<String>();
    private List<TMP_Text> passiveItems= new List<TMP_Text>();

    public GameObject panelObject;
    public TMP_Text PassivePrefab;
    private float nextPassive = -40f;

    [Header("Camera Shake")]
    public float shakeDuration;
    public float shakeIntensity;
    public int flashes;
    public float flashDuration;

    private Inventory inventory;
    private PlayerAbilities abilities;

    //make a list of acquired items which can then be read
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inventory = GameManager.instance.player.GetComponent<Inventory>();
        abilities = GameManager.instance.player.GetComponent<PlayerAbilities>();
        UpdateGold();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void ActiveItemSet()
    {
        gold.text = string.Format(goldFormat, inventory.gold);
        if(abilities.AbilitySlot[3].itemName != "Empty Ability")
        {
            activeItem.text = string.Format(activeItemFormat, abilities.AbilitySlot[3].itemName);
        }
    }

    public void GetNewItem(ItemBase item)
    {
        //active item check
        if(HaveItemBefore(item.itemName))
        {
            itemNamesList.Add(item.itemName);
        }
        for(int i=0; i< itemNamesList.Count; i++)
        {
            if(item.itemName== itemNamesList[i])
            {
                //passiveItems[i].text = string.Format(passiveItemFormat, item.itemName, inventory.GetStacks(item));
                TMP_Text newPassiveText = Instantiate(PassivePrefab, PassivePrefab.transform.position, transform.rotation) as TMP_Text;
                newPassiveText.transform.SetParent(panelObject.transform, false);
                newPassiveText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, nextPassive);
                nextPassive -= 40;
                passiveItems.Add(newPassiveText);
                passiveItems[i].text = string.Format(passiveItemFormat, item.itemName, inventory.GetStacks(item));

            }
        }
    }
    public bool HaveItemBefore(string itemname)
    {
        for(int i=0; i< itemNamesList.Count; i++)
        {
            if(itemname== itemNamesList[i])
            {
                return false;
            }
        }
        return true;
    }

    public void UpdateGold()
    {
        gold.text = string.Format(goldFormat, inventory.gold);
    }

    public void InvalidGold()
    {
        StartCoroutine(FlashRed());
    }

    public IEnumerator FlashRed()
    {
        CameraListener.instance.CameraShake(shakeIntensity, shakeDuration);

        for (int i = 0; i < flashes; i++)
        {
            gold.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            gold.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
        }

        yield return new WaitForEndOfFrame();
    }

    public void ClearListOfItems()
    {        
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("UIItems");
        for (int i = 0; i< allItems.Length; i++)
        {
            Destroy(allItems[i]);
        }
        itemNamesList.Clear();
        passiveItems.Clear();
        nextPassive = -40;
        activeItem.text = "";
        inventory = GameManager.instance.player.GetComponent<Inventory>();
        abilities = GameManager.instance.player.GetComponent<PlayerAbilities>();
        UpdateGold();

    }
}
