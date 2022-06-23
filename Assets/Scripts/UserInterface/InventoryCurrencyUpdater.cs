using System;
using Inventory;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InventoryCurrencyUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text dramText;

        public static Action<int> onGainMoney;

        private void Awake()
        {
            onGainMoney += GainMoney;
        }

        private void Start()
        {
            dramText.text = InventoryManager.Instance.Money.ToString();
        }

        private void GainMoney(int value)
        {
            InventoryManager.Instance.Money += value;
            dramText.text = InventoryManager.Instance.Money.ToString();
        }
    }
}
