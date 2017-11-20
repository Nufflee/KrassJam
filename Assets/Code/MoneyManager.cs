using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
  public int money;

  private TextMeshProUGUI text;

  private void Awake()
  {
    text = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
    money = 4000;
  }

  public bool Remove(int toRemove)
  {
    if (money - toRemove < 0)
    {
      return false;
    }

    money -= toRemove;

    return true;
  }

  public void Add(int toAdd)
  {
    money += toAdd;
  }

  private void Update()
  {
    text.text = "$" + money;
  }
}