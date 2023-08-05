using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemsDisplay : MonoBehaviour
{
    PlayerData playerData;

    [SerializeField] GameObject content;
    [SerializeField] RectTransform rectTransform;

    [SerializeField] float size, padding;

    [SerializeField] bool attachDescription;

    [SerializeField] GameObject itemNodePrefab;
    List<GameObject> itemNodes = new List<GameObject>();

    void Awake()
    {
        playerData = PlayerData.Instance;

        playerData.InventoryChangedEvent += OnInventoryChange;
    }

    private void OnEnable()
    {
        DestroyItems();
        InstantiateItems();
    }

    private void OnInventoryChange()
    {
        DestroyItems();
        InstantiateItems();
    }

    private void DestroyItems()
    {
        foreach (GameObject gameObject in itemNodes)
            Destroy(gameObject);
    }

    private void InstantiateItems()
    {
        float contentWidth = rectTransform.rect.width;

        Vector2 pos = new Vector2(padding + size/2, -(padding + size/2));
        Dictionary<Item, int> items = playerData.Items;
        foreach (Item item in items.Keys)
        {
            GameObject itemNode = Instantiate(itemNodePrefab);

            itemNode.GetComponent<Image>().sprite = item.Icon;
            itemNode.transform.Find("QuantityText").GetComponent<TMP_Text>().text = "x"+items[item];

            itemNodes.Add(itemNode);
            itemNode.transform.SetParent(content.transform);

            RectTransform itemRect = itemNode.GetComponent<RectTransform>();
            itemRect.anchoredPosition = pos;
            itemRect.sizeDelta = new Vector2(size, size);

            if (pos.x + size > contentWidth-padding-size/2)
            {
                pos.x = padding + size/2;
                pos.y -= padding + size;
            }
            else
                pos.x += padding + size;

            itemNode.GetComponent<ItemNode>().SetItem(item);
        }
    }
}
