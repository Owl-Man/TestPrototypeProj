namespace Enemies
{
    public class Plane : EnemyBase {}
}


/*public class Plane : MonoBehaviour
{
    private Transform _centerTransform;
    
    private Rigidbody _rb;
   
    private float _orbitRadius = 20;
    private float _angularSpeed = 0.3f; // Degrees per second
    private float _currentAngle;

    private float _YPosition = 10;

    private ObjectPool<Plane> _pool;

    private EventHandler _eventHandler;
    
    public void Initialize(ObjectPool<Plane> pool, EventHandler eventHandler, Transform turretTransform)
    {
        _pool = pool;
        _eventHandler = eventHandler;
        _rb = GetComponent<Rigidbody>();
        _centerTransform = turretTransform.transform;
    }

    public void OnSpawn()
    {
        _rb.linearVelocity = Vector3.zero;
        MoveInCircle();
    }

    private void MoveInCircle()
    {
        Debug.Log(_centerTransform);
        float x = _centerTransform.position.x + Mathf.Cos(_currentAngle) * _orbitRadius + 20;
        float y = _centerTransform.position.y + _YPosition;
        float z = _centerTransform.position.z + Mathf.Sin(_currentAngle) * _orbitRadius + 20;
        
        transform.position = new Vector3(x, y, z);
        
        _currentAngle += _angularSpeed * Time.deltaTime;
        
        // Calculate the tangent (direction of movement) for rotation
        float nextAngle = _currentAngle + _angularSpeed * Time.fixedDeltaTime;
        Vector3 nextPosition = new Vector3(
            _centerTransform.position.x + Mathf.Cos(nextAngle) * _orbitRadius,
            y,
            _centerTransform.position.z + Mathf.Sin(nextAngle) * _orbitRadius
        );
        Vector3 movementDirection = (nextPosition - transform.position).normalized;

        // Set rotation to face the movement direction
        if (movementDirection.magnitude > 0.1f) // Avoid division by zero or small vectors
        {
            transform.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }

        // Update angle for the next frame
        _currentAngle += _angularSpeed * Time.fixedDeltaTime;

        // Loop the angle to keep it within a reasonable range
        if (_currentAngle >= 360f) _currentAngle -= 360f;
    }
    
    private void FixedUpdate()
    {
        MoveInCircle();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _eventHandler.DefeatEnemy();
            _pool.Release(this);
        }
    }
}*/