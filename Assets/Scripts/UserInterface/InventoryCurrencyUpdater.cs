using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InventoryCurrencyUpdater : MonoBehaviour
    {
        [SerializeField] private GameObject moneyGainNotification;
        [SerializeField] private TMP_Text[] moneyTexts;

        public static Action<int> onGainMoney;

        private void Awake()
        {
            onGainMoney += GainMoney;
        }

        private void Start()
        {
            UpdateMoneyTexts();
        }

        private void UpdateMoneyTexts()
        {
            foreach (var moneyText in moneyTexts)
            {
                moneyText.text = InventoryManager.Instance.Money.ToString();
            }
        }

        private void GainMoney(int value)
        {
            InventoryManager.Instance.Money += value;
            var notification = Instantiate(moneyGainNotification, moneyTexts[0].transform);
            notification.GetComponentInChildren<TMP_Text>().text = $"+{value}";
            UpdateMoneyTexts();
        }
    }
}
