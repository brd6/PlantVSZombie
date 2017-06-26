using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlantVsZombie
{
    public class BasePlayer : MonoBehaviour
    {
        [SerializeField]
        protected int life = 10;

        [SerializeField]
        private BasePlayerState state;

        protected BasePlayerState CurrentState { get; private set; }

        private void Awake()
        {
            SetPlayerIdle();
        }

#region StateManager
        protected virtual void SetPlayerDead()
        {
            SetState(BasePlayerState.DEAD);
        }

        protected virtual void SetPlayerWalking()
        {
            SetState(BasePlayerState.WALKING);
        }

        protected virtual void SetPlayerNone()
        {
            SetState(BasePlayerState.NONE);
        }

        protected virtual void SetPlayerIdle()
        {
            SetState(BasePlayerState.IDLE);
        }

        protected virtual void SetPlayerAttacking()
        {
            SetState(BasePlayerState.ATTACKING);
        }

        private void SetState(BasePlayerState _state)
        {
            CurrentState = _state;
            state = _state;
        }

        #endregion
    }
}