using UnityEngine;

namespace Enemies.EnemyMoves
{
    [RequireComponent(typeof(EnemyBase))]
    public abstract class EnemyMoveBase : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed = 5;
        
        protected Rigidbody Rb;
        protected Transform[] MovePoints;
        protected EnemyBase EnemyBase;
    
        protected int PointsIndex;

        private void Start()
        {
            EnemyBase = GetComponent<EnemyBase>();
            MovePoints = EnemyBase.MovePoints;
            
            Rb = EnemyBase.rb;
        }

        private void FixedUpdate()
        {
            if (!EnemyBase.IsFalling) Move();
        }

        protected abstract void Move();
    }
}