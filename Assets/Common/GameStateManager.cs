using UnityEngine;
using System.Collections;

namespace Common
{
    public sealed class GameStateManager : MonoBehaviour
    {
        [SerializeField]
        private GameState state;

        public static GameState CurrentState { get; private set; }
        public static GameState PreviousState { get; private set; }
        public delegate void StateChangedDelegate(GameState newState);
        public static event StateChangedDelegate StateChangedEvent;

        public void SetState(GameState newState)
        {
            PreviousState = CurrentState;
            CurrentState = newState;
            state = CurrentState;
            InvokeStateChangedEvent(newState);
        }

        private void InvokeStateChangedEvent(GameState newState)
        {
            if (StateChangedEvent != null)
            {
                StateChangedEvent(newState);
            }
        }

    }
}