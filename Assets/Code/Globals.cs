using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Globals : MonoBehaviour
{
  public static PriceManager PriceManager { get; private set; }
  public static List<string> GameNames { get; private set; }
  public static ShopManager ShopManager { get; private set; }

  private void Awake()
  {
    PriceManager = FindObjectOfType<PriceManager>();
    GameNames = JsonConvert.DeserializeObject<List<string>>(Resources.Load<TextAsset>("Data/games").text);
    ShopManager = FindObjectOfType<ShopManager>();
  }
}