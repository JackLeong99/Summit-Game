using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsSpawnable : MonoBehaviour
{
    public float decayTime;
    public GameObject itemDropPrefab;
    public ItemBase guaranteedItem;
    public List<ItemBase> itemPool;
    public float itemSpawnDelay;
    public int minimumGoldReward;
    public int maximumGoldReward;
    public float goldRate;
    private int goldReward;
    private ItemBase randItem;

    void Start()
    {
        goldReward = Random.Range(minimumGoldReward, maximumGoldReward);
        int randItemIndex = Random.Range(0, itemPool.Count);
        randItem = itemPool[randItemIndex];
        StartCoroutine(drop());
    }

    public IEnumerator drop() 
    {
        GameObject dropA = Instantiate(itemDropPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        dropA.GetComponent<ItemDrop>().SetItem(guaranteedItem);


        for (int i = 0; i < goldReward; i++)
        {
            Inventory.instance.updateStat(Inventory.StatType.gold, 1);
            yield return new WaitForSeconds(goldRate);
        }

        yield return new WaitForSeconds(decayTime);
    }
}
