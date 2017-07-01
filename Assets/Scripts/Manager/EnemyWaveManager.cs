using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlantVsZombie
{

    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> enemyWavePrefabs;

        [SerializeField]
        private float minTimeBetweenWave = 7;

        [SerializeField]
        private float maxTimeBetweenWave = 12;

        [SerializeField]
        private Transform waveStart;

        private bool isWaveSended;

        private PlantVsZombieGameManager gameManager;

        // Use this for initialization
        void Start()
        {
            gameManager = ((PlantVsZombieGameManager)(PlantVsZombieGameManager.Instance));
            GameStateManager.StateChangedEvent += StateChanged;
        }

        private void StateChanged(GameState newState)
        {
            if (newState != GameState.RUNNING)
            {
                StopCoroutine("WaveGeneratorCoroutine");
            }
        }

        // Update is called once per frame
        void Update()
        {
            SendEnemyWave();
        }

        private void SendEnemyWave()
        {
            if (!CanSendNextWave())
                return;
            isWaveSended = true;
            StartCoroutine(WaveGeneratorCoroutine(UnityEngine.Random.Range(minTimeBetweenWave, maxTimeBetweenWave)));
        }

        IEnumerator WaveGeneratorCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            var enemyWave = enemyWavePrefabs[UnityEngine.Random.Range(0, enemyWavePrefabs.Count - 1)];
            var moneyIcon = Instantiate(enemyWave, waveStart.position, waveStart.rotation);
            isWaveSended = false;
        }

        bool CanSendNextWave()
        {
            return (!isWaveSended && GameStateManager.CurrentState == GameState.RUNNING);
        }

        private void OnDestroy()
        {
            GameStateManager.StateChangedEvent -= StateChanged;
        }
    }
}