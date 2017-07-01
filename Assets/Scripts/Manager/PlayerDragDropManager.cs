using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlantVsZombie
{
    public class PlayerDragDropManager : MonoBehaviour {

        [SerializeField]
        private List<GameObject> playerPrefabs;

        [SerializeField]
        private PlantVsZombieGameManager gameManager;

        [SerializeField]
        private GameObject playersGroup;

        private int currentPlayerTypeSelected;

        // Use this for initialization
        void Start()
        {
            currentPlayerTypeSelected = -1;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectPlayerType(int index)
        {
            currentPlayerTypeSelected = index;
        }

        public void AddPlayerOnTile(GameObject buttonRefered)
        {
            if (currentPlayerTypeSelected < 0)
                return;
            if (gameManager.GetMoney() - 50 < 0)
                return;
            var player = Instantiate(playerPrefabs[currentPlayerTypeSelected], playersGroup.transform);
            var button = buttonRefered.GetComponent<Button>();
            player.transform.position = button.transform.position;
            player.GetComponent<Player>().SetButtonRefered(button);
            button.interactable = false;
            gameManager.DecreaseMoney(50);
            if (gameManager.GetMoney() - 50 < 0)
                button.interactable = false;
            currentPlayerTypeSelected = -1;
        }

    }
}