using System;
using Core.Bricks;
using Core.Configs.Ball;
using Core.Platform;
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
        private Subject<Unit> _concern = new();

        private BallStateType _currentState = BallStateType.Waiting;
        private Rigidbody2D _rigidbody;
        private Vector2 _currentPosition;
        private float _ballRadius;
        private float _platformWidth = 1f;
        private CircleCollider2D _circleCollider;

        private bool _damagedThisFixedStep;
        private float _targetSpeed;
        private int _platformCollisionCount;
        private int _brickCollisionCount;

        private BallConfig Config => _ball.Stats;

        private void Awake()
        {
            _targetSpeed = Config.Speed;

            _circleCollider = GetComponent<CircleCollider2D>();
            _ballRadius = _circleCollider.radius * transform.localScale.x;

            var capsule = _platform.GetComponent<CapsuleCollider2D>();
            _platformWidth = capsule.bounds.size.x;

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0f;
            _rigidbody.freezeRotation = true;

            RefreshBallPosition();
        }

        private void OnValidate()
        {
            if (_platform == null) _platform = FindObjectOfType<PlatformInstance>();
            if (_ball == null) _ball = GetComponent<BallInstance>();
        }

        private void OnEnable()
        {
            _platform.Controller.OnTouch.Take(1).
                Subscribe(_ => BallMoving()).AddTo(this);
        }
        
        private void Update()
        {
            RefreshBallPosition();
        }

        private void FixedUpdate()
        {
            _damagedThisFixedStep = false;
        }

        public void Reset()
        {
            _currentState = BallStateType.Waiting;
            ResetPosition();
        }

        private async void BallMoving()
        {
            if (_currentState == BallStateType.Moving) return;
            
            _currentState = BallStateType.Moving;

            transform.position = _fixedBallPoint.position + new Vector3(0f, _ballRadius + Config.StartEpsilon, 0f);
            Physics2D.SyncTransforms();

            var desiredVertical = Mathf.Min(Config.VerticalVelocity, _targetSpeed);
            var vx = Mathf.Sqrt(Mathf.Max(0f, _targetSpeed * _targetSpeed - desiredVertical * desiredVertical));
            var velocity = new Vector2(vx, desiredVertical);

            _rigidbody.linearVelocity = velocity;
        }

        private void RefreshBallPosition()
        {
            if (_currentState == BallStateType.Waiting)
            {
                ResetPosition();
                transform.position = _currentPosition;
            }
        }

        private void ResetPosition()
        {
            _currentPosition = _fixedBallPoint.position + new Vector3(0f, _ballRadius, 0f);
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var velocity = _rigidbody.linearVelocity;

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

                velocity = direction * _targetSpeed;

                _concern?.OnNext(Unit.Default);
                _platformCollisionCount++;
                ChangeSpeed();
            }
            else if (collision.collider.TryGetComponent(out Brick brick))
            {
                if (!_damagedThisFixedStep)
                {
                    brick.HealthComponent.TakeDamage(1);
                    _damagedThisFixedStep = true;
                }

                if (velocity.y < 0f)
                    velocity.y = -Mathf.Abs(velocity.y);

                _brickCollisionCount++;
                ChangeSpeed();
            }
            else
            {
                velocity.x += Random.Range(-Config.HorizontalJitter, Config.HorizontalJitter);
            }

            if (Mathf.Abs(velocity.y) < Config.MinVerticalVelocity)
                velocity.y = Mathf.Sign(velocity.y) * Config.MinVerticalVelocity;

            var angleCheck = Mathf.Abs(Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);
            if (angleCheck < Config.MinAngle || angleCheck > (90f - Config.MinAngle))
                velocity = Quaternion.Euler(0, 0, Random.Range(-7f, 7f)) * velocity;

            _rigidbody.linearVelocity = velocity;
        }

        private void ChangeSpeed()
        {
            if (_platformCollisionCount >= Config.PlatformLoopCount)
            {
                _targetSpeed = Mathf.Min(_targetSpeed + Config.BallAcceleration, Config.MaxSpeed);
                _platformCollisionCount = 0;
            }

            if (_brickCollisionCount >= Config.BrickLoopCount)
            {
                _targetSpeed = Mathf.Min(_targetSpeed + Config.BrickAcceleration, Config.MaxSpeed);
                _brickCollisionCount = 0;
            }
        }

        public void ResetSpeed()
        {
            _targetSpeed = Config.Speed;
        }
    }
}