using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InventoryCurrencyUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyTextInventory;
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
            UpdateMoneyTexts();
        }
    }
}
