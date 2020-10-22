using LobbyTest;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyTest
{
    public class RoomPlayerMenu : MonoBehaviour
    {
        public TMP_Text playerName;
        public TMP_Text readyText;
		public Button readyButton;

        public LobbyPlayer playerObj { get; private set; }

		private void OnEnable()
		{
			readyButton.gameObject.SetActive(false);
			UpdateInfo();
		}

		public void SetOwner(LobbyPlayer player)
		{
            playerObj = player;
			player.infoChanged += UpdateInfo;

			readyButton.gameObject.SetActive(playerObj.hasAuthority);
		}

		private void OnDestroy()
		{
			if (playerObj != null)
			{
				playerObj.infoChanged -= UpdateInfo;
			}
		}

		private void OnPlayer_playerDisconnected()
		{
			playerObj = null;
			UpdateInfo();
		}

		public void UpdateInfo()
		{
			if (playerObj == null)
			{
				playerName.text = "Waiting\nfor\nplayer...";
				readyText.text = "Not ready";
				readyButton.image.color = Color.white;
				return;
			}

			playerName.text = playerObj.playerName;
			readyText.text = playerObj.readyToBegin ? "Ready" : "Not ready";
			readyButton.image.color = playerObj.readyToBegin ? Color.green : Color.white;
		}

		public void ToggleReadyState()
		{
			playerObj.ToggleReady();
		}
	}
}