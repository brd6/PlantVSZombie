using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.UI;
using System;

namespace PlantVsZombie
{

    public class GamePlayMenuManager : MonoBehaviour
    {
        private PlantVsZombieGameManager gameManager;

        [SerializeField]
        private Text currentMoneyText;


        private void Start()
        {
            gameManager = ((PlantVsZombieGameManager)(PlantVsZombieGameManager.Instance));
            GameStateManager.StateChangedEvent += StateChanged;
            PlantVsZombieGameManager.MoneyChangedEvent += MoneyChangedEvent;
            //Score.ScoreChangedEvent += MoneyChanged;
        }

        private void MoneyChangedEvent(int money)
        {
            UpdateMoneyTexts();
        }

        private void StateChanged(GameState newState)
        {
            if (newState == GameState.RUNNING)
            {
                UpdateMoneyTexts();
            }
        }

        private void UpdateMoneyTexts()
        {
            currentMoneyText.text = gameManager.GetMoney().ToString();
            Debug.Log("UpdateMoneyTexts: " + currentMoneyText.text);
        }

        public void PauseButtonAction()
        {
            //if (!popupBG.activeSelf)
            //{
            //    popupBG.SetActive(true);
            //    gameManager.SetGamePause();
            //}
        }

        public void ContinueButtonAction()
        {
            //gameManager.SetGameRunning();
            //ClosePopupAction(popupPause);
            //ClosePopupAction(popupWin);
        }

        public void RestartButtonAction()
        {
            gameManager.RestartGame();
        }

        public void QuitButtonAction()
        {
            gameManager.QuitGame();
        }

        public void ClosePopupAction(GameObject popup)
        {
            //popupBG.SetActive(false);
            //popup.SetActive(false);
        }

        public void OnDestroy()
        {
            //Score.ScoreChangedEvent -= ScoreChanged;
            GameStateManager.StateChangedEvent -= StateChanged;
            PlantVsZombieGameManager.MoneyChangedEvent -= MoneyChangedEvent;

        }
    }
}