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


        private int _health;

        private void Awake()
        {
            _health = _initialHealth;
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

        private void DecreaseHealth()
        {
            TakeDamage(1);
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
            if (_health > 0)
                return;
            if (_health == -1)
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
    }
}