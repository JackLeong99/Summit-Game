using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 rotationSpeed;
    public ItemBase item;

    public SpriteRenderer icon;

    public void Start()
    {
        Physics.IgnoreLayerCollision(13, 15);
    }

    public void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            item.acquire();
            Destroy(gameObject);
        }
    }

    public void SetItem(ItemBase i) 
    {
        item = i;
        //icon.sprite = item.icon;
    }

    public void DisplayItem()
    {
        //TODO add UI elements for hovering over item drops
        Debug.Log(item.itemName);
    }
}
