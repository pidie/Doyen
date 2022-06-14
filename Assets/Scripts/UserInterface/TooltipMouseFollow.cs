using UnityEngine;

namespace UserInterface
{
    public class TooltipMouseFollow : MonoBehaviour
    {
        private bool _follow;
        private Vector2 _offset;

        private void Update()
        {
            if (_follow)
                transform.position = (Vector2) Input.mousePosition + _offset;
        }

        private void OnEnable()
        {
            _follow = true;
            _offset = TooltipManager.Offset;
        }

        private void OnDisable() => _follow = false;
    }
}