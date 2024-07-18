using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace CarGame.Car
{
    public class CarController : MonoBehaviour, IHealth
    {
        [SerializeField] private int _initialHealth = 2;
        [SerializeField] private float _protectionTimeInterval = 3;
        [SerializeField] private bool _isDamagable = true;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera; // Add a reference to the Cinemachine Virtual Camera

        private Rigidbody _rb;
        private Coroutine _scoreUpdate;
        [SerializeField] private int _health;

        public int Health { get { return _health; } }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _health = _initialHealth;
        }

        private void Start()
        {
            OnLevelInitialized(0);
            CoreGameSignals.Instance.onLevelInitialized += OnLevelInitialized;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.onLevelInitialized -= OnLevelInitialized;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        }

        void Update()
        {
            Debug.Log("Car Health: " + _health);
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground") && _isDamagable)
            {
                DecreaseHealth();
                _isDamagable = false;
                Invoke("EndProtection", _protectionTimeInterval);
            }
            if (other.gameObject.TryGetComponent(out EnemyBase enemy))
            {
                if (this.GetComponent<CarMovementController>()._velocity > 5)
                {
                    enemy.Die();
                }
            }

        }

        private void OnLevelInitialized(byte a)
        {
            _scoreUpdate = StartCoroutine("ScoreUpdate");
        }

        private void OnLevelFailed()
        {
            StopCoroutine(_scoreUpdate);
        }

        private void DecreaseHealth()
        {
            CoreUISignals.Instance.onGameSetHpBarUpdate?.Invoke((byte)(_health - 1));
            TakeDamage(1);
        }

        public void IncreaseHealth()
        {
            _health++;
            CoreUISignals.Instance.onGameSetHpBarUpdate?.Invoke((byte)_health);
        }

        private void EndProtection()
        {
            _isDamagable = true;
        }

        //Implement interface
        public float GetCurrentHealth()
        {
            return _health;
        }
        public void TakeDamage(int amount)
        {
            _health -= amount;
            CameraShake();
            CheckHealth();
        }

        private void CameraShake()
        {
            // Assuming the Cinemachine Virtual Camera is already assigned
            _virtualCamera.m_Lens.Dutch = 0;  // Ensure starting at 0
            _virtualCamera.m_Lens.Dutch = -5;
            _virtualCamera.m_Lens.Dutch = 0;
            _virtualCamera.m_Lens.Dutch = -5;
            _virtualCamera.m_Lens.Dutch = 0;
            DOTween.To(() => _virtualCamera.m_Lens.Dutch, x => _virtualCamera.m_Lens.Dutch = x, -5, 0.1f)
                   .OnComplete(() =>
                       DOTween.To(() => _virtualCamera.m_Lens.Dutch, x => _virtualCamera.m_Lens.Dutch = x, 0, 0.1f)
                   );
        }

        public void CheckHealth()
        {
            //if (_health > 0)
            //return;
            if (_health <= 0)
            {

                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
            Die();

        }
        public void Die()
        {
            print("Dead");
            //Die
        }

        private IEnumerator ScoreUpdate()
        {
            float elapsedTime = 0;
            CoreUISignals.Instance.onGameScoreTextUpdate?.Invoke((int)0);
            while (true)
            {
                if (Mathf.Abs(_rb.velocity.magnitude) > Mathf.Epsilon)
                {
                    elapsedTime += Time.deltaTime;
                }

                if (elapsedTime >= 5)
                {
                    CoreUISignals.Instance.onGameScoreTextUpdate?.Invoke((int)50);
                    elapsedTime = 0;
                }

                yield return null;
            }
        }
    }
}