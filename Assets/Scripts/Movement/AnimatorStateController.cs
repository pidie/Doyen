using System;
using UnityEngine;

namespace Movement
{
    public class AnimatorStateController : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
        private readonly int _isRunningHash = Animator.StringToHash("isRunning");
        
        public static Action<bool> OnPlayerWalk;
        public static Action<bool> OnPlayerRun;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            OnPlayerWalk += SetPlayerWalkAnimation;
            OnPlayerRun += SetPlayerRunAnimation;
        }

        private void SetPlayerWalkAnimation(bool value) => _animator.SetBool(_isWalkingHash, value);

        private void SetPlayerRunAnimation(bool value)
        {
            if (_animator.GetBool(_isWalkingHash))
                _animator.SetBool(_isWalkingHash, false);
            _animator.SetBool(_isRunningHash, value);
        }
    }
}
