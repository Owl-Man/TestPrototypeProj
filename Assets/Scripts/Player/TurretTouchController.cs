using UnityEngine;
using Zenject;

namespace Player
{
    public class TurretTouchController : ITickable
    {
        [Inject(Id = "turretTransform")] 
        private Transform _turretTransform;
    
        [Inject(Id = "turretGunTransform")] 
        private Transform _turretGunTransform;
    
        private InputSystem_Actions _actions;
    
        private Vector2 _touchStartPos, _movementVelocity;
        private bool _isDragging;
    
        private readonly float _rotationSpeed = 20;
    
        private readonly float _friction = 0.9f;

        [Inject]
        public void Construct(InputSystem_Actions actions)
        {
            _actions = actions;
        
            actions.Player.PrimaryTouch.started += ctx =>
            {
                _isDragging = true;
                _movementVelocity = Vector2.zero;
                _touchStartPos = actions.Player.PrimaryTouchPosition.ReadValue<Vector2>();
            };
        
            actions.Player.PrimaryTouch.canceled += ctx => _isDragging = false;

            actions.Enable();
        }

        public void Tick()
        {
            if (_isDragging)
            {
                Vector2 currentTouchPos = _actions.Player.PrimaryTouchPosition.ReadValue<Vector2>();
                Vector2 touchDelta = currentTouchPos - _touchStartPos;
            
                float yaw = touchDelta.x * _rotationSpeed * Time.deltaTime;
                _turretTransform.Rotate(0, yaw, 0);

                float pitch = -touchDelta.y * _rotationSpeed * Time.deltaTime;
            
                Vector3 gunpointEuler = _turretGunTransform.localEulerAngles;
                float currentPitch = Mathf.Repeat(gunpointEuler.x + 180f, 360f) - 180f;
                float newPitch = currentPitch + pitch;
                newPitch = Mathf.Clamp(newPitch, -30f, 30f);
                float clampedPitchDelta = newPitch - currentPitch;
            
                _turretGunTransform.Rotate(clampedPitchDelta, 0, 0, Space.Self);

                _movementVelocity = new Vector2(yaw, pitch) * _rotationSpeed;

                if (_movementVelocity.magnitude > 0.01f && !_actions.Player.PrimaryTouch.IsPressed())
                {
                    _movementVelocity *= _friction;
                    _turretTransform.Rotate(0, _movementVelocity.x * Time.deltaTime, 0);
                    _turretGunTransform.Rotate(_movementVelocity.y * Time.deltaTime, 0, 0, Space.Self);
                }
                else if (_movementVelocity.magnitude <= 0.01f)
                {
                    _movementVelocity = Vector2.zero;
                }

                _touchStartPos = currentTouchPos;
            }
        }
    }
}