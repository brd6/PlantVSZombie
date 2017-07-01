using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{

    public class EndGameTrigger : MonoBehaviour
    {
        [SerializeField]
        private PlantVsZombieGameManager gameManager;

        private void Awake()
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Zombie")
                gameManager.SetGameover();
        }

    }
}