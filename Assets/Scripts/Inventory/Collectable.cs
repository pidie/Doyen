using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Outline))]
    public abstract class Collectable : MonoBehaviour
    {
        protected Collider col;
        protected Outline outline;
        
        public bool IsAutocollected { get; protected set; }
        public virtual CollectableData Data { get; set; }

        private void Awake()
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
            col = GetComponent<Collider>();
            col.isTrigger = true;

            IsAutocollected = false;
        }

        public void ToggleOutlineOn() => outline.enabled = true;

        public void ToggleOutlineOff() => outline.enabled = false;
    }

    [Serializable]
    public class CollectableData
    {
        public string name;
        public string displayContents;
        public Texture icon;
        public IngredientData ingredientData;
        public List<IngredientPropertyValue> values;
        public int worth;
        public string onCollectSound;

        public CollectableData(string name, IngredientData ingredientData, List<IngredientPropertyValue> values)
        {
            this.name = name;
            this.ingredientData = ingredientData;
            this.values = values;

            icon = ingredientData.icon;
            onCollectSound = ingredientData.onCollectSound;
            displayContents = "";

            foreach (var value in values.Where(value => value.name != "Purity" && value.name != "LifeSpan"))
                displayContents += $"{Globals.bulletSymbol} {Globals.TitleCase(value.name)}\n";
        }

        public CollectableData(string name, CurrencyData currencyData)
        {
            this.name = name;
            icon = currencyData.icon;
            worth = currencyData.worth;
            onCollectSound = currencyData.onCollectSound;
        }
    }
}