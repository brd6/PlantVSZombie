using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Common
{

    public class GUIManager : MonoBehaviour
    {
        [System.Serializable]
        public struct StateToGUI
        {
            public GameState state;
            public GameObject GUI;
        }

        [SerializeField]
        private List<StateToGUI> GameGUI;

        private void Start()
        {
            GameStateManager.StateChangedEvent += ChangeGUI;
        }

        private void ChangeGUI(GameState previousState)
        {
            foreach (StateToGUI gui in GameGUI)
            {
                gui.GUI.SetActive(gui.state == previousState);
            }
        }

        private void OnDestroy()
        {
            GameStateManager.StateChangedEvent -= ChangeGUI;
        }
    }
}