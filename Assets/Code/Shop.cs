using System.Collections.Generic;
using Code;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
  List<ItemInfo> items = new List<ItemInfo>();
  public GameObject confirmDialog;

// Use this for initialization
  private void Start()
  {
    GameObject sellerItem = Resources.Load<GameObject>("Prefabs/SellerItem");
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
      items.Add(itemInfo);

      Canvas.ForceUpdateCanvases();
    }
  }

  private void OnClickBuy(int index, ItemInfo itemInfo)
  {
    confirmDialog.SetActive(true);

    Slider slider = confirmDialog.transform.Find("Slider").GetComponent<Slider>();
    TextMeshProUGUI text = confirmDialog.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    slider.minValue = 1;
    slider.maxValue = itemInfo.quantity;

    confirmDialog.transform.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(() =>
    {
      confirmDialog.SetActive(false);

      Globals.Inventory.Add(index, (int) slider.value);

      itemInfo.quantity -= (int) slider.value;
    });

    text.text = $"Do you want to buy 1 {Globals.Games[index].title}?";

    slider.onValueChanged.AddListener((value) => { text.text = $"Do you want to buy {(int) value} {Globals.Games[index].title}?"; });
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
        items[i].quantity += Globals.QuantityManager.quantityDeltas[i];
        items[i].price += Globals.PriceManager.priceDeltas[i];
      }
    }
    for (int i = 0; i < items.Count; i++)
    {
      items[i].gameObject.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"{items[i].quantity} pieces @ ${items[i].price}";
    }
  }
}