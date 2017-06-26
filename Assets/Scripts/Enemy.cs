using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{
    public class Enemy : BasePlayer
    {
        [SerializeField]
        private float moveSpeed = 0.38f;

        [SerializeField]
        private float visionLength = 0.57f;

        [SerializeField]
        private LayerMask raycastLayer;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private BoxCollider2D boxColider2d;

        private Rigidbody2D rigidBody2D;


        private void Awake()
        {
            boxColider2d = GetComponent<BoxCollider2D>();
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            SetPlayerWalking();
        }

        private void Update()
        {
        }

        protected override void SetPlayerWalking()
        {
            base.SetPlayerWalking();
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
        }

        protected override void SetPlayerAttacking()
        {
            base.SetPlayerAttacking();
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", true);
        }

        protected override void SetPlayerIdle()
        {
            base.SetPlayerIdle();
            animator.SetBool("Walking", false);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (CurrentState == BasePlayerState.WALKING)
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                SetPlayerAttacking();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                SetPlayerWalking();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Projectile")
            {
                LoseLife();
            }
        }

        protected override void LoseLife()
        {
            base.LoseLife();
            BlinkGameObject(0.03f);
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