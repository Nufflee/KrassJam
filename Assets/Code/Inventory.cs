﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  private List<int> inventory = new List<int>();
  List<ItemInfo> items = new List<ItemInfo>();

  GameObject inventoryItem;

  public GameObject confirmDialog;

  private void Awake()
  {
    for (int i = 0; i < Globals.Instance.Games.Count; i++)
    {
      items.Add(null);
      inventory.Add(0);
    }

    inventoryItem = Resources.Load<GameObject>("Prefabs/InventoryItem");
  }

  public void Add(int index, int quantity, ItemInfo info)
  {
    if (inventory[index] == 0 || items[index] == null)
    {
      if (Globals.Instance.Games == null)
        new GameObject().AddComponent<Globals>();

      GameObject item = Instantiate(inventoryItem, transform.Find("Scroll View/Viewport/Content"), false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.Instance.Games[index].title;
      ItemInfo itemInfo = new ItemInfo(quantity, item, info.price);

      item.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{itemInfo.quantity} pieces @ ${itemInfo.price}";
      item.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => OnClickSell(index, itemInfo));
      //item.transform.SetParent(gameObject.transform, false);
      items[index] = itemInfo;

      Canvas.ForceUpdateCanvases();
    }
    else
    {
      //items[index] = info; //MIGHT BREAK STUFF
      items[index].quantity = quantity;

      if (info.price != items[index].price)
      {
        items[index].price = (info.price + items[index].price) / 2;
      }
    }

    inventory[index] += quantity;
    print(quantity);
  }

  public void OnClickSell(int index, ItemInfo itemInfo)
  {
    confirmDialog.SetActive(true);

    Slider slider = confirmDialog.transform.Find("Slider").GetComponent<Slider>();
    TextMeshProUGUI text = confirmDialog.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    slider.minValue = 1;
    slider.maxValue = itemInfo.quantity;
    slider.value = 0;

    int price;

    price = Globals.Instance.ShopManager.CurrentShop.items[index] == null ? Globals.Instance.Games[index].price : Globals.Instance.ShopManager.CurrentShop.items[index].price;

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.RemoveAllListeners();

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      Globals.Instance.MoneyManager.Add((int) slider.value * price);

      confirmDialog.SetActive(false);

      Globals.Instance.ShopManager.CurrentShop.Add(index, (int) slider.value, items[index]);

      items[index].quantity -= (int) slider.value;
      inventory[index] -= (int) slider.value;
    });

    text.text = $"Do you want to sell 1 of {Globals.Instance.Games[index].title} @ ${price} (${price})?";

    slider.onValueChanged.AddListener((value) => { text.text = $"Do you want to sell {(int) value} of {Globals.Instance.Games[index].title} @ ${price} (${price * (int) value})?"; });
  }

  public void Update()
  {
    for (int i = 0; i < items.Count; i++)
    {
      if (items[i] == null)
        continue;

      if (items[i].quantity == 0)
      {
        Destroy(items[i].gameObject);
        items[i] = null;
      }
      else
      {
        items[i].gameObject.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{inventory[i]} pieces @ ${items[i].price}";
      }
    }
  }
}