using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Mirror;

namespace LobbyTest
{
	public class LevelManager : Singleton<LevelManager>
	{
		public Button backToRoomButton;

		private void OnEnable()
		{
			backToRoomButton.gameObject.SetActive(NetworkServer.active && NetworkClient.isConnected);
		}

		public void BackToLobby()
		{
			GameManager.instance.lobbyManager.ServerChangeScene(GameManager.instance.lobbyManager.RoomScene);
		}
	}

}