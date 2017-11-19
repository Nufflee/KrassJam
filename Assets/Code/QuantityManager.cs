using System.Collections.Generic;
using UnityEngine;

public class QuantityManager : MonoBehaviour
{
  public List<int> quantityDeltas = new List<int>();

  private void Awake()
  {
    for (int i = 0; i < Globals.Games.Count; i++)
    {
      quantityDeltas.Add(0);
    }
  }

  private void FixedUpdate()
  {
    for (int i = 0; i < Globals.Games.Count; i++)
    {
      int r = Mathf.RoundToInt(Random.Range(-0.51f, 0.51f));

      if (r < 0 && quantityDeltas[i] <= 0)
      {
      }
      else
      {
        quantityDeltas[i] = r;
      }
    }
  }
}