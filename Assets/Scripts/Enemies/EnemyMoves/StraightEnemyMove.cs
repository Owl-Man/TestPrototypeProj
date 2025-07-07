using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Enemies.EnemyMoves
{
    public class StraightEnemyMove : EnemyMoveBase
    {
        private float _bankAngle = 20f;
        private float _bankSpeed = 2f;
        private float _currentBankAngle;
        
        private float rotationSpeed = 5f;
        
        protected override void Move()
        {
            Vector3 targetPosition = MovePoints[PointsIndex].position;
            float distanceToWaypoint = Vector3.Distance(transform.position, targetPosition);
            
            Debug.Log($"Current Point: {PointsIndex}, Target: {targetPosition}, Distance: {distanceToWaypoint}, Parent Pos: {transform.position}, Model Pos: {Rb.transform.position}");

            if (distanceToWaypoint < 1f)
            {
                PointsIndex = Random.Range(0, MovePoints.Length);
            }
            
            MoveToNextWaypoint();
        }
        
        private void MoveToNextWaypoint()
        {
            Vector3 targetPosition = MovePoints[PointsIndex].position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Rb.linearVelocity = moveDirection * moveSpeed;
            
            float targetBankAngle = 0f;
            if (PointsIndex > 0 || PointsIndex < MovePoints.Length - 1)
            {
                Vector3 prevDirection = (PointsIndex == 0) ? moveDirection : (MovePoints[PointsIndex - 1].position - transform.position).normalized;
                Vector3 nextDirection = (PointsIndex == MovePoints.Length - 1) ? moveDirection : (MovePoints[PointsIndex + 1].position - transform.position).normalized;
                float angle = Vector3.Angle(prevDirection, nextDirection);
                float turnIntensity = Mathf.Clamp01(angle / 90f);

                Vector3 cross = Vector3.Cross(prevDirection, nextDirection);
                float bankDirection = Mathf.Sign(cross.y);
                targetBankAngle = bankDirection * _bankAngle * turnIntensity;
            }

            _currentBankAngle = Mathf.Lerp(_currentBankAngle, targetBankAngle, _bankSpeed * Time.fixedDeltaTime);

            Quaternion baseRotation = (moveDirection.magnitude > 0.1f) ? Quaternion.LookRotation(moveDirection, Vector3.up) : transform.rotation;
            Quaternion bankRotation = Quaternion.Euler(0f, 0f, _currentBankAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * bankRotation, rotationSpeed * Time.fixedDeltaTime);

            if (moveDirection.magnitude > 0.1f) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.fixedDeltaTime);
            }
        }
    }
}
