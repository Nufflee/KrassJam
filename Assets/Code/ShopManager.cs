using UnityEngine;

public class ShopManager : MonoBehaviour
{
  public Shop CurrentShop { get; private set; }

  public void ChangeShop(Shop newShop)
  {
    CurrentShop?.transform.GetChild(0).gameObject.SetActive(false);
    CurrentShop = newShop;
    CurrentShop.transform.GetChild(0).gameObject.SetActive(true);
  }
}