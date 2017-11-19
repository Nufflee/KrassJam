using System.Collections.Generic;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
  public List<float> prices = new List<float>();

  private void Awake()
  {
    for (int i = 0; i < Globals.GameNames.Count; i++)
    {
      prices.Add(0);
    }
  }

  private void Update()
  {
    for (int i = 0; i < Globals.GameNames.Count; i++)
    {
      float r = Random.Range(-0.5f, 0.5f);

      if (r < 0 && prices[i] <= 0)
      {
      }
      else
      {
        prices[i] += r;
      }
    }
  }
}