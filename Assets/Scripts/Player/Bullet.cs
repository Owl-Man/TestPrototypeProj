using System.Collections;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        private readonly float _lifetime = 5;
    
        private readonly float _speed = 40;
        
        private Rigidbody _rb;
    
        public void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.linearVelocity = transform.forward * _speed;
            
            StartCoroutine(DestroyAfter());
        }


        private IEnumerator DestroyAfter()
        {
            yield return new WaitForSeconds(_lifetime);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("hitted");
            //Destroy(gameObject);
        }
    }
}