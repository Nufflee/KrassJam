using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
  public List<ItemInfo> items = new List<ItemInfo>();
  public GameObject confirmDialog;
  private GameObject sellerItem;

  // Use this for initialization
  private void Start()
  {
    for (int i = 0; i < Globals.Instance.Games.Count; i++)
    {
      items.Add(null);
    }
    sellerItem = Resources.Load<GameObject>("Prefabs/SellerItem");
    List<int> alreadyChoosen = new List<int>();

    int count = Random.Range(1, Globals.Instance.Games.Count);
    for (int i = 0;
      i < count;
      i++)
    {
      GameObject item = Instantiate(sellerItem, transform.Find("SellerInventory/Scroll View (1)/Viewport/Content"), false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      int r = -1;

      do
      {
        r = Globals.Instance.Random.Next(0, Globals.Instance.Games.Count);
        print(Globals.Instance.Random.NextDouble() * count);
      } while (alreadyChoosen.Contains(r));

      alreadyChoosen.Add(r);
      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.Instance.Games[r].title;
      ItemInfo itemInfo = new ItemInfo(Random.Range(10, 200), item, Globals.Instance.Games[r].price);

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

      if (Globals.Instance.Games == null)
        new GameObject().AddComponent<Globals>();

      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.Instance.Games[index].title;
      ItemInfo itemInfo = new ItemInfo(info.quantity, item, Globals.Instance.Games[index].price);

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

    confirmDialog.transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(() => { confirmDialog.SetActive(false); });

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.RemoveAllListeners();

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      //items[index] = itemInfo;

      if (Globals.Instance.Inventory == null)
        new GameObject().AddComponent<Globals>();

      if (!Globals.Instance.MoneyManager.Remove(items[index].price * (int) slider.value))
      {
      }
      else
      {
        confirmDialog.SetActive(false);

        Globals.Instance.Inventory.Add(index, (int) slider.value, itemInfo);

        items[index].quantity -= (int) slider.value;

        if (items[index].quantity == 0)
        {
          Destroy(items[index].gameObject);
          items[index] = null;
        }
      }
    });

    if (Globals.Instance.Games == null)
      new GameObject().AddComponent<Globals>();

    text.text = $"Do you want to buy 1 of {Globals.Instance.Games[index].title} @ ${itemInfo.price} (${itemInfo.price})?";

    slider.onValueChanged.AddListener((value) => { text.text = $"Do you want to buy {(int) value} of {Globals.Instance.Games[index].title} @ ${itemInfo.price} (${itemInfo.price * (int) value})?"; });
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

        if (Globals.Instance.QuantityManager == null)
          new GameObject().AddComponent<Globals>();

        items[i].quantity += Globals.Instance.QuantityManager.quantityDeltas[i];
        items[i].price += Globals.Instance.PriceManager.priceDeltas[i];
      }
    }

    for (int i = 0; i < items.Count; i++)
    {
      if (items[i] == null)
        continue;

      if (items[i].quantity == 0)
      {
        Destroy(items[i].gameObject);
        items[i] = null;
      }

      items[i].gameObject.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{items[i].quantity} pieces @ ${items[i].price}";
    }
  }
}