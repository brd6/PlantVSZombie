using UnityEngine;
using System.Collections;

namespace Common
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameStateManager GameStateManager { get; private set; }

        public SoundManager SoundManager { get; private set; }

        protected GameManager()
        {
            
        }

        private void Awake()
        {
            MakeSingletonInstance();
        }

        protected virtual void Start()
        {
            TryInitManagers();
            SetGameMenu();
        }

        private void TryInitManagers()
        {
            GameStateManager = gameObject.GetComponent<GameStateManager>();
            SoundManager = gameObject.GetComponent<SoundManager>();
            if (GameStateManager == null || 
                SoundManager == null)
            {
                Debug.LogError("Init Manager fail !");
            }
        }

        #region Singleton
        private void MakeSingletonInstance()
        {
            if (Instance != null)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        #endregion

        public virtual void SetGameMenu()
        {
            GameStateManager.SetState(GameState.MENU);
        }

        public virtual void SetGamePause()
        {
            GameStateManager.SetState(GameState.PAUSE);
        }

        public virtual void SetGameRunning()
        {
            GameStateManager.SetState(GameState.RUNNING);
        }

        public virtual void SetGameover()
        {
            GameStateManager.SetState(GameState.GAMEOVER);
        }

        public virtual void SetGameWin()
        {
            GameStateManager.SetState(GameState.WIN);
        }

        public virtual void SetGameLauncher()
        {
            GameStateManager.SetState(GameState.LAUNCHER);
        }
    }
}