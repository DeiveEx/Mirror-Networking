using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LobbyTest
{
	public class RoomPanel : NetworkBehaviour
	{
		public GameObject startGameButton;
		public RoomPlayerMenu[] playerPanels;


		#region Monobehavior
		private void OnEnable()
		{
			startGameButton.SetActive(false);

			MenuManager.instance.lobbyManager.playersReady += OnPlayersReady;
		}

		private void OnDisable()
		{
			MenuManager.instance.lobbyManager.playersReady -= OnPlayersReady;
		}
		#endregion

		#region Server
		private void OnPlayersReady(bool ready)
		{
			if (NetworkServer.active && NetworkClient.isConnected)
			{
				startGameButton.SetActive(ready);
			}
		}
		#endregion

		#region Misc
		public void LeaveRoom()
		{
			//If we're a host (which means we're both the server AND a client), we stop the host
			if (NetworkServer.active && NetworkClient.isConnected)
			{
				MenuManager.instance.lobbyManager.StopHost();
			}
			else
			{
				//In THIS case, we don't have dedicated servers, so if we're not a host, we're definetely a client
				MenuManager.instance.lobbyManager.StopClient();
			}
		}

		public RoomPlayerMenu GetFreePanel()
		{
			for (int i = 0; i < playerPanels.Length; i++)
			{
				if (playerPanels[i].playerObj == null)
					return playerPanels[i];
			}

			return null;
		} 
		#endregion
	}
}