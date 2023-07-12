using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsDisplay : MonoBehaviour
{
    PlayerData playerData;

    [SerializeField] RectTransform rectTransform;

    [SerializeField] float size;

    [SerializeField] float padding;

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
        float distance = contentWidth - (padding * 2) / size;

        Vector2 pos = new Vector2(padding + size / 2, padding + size / 2);
        Dictionary<Item, int> items = playerData.Items;
        foreach (Item item in items.Keys)
        {
            GameObject itemNode = Instantiate(itemNodePrefab);

            itemNode.GetComponent<Image>().sprite = item.Icon;

            RectTransform itemRect = itemNode.GetComponent<RectTransform>();
            itemRect.anchoredPosition = pos;
            itemRect.sizeDelta = new Vector2(size,size);

            itemNodes.Add(itemNode);
            itemNode.transform.SetParent(gameObject.transform);

            if (pos.x + size > contentWidth-padding-size/2)
            {
                pos.x = padding + size / 2;
                pos.y += size;
            }
            else
                pos.x += distance;
        }
    }
}
