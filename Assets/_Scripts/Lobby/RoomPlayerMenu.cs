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
			OnPlayer_infoChanged();
		}

		public void SetOwner(LobbyPlayer player)
		{
            playerObj = player;
			player.infoChanged += OnPlayer_infoChanged;
			player.playerDisconnected += OnPlayer_playerDisconnected;

			readyButton.gameObject.SetActive(playerObj.hasAuthority);
		}

		private void OnPlayer_playerDisconnected()
		{
			playerObj = null;
			OnPlayer_infoChanged();
		}

		private void OnPlayer_infoChanged()
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