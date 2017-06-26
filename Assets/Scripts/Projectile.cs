using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private string enemyTag = "Zombie";

        [SerializeField]
        private float moveSpeed = 1f;

        // Use this for initialization
        void Start()
        {

        }

        void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == enemyTag)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == enemyTag)
            {
                Destroy(gameObject);
            }
        }

    }
}