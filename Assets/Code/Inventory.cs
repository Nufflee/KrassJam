using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  private List<int> inventory = new List<int>();
  List<ItemInfo> items = new List<ItemInfo>();

  GameObject inventoryItem;

  private void Awake()
  {
    for (int i = 0; i < Globals.Games.Count; i++)
    {
      items.Add(null);
      inventory.Add(0);
    }

    inventoryItem = Resources.Load<GameObject>("Prefabs/InventoryItem");
  }

  public void Add(int index, int quantity, ItemInfo info)
  {
    if (inventory[index] == 0)
    {
      GameObject item = Instantiate(inventoryItem, transform.Find("Scroll View/Viewport/Content"), false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.Games[index].title;
      ItemInfo itemInfo = new ItemInfo(quantity, item, info.price);

      item.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{itemInfo.quantity} pieces @ ${itemInfo.price}";
      //item.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => OnClickBuy(r, itemInfo));
      //item.transform.SetParent(gameObject.transform, false);
      items.Insert(index, itemInfo);

      Canvas.ForceUpdateCanvases();
    }
    else
    {
      items[index].quantity = quantity;

      if (info.price != items[index].price)
      {
        info.price = (info.price + items[index].price) / 2;
      }
    }

    inventory[index] += quantity;
    print(quantity);
  }

  public void Update()
  {
    for (int i = 0; i < items.Count; i++)
    {
      if (items[i] == null)
        continue;

      items[i].gameObject.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{inventory[i]} pieces @ ${items[i].price}";
    }
  }
}