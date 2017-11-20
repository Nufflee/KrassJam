using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

public class Globals : MonoBehaviour
{
  public static Globals Instance
  {
    get { return instance == null ? instance = new GameObject().AddComponent<Globals>() : instance; }
  }

  private static Globals instance;

  public PriceManager PriceManager { get; private set; }
  public List<GameInfo> Games { get; private set; }
  public ShopManager ShopManager { get; private set; }
  public QuantityManager QuantityManager { get; private set; }
  public Inventory Inventory { get; private set; }
  public MoneyManager MoneyManager { get; private set; }
  public Random Random { get; } = new Random();

  private void Awake()
  {
    PriceManager = FindObjectOfType<PriceManager>();
    Games = JsonConvert.DeserializeObject<List<GameInfo>>(Resources.Load<TextAsset>("Data/games").text);
    ShopManager = FindObjectOfType<ShopManager>();
    QuantityManager = FindObjectOfType<QuantityManager>();
    Inventory = FindObjectOfType<Inventory>();
    MoneyManager = FindObjectOfType<MoneyManager>();
  }
}