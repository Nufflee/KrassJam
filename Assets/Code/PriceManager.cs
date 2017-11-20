using System.Collections.Generic;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
  public List<int> priceDeltas = new List<int>();

  private void Awake()
  {
    for (int i = 0; i < Globals.Instance.Games.Count; i++)
    {
      priceDeltas.Add(0);
    }
  }

  private void FixedUpdate()
  {
    if (Globals.Instance.Games == null)
      new GameObject().AddComponent<Globals>();

    for (int i = 0; i < Globals.Instance.Games.Count; i++)
    {
      int r = Mathf.RoundToInt(Random.Range(-0.51f, 0.75f));

      if (r < 0 && priceDeltas[i] <= 0)
      {
      }
      else
      {
        priceDeltas[i] = r;
      }
    }
  }
}