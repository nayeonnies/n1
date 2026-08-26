using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text moneyText;
    private int totalMoney = 0;

    void Start()
    {
        UpdateDisplay();
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        moneyText.text = "$" + totalMoney;
    }
}