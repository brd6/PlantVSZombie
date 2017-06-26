using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Common;
using System.Collections.Generic;
using System;

namespace PlantVsZombie
{
    public class PlantVsZombieGameManager : GameManager
    {
        [SerializeField]
        private string mainMenuSceneName = "MainMenu";

        [SerializeField]
        private string gamePlaySceneName = "GamePlay";

        protected override void Start()
        {
            base.Start();
        }

        public override void SetGameMenu()
        {
            base.SetGameMenu();
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name != mainMenuSceneName)
            {
                if (SceneManager.GetSceneByName(mainMenuSceneName).IsValid())
                    SceneManager.LoadScene(mainMenuSceneName);
                else
                    SetGameRunning();
            }
        }

        public override void SetGameLauncher()
        {
            base.SetGameLauncher();
            SceneManager.LoadScene(gamePlaySceneName);
            SetGameRunning();
        }

        public override void SetGameRunning()
        {
            base.SetGameRunning();
        }

        public void QuitGame()
        {
            if (GameStateManager.CurrentState != GameState.MENU)
                SetGameMenu();
            else
                Application.Quit();
        }

        public void RestartGame()
        {
            SetGameRunning();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        protected virtual void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }

    }
}