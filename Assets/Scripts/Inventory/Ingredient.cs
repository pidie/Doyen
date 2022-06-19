using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
	public class Ingredient : Collectable
	{
		[SerializeField] private IngredientData ingredientData;
		[SerializeField] private List<IngredientPropertyValue> properties;
        
		public override CollectableData Data
		{
			get
			{
				var isActiveAtCall = true;
                
				if (!gameObject.activeSelf)
				{
					isActiveAtCall = false;
					gameObject.SetActive(true);
				}
                
				var data = new CollectableData(ingredientData.name, ingredientData, properties);
                
				if (!isActiveAtCall) 
					gameObject.SetActive(false);

				return data;
			}
		}
        
		private void OnEnable()
		{
			var defaultProperties = new[]
			{
				ingredientData.purity, 
				ingredientData.lifeSpan
			};

			var allProperties = defaultProperties.Concat(ingredientData.properties);
            
			foreach (var property in allProperties)
			{
				var name = property.name;
				var value = Random.Range(property.minimum, property.maximum);
				properties.Add(new IngredientPropertyValue(name, value));
			}
		}
	}
}