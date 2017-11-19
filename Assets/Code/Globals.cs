using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Globals : MonoBehaviour
{
  public static PriceManager PriceManager { get; private set; }
  public static List<GameInfo> Games { get; private set; }
  public static ShopManager ShopManager { get; private set; }
  public static QuantityManager QuantityManager { get; private set; }
  public static Inventory Inventory { get; private set; }

  private void Awake()
  {
    PriceManager = FindObjectOfType<PriceManager>();
    Games = JsonConvert.DeserializeObject<List<GameInfo>>(Resources.Load<TextAsset>("Data/games").text);
    ShopManager = FindObjectOfType<ShopManager>();
    QuantityManager = FindObjectOfType<QuantityManager>();
    Inventory = FindObjectOfType<Inventory>();
  }
}