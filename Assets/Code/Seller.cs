using UnityEngine;

public class Seller : MonoBehaviour
{
  // Use this for initialization
  public void Start()
  {
    GameObject sellerItem = Resources.Load<GameObject>("Prefabs/SellerItem");

    //343.96
    Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);
    Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);
    Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);
    Instantiate(sellerItem, transform, false).GetComponent<RectTransform>().sizeDelta = new Vector2(349.1f, 113.6f);
  }

  // Update is called once per frame
  public void Update()
  {
  }
}