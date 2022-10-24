using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIItemDisplay : MonoBehaviour
{
    public static UIItemDisplay instance;
    private string goldFormat = "Gold: {0}";
    public TMP_Text gold;
    private string activeItemFormat = "Active Item: {0}";
    public TMP_Text activeItem;
    private string passiveItemFormat = "{0}: {1}"; //old set up if we want to use it
    private string passiveItemStack = "{0}";
    
    private List<String> itemNamesList = new List<String>();
    private List<TMP_Text> passiveItems= new List<TMP_Text>();

    public GameObject panelObject;
    public TMP_Text PassivePrefab; 
    private float nextPassive = -40f;
    private float nextPassiveImageX = -40;
    private float nextPassiveImageY = -40;
    public TMP_Text numberOfStacks;
    private float nextPassiveNumberX = -25;
    private float nextPassiveNumberY = -57;
    public Image passiveItem; 

    public Image activeItemImage;
    public Slider cooldownTimer;
    public Image sliderBackground;
    public bool onCooldown=false;


    [Header("Camera Shake")]
    public float shakeDuration;
    public float shakeIntensity;
    public int flashes;
    public float flashDuration;

    private Inventory inventory;
    private PlayerAbilities abilities;

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

    void Update()
    {
        if(onCooldown)
        {
            cooldownTimer.value+=Time.deltaTime;
            if(cooldownTimer.value== cooldownTimer.maxValue)
            {
                onCooldown = false;
            }
        }
    }

    public void ActiveItemSet()
    {
        gold.text = string.Format(goldFormat, inventory.gold);
        if(abilities.AbilitySlot[3].itemName != "Empty Ability")
        {
            activeItemImage.color = Color.white;
            sliderBackground.color = new Color(163f, 171f, 159f, 0.3f);
            activeItemImage.sprite = abilities.AbilitySlot[3].icon;
            cooldownTimer.maxValue = abilities.AbilitySlot[3].cooldown;
            cooldownTimer.value = cooldownTimer.maxValue;
            //sactiveItem.text = string.Format(activeItemFormat, abilities.AbilitySlot[3].itemName);
        }
    }
    //updated version of old system
    /*public void GetNewItem(ItemBase item)
    {
        if (HaveItemBefore(item.itemName))
        {
            itemNamesList.Add(item.itemName);
            TMP_Text newPassiveText = Instantiate(PassivePrefab, PassivePrefab.transform.position, transform.rotation) as TMP_Text;
            newPassiveText.transform.SetParent(panelObject.transform, false);
            newPassiveText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-50, nextPassive);
            nextPassive -= 40;
            passiveItems.Add(newPassiveText);
            passiveItems[itemNamesList.Count-1].text = string.Format(passiveItemFormat, item.itemName, inventory.GetStacks(item));
        }
        for (int i = 0; i < itemNamesList.Count; i++)
        {
            if (item.itemName == itemNamesList[i])
            {
                passiveItems[i].text = string.Format(passiveItemFormat, item.itemName, inventory.GetStacks(item));
            }
        }
    }*/
    public void GetNewItem(ItemBase item)
    {
        if (HaveItemBefore(item.itemName))
        {
            itemNamesList.Add(item.itemName);
            Image newPassiveImage = Instantiate(passiveItem, passiveItem.transform.position, transform.rotation) as Image;
            newPassiveImage.sprite = item.icon;
            newPassiveImage.transform.SetParent(panelObject.transform, false);
            newPassiveImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(nextPassiveImageX, nextPassiveImageY);
            nextPassiveImageX -= 60;

            TMP_Text newPassiveText = Instantiate(numberOfStacks, numberOfStacks.transform.position, transform.rotation) as TMP_Text;
            newPassiveText.transform.SetParent(panelObject.transform, false);
            newPassiveText.GetComponent<RectTransform>().anchoredPosition = new Vector2(nextPassiveNumberX, nextPassiveNumberY);
            nextPassiveNumberX -= 60;

            passiveItems.Add(newPassiveText);
            passiveItems[itemNamesList.Count - 1].text = string.Format(passiveItemStack, inventory.GetStacks(item));            
            if(itemNamesList.Count%8==0)
            {
                nextPassiveImageX = -40;
                nextPassiveImageY -= 55;
                nextPassiveNumberX = -25;
                nextPassiveNumberY -= 55;
            }
 
        }
        for (int i = 0; i < itemNamesList.Count; i++)
        {
            if (item.itemName == itemNamesList[i])
            {
                passiveItems[i].text = string.Format(passiveItemStack, inventory.GetStacks(item));
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
        //nextPassive = -40; //from old text display items
        nextPassiveImageX = -40;
        nextPassiveImageY = -40;
        nextPassiveNumberX = -25;
        nextPassiveNumberY = -57;
        activeItem.text = "";
        inventory = GameManager.instance.player.GetComponent<Inventory>();
        abilities = GameManager.instance.player.GetComponent<PlayerAbilities>();
        UpdateGold();
        sliderBackground.color = new Color(0f, 0f, 0f, 0f);
        activeItemImage.color = new Color(0f, 0f, 0f, 0f);
    }
}
