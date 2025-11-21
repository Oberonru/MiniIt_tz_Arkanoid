using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Platform.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private PlatformInstance _platform;
        public IObservable<Unit> OnTouch => _onTouch;
        private Subject<Unit> _onTouch = new();

        private PlayerInput _input;
        private Rigidbody2D _rigidbody;
        private Vector2 _inputVector;
        private bool _isTouched;
        private Vector2 _touchOffset;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnValidate()
        {
            if (_platform == null) 
                _platform = GetComponent<PlatformInstance>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnMove(InputValue value)
        {
            _inputVector = value.Get<Vector2>();
        }

        private void OnTouchPress(InputValue value)
        {
            _isTouched = value.isPressed;

            if (_isTouched)
            {
                var screenPos = Pointer.current.position.ReadValue();
                var worldPos = (Vector2)Camera.main.ScreenToWorldPoint(
                    new Vector3(screenPos.x, screenPos.y, 0f));

                if (_platform.GetComponent<Collider2D>().OverlapPoint(worldPos))
                {
                    _touchOffset = (Vector2)_platform.transform.position - worldPos;
                    _onTouch?.OnNext(Unit.Default);
                }
                else
                {
                    _isTouched = false;
                }
            }
        }

        private void Move()
        {
            if (!_isTouched) return;

            var screenPos = Pointer.current.position.ReadValue();
            var worldPos = (Vector2)Camera.main.ScreenToWorldPoint(
                new Vector3(screenPos.x, screenPos.y, 0f));
          
            var target = new Vector2(worldPos.x + _touchOffset.x, _rigidbody.position.y);

            _rigidbody.MovePosition(target);
        }
    }
}
