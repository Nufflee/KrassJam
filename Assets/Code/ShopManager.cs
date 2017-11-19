using UnityEngine;

public class ShopManager : MonoBehaviour
{
  public Shop CurrentShop { get; private set; }

  private void Awake()
  {
    CurrentShop = GameObject.Find("Shop1").GetComponent<Shop>();
  }

  public void ChangeShop(Shop newShop)
  {
    CurrentShop?.transform.GetChild(0).gameObject.SetActive(false);
    CurrentShop = newShop;
    CurrentShop.transform.GetChild(0).gameObject.SetActive(true);
  }
}