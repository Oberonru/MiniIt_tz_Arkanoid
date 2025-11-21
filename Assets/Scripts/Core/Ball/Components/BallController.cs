using Core.Platform;
using UniRx;
using UnityEngine;

namespace Core.Ball.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallController : MonoBehaviour
    {
        [SerializeField] private PlatformInstance _platform;
        [SerializeField] private BallInstance _ball;
        [SerializeField] private Transform _fixedBallPoint;

        private Rigidbody2D _rigidbody;
        private Vector2 _currentPosition;
        private float _radius;

        private void Awake()
        {
            var circle = GetComponent<CircleCollider2D>();
            _radius = circle.radius * transform.localScale.x;
            
            _ball.Stats.StateType = BallStateType.Waiting;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            RefreshBallPosition();
        }

        private void OnEnable()
        {
            (_platform.Controller.OnTouch.Take(1).Subscribe(_ => BallMoving())).AddTo(this);
        }

        private void OnValidate()
        {
            if (_platform == null) _platform = FindObjectOfType<PlatformInstance>();
            if (_ball == null) _ball = GetComponent<BallInstance>();
        }

        private void Update()
        {
            RefreshBallPosition();
        }
        
        private void BallMoving()
        {
            _ball.Stats.StateType = BallStateType.Moving;

            var direction = new Vector2(0.5f, 1f);
            
            _rigidbody.linearVelocity = direction * _ball.Stats.Speed;
        }

        private void RefreshBallPosition()
        {
            if (_ball.Stats.StateType == BallStateType.Waiting)
            {
                _currentPosition =  _fixedBallPoint.position + new Vector3(0, _radius, 0);
                transform.position = _currentPosition;
            }
        }
    }
}