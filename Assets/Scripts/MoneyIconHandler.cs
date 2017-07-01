using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{

    public class MoneyIconHandler : MonoBehaviour
    {
        private PlantVsZombieGameManager gameManager;

        // Use this for initialization
        void Start()
        {
            gameManager = ((PlantVsZombieGameManager)(PlantVsZombieGameManager.Instance));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DestroyIcon(int moneyIncreaseAmont)
        {
            gameManager.IncreaseMoney(moneyIncreaseAmont);
            Destroy(gameObject);
        }


    }
}