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

        private void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {
            SetPlayerWalking();
        }

        private void Update()
        {
            RaycastHit2D hit;
            RaycastHit2D hit2d;

            if (CurrentState == BasePlayerState.WALKING)
            {
                hit2d = Physics2D.Raycast(transform.position, Vector2.left, visionLength, raycastLayer.value);
                Debug.DrawRay(transform.position, Vector3.left * visionLength, Color.green);
                if (hit2d)
                {
                    Debug.Log("hit2d: " + hit2d.collider.name);
                    SetPlayerAttacking();
                }
            }
        }

        protected override void SetPlayerWalking()
        {
            base.SetPlayerWalking();
            animator.SetBool("Walking", true);
        }

        protected override void SetPlayerAttacking()
        {
            base.SetPlayerAttacking();
            animator.SetBool("Walking", false);
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
    }
}