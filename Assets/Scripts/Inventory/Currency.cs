using UnityEngine;

namespace Inventory
{
    public class Currency : Collectable
    {
        [SerializeField] private CurrencyData currencyData;

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
        
                var data = new CollectableData(currencyData.name, currencyData);
                
                if (!isActiveAtCall)
                    gameObject.SetActive(false);
        
                return data;
            }
        }
    }
}
