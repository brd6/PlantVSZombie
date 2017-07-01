using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        private Button buttonRefered;

        [SerializeField]
        private float minTimeMoneyGenerator = 6;

        [SerializeField]
        private float maxTimeMoneyGenerator = 10;

        [SerializeField]
        private int moneyIncreaseAmont = 5;

        [SerializeField]
        private GameObject moneyIconPrefab;

        private bool isGeneratedMoney = false;

        private PlantVsZombieGameManager gameManager;


        // Use this for initialization
        void Start()
        {
            gameManager = ((PlantVsZombieGameManager)(PlantVsZombieGameManager.Instance));

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

            GenerateMoney();

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
                if (Random.Range(0, 2) % 2 == 0)
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
            buttonRefered.interactable = true;
            Destroy(gameObject);
        }

        public void SetButtonRefered(Button button)
        {
            buttonRefered = button;
        }

        private void GenerateMoney()
        {
            if (isGeneratedMoney)
                return;
            isGeneratedMoney = true;
            StartCoroutine(MoneyGeneratorCoroutine(Random.Range(minTimeMoneyGenerator, maxTimeMoneyGenerator)));
        }

        IEnumerator MoneyGeneratorCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            var moneyIcon = Instantiate(moneyIconPrefab, transform.position, transform.rotation);
            iTween.MoveTo(moneyIcon, iTween.Hash(
                "position", gameManager.GetMoneyUI().transform.position,
                "time", 0.8f,
                "easetype", iTween.EaseType.easeOutQuad,
                "oncomplete", "DestroyIcon",
                "oncompletetarget", moneyIcon.gameObject,
                "oncompleteparams", moneyIncreaseAmont
                ));
            isGeneratedMoney = false;
        }

    }
}