using UnityEngine;
using Zenject;

namespace Player
{
    public class ShootingController : IFixedTickable
    {
        [Inject(Id = "gunpointTransform")]
        private Transform _gunpointTransform;
    
        [Inject(Id = "bulletPrefab")]
        private GameObject _bulletPrefab;
    
        private readonly float _cooldown = 0.1f;
    
        private float _currentCooldown;
    
        private InputSystem_Actions _actions;

        [Inject]
        public void Construct(InputSystem_Actions actions)
        {
            _actions = actions;
        
            actions.Player.PrimaryTouch.started += ctx =>
            {
                Shoot();
            };

            actions.Enable();
        }
    
        private Bullet CreateBullet()
        {
            GameObject bulletObj = Object.Instantiate(_bulletPrefab, _gunpointTransform.position, _gunpointTransform.rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
        
            return bullet;
        }

        public void FixedTick()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.fixedDeltaTime;
            }
        
            if (_actions.Player.PrimaryTouch.IsPressed())
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (_currentCooldown > 0) return;
        
            Bullet bullet = CreateBullet();
            bullet.gameObject.SetActive(true);
        
            bullet.transform.localPosition = _gunpointTransform.position;
            bullet.transform.localRotation = _gunpointTransform.rotation;
        
            _currentCooldown = _cooldown;
        }
    }
}