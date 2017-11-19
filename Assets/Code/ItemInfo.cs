using UnityEngine;

namespace Code
{
  public class ItemInfo
  {
    public int quantity;
    public GameObject gameObject;
    public int price;

    public ItemInfo(int quantity, GameObject gameObject, int price)
    {
      this.quantity = quantity;
      this.gameObject = gameObject;
      this.price = price;
    }
  }
}