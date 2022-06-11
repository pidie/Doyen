using UnityEngine;

namespace UserInterface
{
    public class TooltipMouseFollow : MonoBehaviour
    {
        private bool _follow;
        private float _offset;

        private void Update()
        {
            if (_follow)
                transform.position = new Vector2(Input.mousePosition.x + _offset, Input.mousePosition.y);
        }

        private void OnEnable()
        {
            _follow = true;
            _offset = TooltipManager.Offset;
        }

        private void OnDisable() => _follow = false;
    }
}