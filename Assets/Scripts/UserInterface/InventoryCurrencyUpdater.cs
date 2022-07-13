using System;
using System.Linq;
using Inventory;
using Serialization;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class InventoryCurrencyUpdater : MonoBehaviour
    {
        // the moneyTexts field will likely be depreciated once the UI layout is finalized
        [SerializeField] private GameObject moneyGainNotification;
        [SerializeField] private TMP_Text[] moneyTexts;

        public static Action<int> onGainMoney;

        private void Awake()
        {
            onGainMoney += GainMoney;
            GameManager.onLoadNewScene += InitializeMoneyTexts;
            GameManager.onLoadNewScene += UpdateMoneyTexts;

            // SceneLoader.onSetUIElements += InitializeMoneyTexts;
            // SceneLoader.onAssignReferences += UpdateMoneyTexts;
        }

        private void InitializeMoneyTexts()
        {
            var texts = FindObjectsOfType<TMP_Text>().Where(t => t.name == "MoneyText").ToArray();

            moneyTexts = new TMP_Text[texts.Count()];

            for (var i = 0; i < texts.Count(); i++)
                moneyTexts[i] = texts[i];
            
        }

        private void UpdateMoneyTexts()
        {
            if (moneyTexts.Length == 0) return;
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
