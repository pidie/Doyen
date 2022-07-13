using UnityEngine;

namespace Serialization
{
    public class AssignReferences : MonoBehaviour
    {
        private bool _isFirstUpdate = true;
        private bool _isSecondUpdate;
        private void Start() => SceneLoader.onAssignReferences.Invoke();
        
        private void Update()
        {
            if (_isSecondUpdate)
            {
                SceneLoader.onSetUIElements.Invoke();
                GameManager.onLoadNewScene.Invoke();
                _isSecondUpdate = false;
            }
            else if (_isFirstUpdate)
            {
                SceneLoader.onAssignReferences.Invoke();
                _isSecondUpdate = true;
                _isFirstUpdate = false;
            }
        }
    }
}