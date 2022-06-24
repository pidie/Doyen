using TMPro;
using UnityEngine;

namespace Debugging
{
    public class DebugHUD : MonoBehaviour
    {
        [SerializeField] private GameObject playerAvatar;
        [SerializeField] private GameObject maleModel;
        [SerializeField] private GameObject femaleModel;
        [SerializeField] private TMP_Text changeSexButtonText;

        private bool _isFemale;

        private void Awake()
        {
            if (playerAvatar.transform.GetChild(0).name == "Female Player")
                _isFemale = true;

            UpdateChangeSexButtonText();
        }

        public void ChangeSex()
        {
            Destroy(playerAvatar.transform.GetChild(0).gameObject);
            Instantiate(_isFemale ? maleModel : femaleModel, playerAvatar.transform);
        }

        private void UpdateChangeSexButtonText() => changeSexButtonText.text = _isFemale ? "Switch to male" : "Switch to female";
    }
}
