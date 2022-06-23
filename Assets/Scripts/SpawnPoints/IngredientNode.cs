using System.Collections;
using Inventory;
using UnityEngine;

namespace SpawnPoints
{
	public class IngredientNode : SpawnNode
	{
		private Vector3 _originalScale;

		public override void HandlePickUp()
		{
			if (hasBeenCollected) return;

			PickUpRadius.nodeCollected(this);
			Collectable.ToggleOutlineOff();
			_originalScale = instance.transform.localScale;
			hasBeenCollected = true;
			StartCoroutine(RegrowthTimer());
		}

		private IEnumerator RegrowthTimer()
		{
			instance.transform.localScale = new Vector3(_originalScale.x * .8f, _originalScale.y * 0.2f, _originalScale.z * 0.8f);
			yield return new WaitForSeconds(Collectable.Data.ingredientData.timeToRegrow);
			instance.transform.localScale = _originalScale;
			hasBeenCollected = false;
			PickUpRadius.nodeRefreshed(this);
		}
	}
}