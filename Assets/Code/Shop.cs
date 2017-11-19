using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
  List<GameObject> gameItems = new List<GameObject>();

  private void Awake()
  {
  }

  // Use this for initialization
  private void Start()
  {
    GameObject sellerItem = Resources.Load<GameObject>("Prefabs/SellerItem");

    List<int> alreadyChoosen = new List<int>();

    int count = 15; //Random.Range(1, gameNames.Count / 2);

    for (int i = 0; i < count; i++)
    {
      GameObject item = Instantiate(sellerItem, transform.Find("SellerInventory/Scroll View (1)/Viewport/Content"), false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      int r = -1;

      do
      {
        r = Random.Range(0, count);
      } while (alreadyChoosen.Contains(r));

      alreadyChoosen.Add(r);
      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = Globals.GameNames[r];
      item.transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"100 pieces @ ${Mathf.RoundToInt(Globals.PriceManager.prices[r])}";
      //item.transform.SetParent(gameObject.transform, false);
      gameItems.Add(item);

      Canvas.ForceUpdateCanvases();
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (transform.GetChild(0).gameObject.activeSelf)
      return;

    for (int i = 0; i < gameItems.Count; i++)
    {
      gameItems[i].transform.Find("InfoText").GetComponent<TextMeshProUGUI>().text = $"100 pieces @ ${Mathf.RoundToInt(Globals.PriceManager.prices[i])}";
    }
  }
}