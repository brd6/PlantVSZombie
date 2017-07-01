using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{


    public class RestartGameAction : MonoBehaviour
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

        public void RestartGame()
        {
            Time.timeScale = 1;
            gameManager.RestartGame();
        }

    }
}