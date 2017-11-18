using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class Seller : MonoBehaviour
{
  private List<string> gameNames;

  // Use this for initialization
  public void Start()
  {
    GameObject sellerItem = Resources.Load<GameObject>("Prefabs/SellerItem");

    TextAsset asset = Resources.Load<TextAsset>("Data/games");

    gameNames = JsonConvert.DeserializeObject<List<string>>(asset.text);

    List<int> alreadyChoosen = new List<int>();

    int count = 15; //Random.Range(1, gameNames.Count / 2);

    for (int i = 0; i < count; i++)
    {
      GameObject item = Instantiate(sellerItem, transform, false);
      item.GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.0f);

      int r = -1;

      do
      {
        r = Random.Range(0, count);
      } while (alreadyChoosen.Contains(r));

      alreadyChoosen.Add(r);
      item.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = gameNames[r];
      item.transform.SetParent(gameObject.transform, false);

      Canvas.ForceUpdateCanvases();
    }

    //343.96

    /*Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);
    Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);
    Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);*/
  }

  // Update is called once per frame
  public void Update()
  {
  }
}