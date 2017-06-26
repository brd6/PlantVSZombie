using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.UI;

namespace PlantVsZombie
{

    public class GamePlayMenuManager : MonoBehaviour
    {
        private PlantVsZombieGameManager gameManager;

        [SerializeField]
        private Text currentScoreText;

        [SerializeField]
        private Text bestScoreText;

        [SerializeField]
        private GameObject popupPause;

        [SerializeField]
        private GameObject popupGameOver;

        [SerializeField]
        private GameObject popupWin;

        [SerializeField]
        private GameObject popupBG;

        private void Start()
        {
            gameManager = ((PlantVsZombieGameManager)(PlantVsZombieGameManager.Instance));
            GameStateManager.StateChangedEvent += StateChanged;
            //Score.ScoreChangedEvent += ScoreChanged;
            UpdateScoreTexts();
        }

        private void StateChanged(GameState newState)
        {
            if (newState == GameState.GAMEOVER)
            {
                popupBG.SetActive(true);
            }
        }

        private void ScoreChanged()
        {
            UpdateScoreTexts();
        }

        private void UpdateScoreTexts()
        {
            //currentScoreText.text = gameManager.score.currentScore.ToString();
            //bestScoreText.text = gameManager.score.bestScore.ToString();
        }

        public void PauseButtonAction()
        {
            if (!popupBG.activeSelf)
            {
                popupBG.SetActive(true);
                gameManager.SetGamePause();
            }
        }

        public void ContinueButtonAction()
        {
            gameManager.SetGameRunning();
            ClosePopupAction(popupPause);
            ClosePopupAction(popupWin);
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
            popupBG.SetActive(false);
            popup.SetActive(false);
        }

        public void OnDestroy()
        {
            //Score.ScoreChangedEvent -= ScoreChanged;
            GameStateManager.StateChangedEvent -= StateChanged;

        }
    }
}