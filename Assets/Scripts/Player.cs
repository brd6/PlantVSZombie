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

        // Use this for initialization
        void Start()
        {
            SetPlayerIdle();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit2D hit;
            RaycastHit2D hit2d;

            if (CurrentState == BasePlayerState.IDLE)
            {
                hit2d = Physics2D.Raycast(transform.position, Vector2.right, visionLength, raycastLayer.value);
                Debug.DrawRay(transform.position, Vector3.right * visionLength, Color.green);
                if (hit2d)
                {
                    Debug.Log("hit2d: " + hit2d.collider.tag + " | " + hit2d.collider.name + " | me: " + name);
                    SetPlayerAttacking();
                }
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        }
    }
}