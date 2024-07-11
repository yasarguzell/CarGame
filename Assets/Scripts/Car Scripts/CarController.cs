using UnityEngine;

namespace CarGame.Car
{
    public class CarController : MonoBehaviour, IHealth
    {
        [SerializeField] private int _initialHealth = 3;
        [SerializeField] private float _protectionTimeInterval = 3;
        [SerializeField] private bool _isDamagable = true;
        private int _health;

        private void Awake()
        {
            _health = _initialHealth;
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground") && _isDamagable)
            {
                DecreaseHealth();
                _isDamagable = false;
                Invoke("EndProtection", _protectionTimeInterval);
            }
        }

        private void DecreaseHealth()
        {
            CoreUISignals.Instance.onGameSetHpBarUpdate?.Invoke((byte)_health);
            TakeDamage(1);
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
            CheckHealth();
        }
        public void CheckHealth()
        {
            if (_health > 0)
                return;
            CoreGameSignals.Instance.onLevelFailed?.Invoke();
            Die();

        }
        public void Die()
        {
            print("Dead");
            //Die
        }
    }
}