using UnityEngine;

public class ShopManager : MonoBehaviour
{
  public Shop CurrentShop { get; private set; }

  public void ChangeShop(Shop newShop)
  {
    CurrentShop?.gameObject.SetActive(false);
    CurrentShop = newShop;
    CurrentShop.gameObject.SetActive(true);
  }
}