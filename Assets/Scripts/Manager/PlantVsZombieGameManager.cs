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

        [SerializeField]
        private int money = 200;

        [SerializeField]
        private GameObject moneyUI;

        public delegate void MoneyChangedDelegate(int money);
        public static event MoneyChangedDelegate MoneyChangedEvent;

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
            Time.timeScale = 1;
            SceneManager.LoadScene(gamePlaySceneName);
            SetGameRunning();
        }

        public override void SetGameRunning()
        {
            base.SetGameRunning();
        }

        public override void SetGameover()
        {
            base.SetGameover();
            Time.timeScale = 0;
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

        public int GetMoney()
        {
            return money;
        }

        public void DecreaseMoney(int amont)
        {
            money -= amont;
            InvokeMoneyChangedEvent(money);
        }

        public void IncreaseMoney(int amont)
        {
            money += amont;
            InvokeMoneyChangedEvent(money);
        }

        private void InvokeMoneyChangedEvent(int money)
        {
            if (MoneyChangedEvent != null)
            {
                MoneyChangedEvent(money);
            }
        }

        public GameObject GetMoneyUI()
        {
            return moneyUI;
        }

    }
}