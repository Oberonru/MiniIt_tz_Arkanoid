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

        [SerializeField] private float _maxAngle = 60f;    
        [SerializeField] private float _startEpsilon = 0.05f; 

        private Rigidbody2D _rigidbody;
        private Vector2 _currentPosition;
        private float _ballRadius;
        private float _platformWidth = 1f;
        private CircleCollider2D _circleCollider;

        private void Awake()
        {
            _ball.Stats.StateType = BallStateType.Waiting;

            _circleCollider = GetComponent<CircleCollider2D>();
            _ballRadius = _circleCollider.radius * transform.localScale.x;

            var capsule = _platform.GetComponent<CapsuleCollider2D>();
            _platformWidth = capsule.bounds.size.x;

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0f;
            _rigidbody.freezeRotation = true;

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

        private async void BallMoving()
        {
            _ball.Stats.StateType = BallStateType.Moving;

            transform.position = _fixedBallPoint.position + new Vector3(0f, _ballRadius + _startEpsilon, 0f);
            Physics2D.SyncTransforms();

            var targetSpeed = _ball.Stats.Speed;
            var desiredVertical = Mathf.Min(_ball.Stats.VerticalVelocity, targetSpeed);

            var vx = Mathf.Sqrt(Mathf.Max(0f, targetSpeed * targetSpeed - desiredVertical * desiredVertical));
            var velocity = new Vector2(vx, desiredVertical);

            _rigidbody.linearVelocity = velocity;
            
        }

        private void RefreshBallPosition()
        {
            if (_ball.Stats.StateType == BallStateType.Waiting)
            {
                _currentPosition = _fixedBallPoint.position + new Vector3(0f, _ballRadius, 0f);
                transform.position = _currentPosition;
                _rigidbody.linearVelocity = Vector2.zero;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlatformInstance platform))
            {
                float offset = (_ball.Transform.position.x - _platform.Transform.position.x) / (_platformWidth / 2f);
                offset = Mathf.Clamp(offset, -1f, 1f);

                float angle = offset * _maxAngle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;

                if (direction.y < 0f) direction.y = -direction.y;

                _rigidbody.linearVelocity = direction * _ball.Stats.Speed;
            }
        }
    }
}
