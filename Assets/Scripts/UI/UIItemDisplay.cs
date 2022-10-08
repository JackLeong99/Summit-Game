using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIItemDisplay : MonoBehaviour
{
    public static UIItemDisplay instance;
    private string goldFormat = "Gold: {0}";
    public TMP_Text gold;
    private string activeItemFormat = "Active Item: {0}";
    public TMP_Text activeItem;


    private Inventory inventory;
    private PlayerAbilities abilities;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        inventory = GameManager.instance.player.GetComponent<Inventory>();
        abilities = GameManager.instance.player.GetComponent<PlayerAbilities>();
        gold.text = string.Format(goldFormat, inventory.gold);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
           // Inventory test = GameManager.instance.player.GetComponent<Inventory>();
           
            //Debug.Log(test.GetStacks(MsBuff.itemName));
            // test.GetStacks(test2.AbilitySlot[0]); //like this but with all ability slots
            // int i = test.gold;
            //Debug.Log(test.items.ContainsKey(1));

            /*for(int i=0; i<test2.Size(); i++)
            {

            }*/
            //Debug.Log(test.GetStacks(test2.defaultAbility));
            //Debug.Log(test.gold);
            Debug.Log(abilities.AbilitySlot[3].itemName);
            ItemDisplayUpdate();
                }
    }

    public void ItemDisplayUpdate()
    {
        gold.text = string.Format(goldFormat, inventory.gold);
        if(abilities.AbilitySlot[3].itemName != "Empty Ability")
        {
            activeItem.text = string.Format(activeItemFormat, abilities.AbilitySlot[3].itemName);
        }
    }
}
