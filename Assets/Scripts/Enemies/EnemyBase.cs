using UnityEngine;

namespace Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        public Transform TurretTransform { get; private set; }
        public Transform[] MovePoints { get; private set; }

        private float _YPosition = 10;
        public bool IsFalling { get; private set; }
        public bool IsDestroyed { get; private set; }
        
        private EventHandler _eventHandler;
        private GameObject _enemiesExplosionEffectPrefab;
        
        public Rigidbody rb;
    
        public void Initialize(EventHandler eventHandler, Transform turretTransform, Transform[] movePoints, GameObject enemiesExplosionEffectPrefab)
        {
            _eventHandler = eventHandler;
        
            TurretTransform = turretTransform.transform;
            MovePoints = movePoints;

            _enemiesExplosionEffectPrefab = enemiesExplosionEffectPrefab;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground") && !IsDestroyed)
            {
                DestroyEnemy();
            }
            else if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                IsFalling = true;
                rb.useGravity = true; 
                rb.linearVelocity = new Vector3(5f, -5f, rb.linearVelocity.z); 
                rb.freezeRotation = false;
            }
        }

        private void DestroyEnemy()
        {
            IsDestroyed = true;
            _eventHandler.DefeatEnemy();
                
            Instantiate(_enemiesExplosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}