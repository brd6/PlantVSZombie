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

        private BoxCollider2D boxColider2d;

        private void Awake()
        {
            boxColider2d = GetComponent<BoxCollider2D>();
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
            boxColider2d.enabled = true;
        }

        protected override void SetPlayerAttacking()
        {
            base.SetPlayerAttacking();
            animator.SetBool("Walking", false);
            boxColider2d.enabled = false;
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
                Debug.Log("OnCollisionEnter2D: " + collision.collider.tag);
                SetPlayerAttacking();
            }
        }
    }
}