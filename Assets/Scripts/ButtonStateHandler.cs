using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlantVsZombie
{

    public class ButtonStateHandler : MonoBehaviour
    {
        private Button button;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();
            PlantVsZombieGameManager.MoneyChangedEvent += MoneyChangedEvent;
        }

        private void MoneyChangedEvent(int money)
        {
            if (money >= 50)
            {
                button.interactable = true;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            PlantVsZombieGameManager.MoneyChangedEvent -= MoneyChangedEvent;
        }
    }
}