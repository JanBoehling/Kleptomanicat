using TMPro;
using UnityEngine;

public class PlayerMoneyController : MonoSingleton<PlayerMoneyController>
{
    private uint balance;

    private TMP_Text balanceDisplay;

    protected override void Awake()
    {
        base.Awake();

        balanceDisplay = GameObject.Find("MoneyText (TMP)").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (!balanceDisplay) Debug.LogWarning($"{GetType()}: Could not fetch 'MoneyText (TMP)'.");
    }

    public void AddMoney(uint amount)
    {
        balance += amount;

        if (balanceDisplay) balanceDisplay.text = balance.ToString();
    }
}
