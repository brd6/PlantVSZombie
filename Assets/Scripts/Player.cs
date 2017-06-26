using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{
    public class Player : BasePlayer
    {
        [SerializeField]
        private float visionLength = 0.57f;

        [SerializeField]
        private LayerMask raycastLayer;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float minTimeProjection = 3.0f;

        [SerializeField]
        private float maxTimeProjection = 7.0f;

        [SerializeField]
        private GameObject projectilePrefab;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private bool isSpawning = false;

        // Use this for initialization
        void Start()
        {
            SetPlayerIdle();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit2D hit2d;

            hit2d = Physics2D.Raycast(transform.position, Vector2.right, visionLength, raycastLayer.value);
            Debug.DrawRay(transform.position, Vector3.right * visionLength, Color.green);
            if (hit2d)
            {
                ShootEnemy();
            }

            //if (CurrentState == BasePlayerState.IDLE)
            //{
            //    //var rayCastStartPosition = transform.position;
            //    //rayCastStartPosition.x -= 25;
            //    hit2d = Physics2D.Raycast(transform.position, Vector2.right, visionLength, raycastLayer.value);
            //    Debug.DrawRay(transform.position, Vector3.right * visionLength, Color.green);
            //    if (hit2d)
            //    {
            //        Debug.Log("hit2d: " + hit2d.collider.tag + " | " + hit2d.collider.name + " | me: " + name);
            //        SetPlayerAttacking();
            //    }
            //}
            //if (CurrentState == BasePlayerState.ATTACKING)
            //{
            //    hit2d = Physics2D.Raycast(transform.position, Vector2.right, visionLength, raycastLayer.value);
            //    Debug.DrawRay(transform.position, Vector3.right * visionLength, Color.green);
            //    if (!hit2d)
            //    {
            //        SetPlayerIdle();
            //    }
            //    else if (!isSpawning)
            //    {
            //        ShootEnemy();
            //    }
            //}
        }

        private void ShootEnemy()
        {
            if (isSpawning)
                return;
            SetPlayerAttacking();
            isSpawning = true;
            StartCoroutine(SpawnProjectile(Random.Range(minTimeProjection, maxTimeProjection)));
        }

        IEnumerator SpawnProjectile(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            isSpawning = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            isSpawning = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            Debug.Log("Player - OnCollisionStay2D : " + collision.collider.tag);
            if (collision.collider.tag == "Zombie")
            {
                LoseLife();
            }
        }


        protected override void LoseLife()
        {
            base.LoseLife();
            BlinkGameObject(0.03f);
            //ShootEnemy();
        }

        void BlinkGameObject(float durationInSecond)
        {
            StartCoroutine(DoBlinks(durationInSecond, 0.06f));
        }

        IEnumerator DoBlinks(float durationInSecond, float blinkTime)
        {
            while (durationInSecond > 0f)
            {
                durationInSecond -= Time.deltaTime;
                spriteRenderer.color = new Color(0.214f, 0.154f, 0.154f);
                yield return new WaitForSeconds(blinkTime / 2);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(blinkTime);
            }
            spriteRenderer.color = Color.white;
        }

        protected override void SetPlayerDead()
        {
            base.SetPlayerDead();
            Destroy(gameObject);
        }

    }
}