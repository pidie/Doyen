using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory
{
    // todo : start to distinguish a collectable from explicitly being an ingredient
    [RequireComponent(typeof(Collider))]
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private Ingredient ingredient;
        [SerializeField] private List<IngredientPropertyValue> properties;
        
        private Collider _collider;
        private Outline _outline;
        
        public bool IsAutocollected { get; private set; }
        public CollectableData Data
        {
            get
            {
                var isActiveAtCall = true;
                
                if (!gameObject.activeSelf)
                {
                    isActiveAtCall = false;
                    gameObject.SetActive(true);
                }
                
                var data = new CollectableData(ingredient.name, ingredient, properties);
                
                if (!isActiveAtCall) 
                    gameObject.SetActive(false);

                return data;
            }
        }

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;

            IsAutocollected = false;
        }

        private void OnEnable()
        {
            var defaultProperties = new[]
            {
                ingredient.purity, 
                ingredient.lifeSpan
            };

            var allProperties = defaultProperties.Concat(ingredient.properties);
            
            foreach (var property in allProperties)
            {
                var name = property.name;
                var value = Random.Range(property.minimum, property.maximum);
                properties.Add(new IngredientPropertyValue(name, value));
            }
        }

        public void ToggleOutlineOn() => _outline.enabled = true;

        public void ToggleOutlineOff() => _outline.enabled = false;
    }

    [Serializable]
    public class CollectableData
    {
        public string name;
        public Ingredient ingredient;
        public List<IngredientPropertyValue> values;

        public CollectableData(string name, Ingredient ingredient, List<IngredientPropertyValue> values)
        {
            this.name = name;
            this.ingredient = ingredient;
            this.values = values;
        }
    }
}