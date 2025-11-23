using System;
using Core.Bricks;
using Core.Platform;
using Core.Configs.Ball;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Core.Ball.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallController : MonoBehaviour
    {
        [SerializeField] private PlatformInstance _platform;
        [SerializeField] private BallInstance _ball;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Transform _fixedBallPoint;

        public IObservable<Unit> OnPlatformConcern => _concern;
        public Subject<Unit> _concern = new();

        private Rigidbody2D _rigidbody;
        private Vector2 _currentPosition;
        private float _ballRadius;
        private float _platformWidth = 1f;
        private CircleCollider2D _circleCollider;

        private bool _damagedThisFixedStep;

        private const float MinVerticalVelocity = 0.2f;
        private const float HorizontalJitter = 0.1f;

        private BallConfig Config => _ball.Stats;

        private void Awake()
        {
            Config.StateType = BallStateType.Waiting;

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
            _platform.Controller.OnTouch.Take(1).Subscribe(_ => BallMoving()).AddTo(this);
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

        private void FixedUpdate()
        {
            _damagedThisFixedStep = false;
        }

        private async void BallMoving()
        {
            Config.StateType = BallStateType.Moving;

            transform.position = _fixedBallPoint.position + new Vector3(0f, _ballRadius + Config.StartEpsilon, 0f);
            Physics2D.SyncTransforms();

            var targetSpeed = Config.Speed;
            var desiredVertical = Mathf.Min(Config.VerticalVelocity, targetSpeed);

            var vx = Mathf.Sqrt(Mathf.Max(0f, targetSpeed * targetSpeed - desiredVertical * desiredVertical));
            var velocity = new Vector2(vx, desiredVertical);

            _rigidbody.linearVelocity = velocity;
        }

        private void RefreshBallPosition()
        {
            if (Config.StateType == BallStateType.Waiting)
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
                var offset = (_ball.Transform.position.x - _platform.Transform.position.x) / (_platformWidth / 2f);
                offset = Mathf.Clamp(offset, -1f, 1f);

                var angle = offset * Config.MaxAngle * Mathf.Deg2Rad;
                var direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;

                if (_rigidbody.linearVelocity.y < 0f)
                    direction.y = -Mathf.Abs(direction.y);
                else
                    direction.y = Mathf.Abs(direction.y);

                var velocity = direction * Config.Speed;

                if (Mathf.Abs(velocity.y) < MinVerticalVelocity)
                {
                    float currentVy = _rigidbody.linearVelocity.y;
                    velocity.y = Mathf.Sign(currentVy) * MinVerticalVelocity;
                }

                _concern?.OnNext(Unit.Default);
                _rigidbody.linearVelocity = velocity;
                CorrectTrajectoryIfNeeded();
                return;
            }

            if (collision.collider.TryGetComponent(out Brick brick))
            {
                if (!_damagedThisFixedStep)
                {
                    brick.HealthComponent.TakeDamage(1);
                    _damagedThisFixedStep = true;
                }

                var velocity = _rigidbody.linearVelocity;
                if (velocity.y < 0f)
                    velocity.y = -Mathf.Abs(velocity.y);

                _rigidbody.linearVelocity = velocity;
                CorrectTrajectoryIfNeeded();
                return;
            }

            {
                var velocity = _rigidbody.linearVelocity;
                velocity.x += Random.Range(-HorizontalJitter, HorizontalJitter);

                if (velocity.y < 0f)
                    velocity.y = -Mathf.Abs(velocity.y);

                _rigidbody.linearVelocity = velocity;
                CorrectTrajectoryIfNeeded();
            }
        }

        private void CorrectTrajectoryIfNeeded()
        {
            var velocity = _rigidbody.linearVelocity;
            if (velocity == Vector2.zero) return;

            float angle = Mathf.Abs(Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);

            if (angle < 5f || angle > 85f)
            {
                velocity = Quaternion.Euler(0, 0, Random.Range(-7f, 7f)) * velocity;
                _rigidbody.linearVelocity = velocity;
            }
        }
    }
}
