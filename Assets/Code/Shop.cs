using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
  List<ItemInfo> items = new List<ItemInfo>();
  public GameObject confirmDialog;
  private GameObject sellerItem;

  // Use this for initialization
  private void Start()
  {
    for (int i = 0; i < Globals.Games.Count; i++)
    {
      items.Add(null);
    }
    sellerItem = Resources.Load<GameObject>("Prefabs/SellerItem");
    List<int> alreadyChoosen = new List<int>();

    int count = 15; //Random.Range(1, gameNames.Count / 2);
    for (int i = 0;
      i < count;
      i++)
    {
      GameObject item = Instantiate(sellerItem, transform.Find("SellerInventory/Scroll View (1)/Viewport/Content"), false);
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
      items[r] = itemInfo;

      Canvas.ForceUpdateCanvases();
    }
  }

  public void Add(int index, int quantity, ItemInfo info)
  {
    if (items[index] == null || items[index].quantity == 0)
    {
      GameObject item = Instantiate(sellerItem, transform.Find("SellerInventory/Scroll View (1)/Viewport/Content"), false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      if (Globals.Games == null)
        new GameObject().AddComponent<Globals>();

      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.Games[index].title;
      ItemInfo itemInfo = new ItemInfo(info.quantity, item, Globals.Games[index].price);

      item.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{itemInfo.quantity} pieces @ ${itemInfo.price}";
      item.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => OnClickBuy(index, itemInfo));
      //item.transform.SetParent(gameObject.transform, false);
      items[index] = itemInfo;

      Canvas.ForceUpdateCanvases();
    }
    else
    {
      items[index].quantity += quantity;
    }
  }

  private void OnClickBuy(int index, ItemInfo itemInfo)
  {
    confirmDialog.SetActive(true);

    Slider slider = confirmDialog.transform.Find("Slider").GetComponent<Slider>();
    TextMeshProUGUI text = confirmDialog.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    slider.minValue = 1;
    slider.maxValue = itemInfo.quantity;
    slider.value = 0;

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.RemoveAllListeners();

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      items[index] = itemInfo;

      confirmDialog.SetActive(false);

      if (Globals.Inventory == null)
        new GameObject().AddComponent<Globals>();

      Globals.Inventory.Add(index, (int) slider.value, itemInfo);

      items[index].quantity -= (int) slider.value;

      if (items[index].quantity == 0)
      {
        Destroy(items[index].gameObject);
        items.Remove(items[index]);
        items[index] = null;
      }
    });

    if (Globals.Games == null)
      new GameObject().AddComponent<Globals>();

    text.text = $"Do you want to buy 1 of {Globals.Games[index].title} @ ${itemInfo.price}?";

    slider.onValueChanged.AddListener((value) => { text.text = $"Do you want to buy {(int) value} of {Globals.Games[index].title} @ ${itemInfo.price}?"; });
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.GetChild(0).gameObject.activeSelf)
    {
    }
    else
    {
      for (int i = 0; i < items.Count; i++)
      {
        if (items[i] == null)
          continue;

        if (Globals.QuantityManager == null)
          new GameObject().AddComponent<Globals>();

        items[i].quantity += Globals.QuantityManager.quantityDeltas[i];
        items[i].price += Globals.PriceManager.priceDeltas[i];
      }
    }

    for (int i = 0; i < items.Count; i++)
    {
      if (items[i] == null)
        continue;

      if (items[i].quantity == 0)
      {
        Destroy(items[i].gameObject);
        items.Remove(items[i]);
      }

      items[i].gameObject.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{items[i].quantity} pieces @ ${items[i].price}";
    }
  }
}