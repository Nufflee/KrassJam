using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  private List<int> inventory = new List<int>();

  GameObject inventoryItem;

  private void Awake()
  {
    for (int i = 0; i < Globals.Games.Count; i++)
    {
      inventory.Add(0);
    }

    inventoryItem = Resources.Load<GameObject>("Prefabs/InventoryItem");
  }

  public void Add(int index, int quantity)
  {
    if (inventory[index] == 0)
    {
      GameObject item = Instantiate(inventoryItem, transform.Find("MyInventory/Scroll View/Viewport/Content"), false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      int r = -1;

      do
      {
        r = Random.Range(0, count);
      } while (alreadyChoosen.Contains(r));

      alreadyChoosen.Add(r);
      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.Games[r].title;
      ItemInfo itemInfo = new ItemInfo(Random.Range(10, 200), item, Globals.Games[r].price);

      item.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{itemInfo.quantity} pieces @ ${itemInfo.price}";
      item.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => OnClickBuy(r, itemInfo));
      //item.transform.SetParent(gameObject.transform, false);
      items.Add(itemInfo);

      Canvas.ForceUpdateCanvases();
    }

    inventory[index] += quantity;
  }
}