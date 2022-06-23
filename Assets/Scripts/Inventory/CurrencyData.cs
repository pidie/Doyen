using System;
using UnityEngine;

namespace Inventory
{
	[Serializable]
	[CreateAssetMenu(menuName = "Collectable/Currency", fileName = "New Currency")]
	public class CurrencyData : ScriptableObject
	{
		public new string name;
		public Texture icon;
		public int worth;
	}
}