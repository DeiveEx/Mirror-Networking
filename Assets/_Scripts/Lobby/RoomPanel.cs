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

		public override void OnStartServer()
		{
			base.OnStartServer();
		}

		#region Monobehavior
		private void OnEnable()
		{
			startGameButton.SetActive(false);

			GameManager.instance.lobbyManager.playersReady += OnPlayersReady;
			GameManager.instance.lobbyManager.playerJoined += PairPanels;
			GameManager.instance.lobbyManager.playerLeft += PairPanels;

			PairPanels();
			OnPlayersReady(GameManager.instance.lobbyManager.allPlayersReady);
		}

		private void OnDisable()
		{
			GameManager.instance.lobbyManager.playersReady -= OnPlayersReady;
			GameManager.instance.lobbyManager.playerJoined -= PairPanels;
			GameManager.instance.lobbyManager.playerLeft -= PairPanels;
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

		#region Client
		private void PairPanels()
		{
			StartCoroutine(PairPlayersToPanels_Routine());
		}

		private IEnumerator PairPlayersToPanels_Routine()
		{
			//Wait a frame so the game object for the new player can be created/destroyed
			yield return null;
			Debug.Log($"Pairing panels to {GameManager.instance.lobbyManager.roomSlots.Count} players");

			//Get all lobby players
			var lobbyPlayers = GameManager.instance.lobbyManager.roomSlots.Select(x => x.GetComponent<LobbyPlayer>()).OrderBy(x => x.index).ToArray();

			//Pair each player to a panel
			for (int i = 0; i < playerPanels.Length; i++)
			{
				RoomPlayerMenu panel = playerPanels[i];

				if (i < lobbyPlayers.Length && (panel.playerObj == null || panel.playerObj != lobbyPlayers[i]))
				{
					panel.SetOwner(lobbyPlayers[i]);
				}

				panel.UpdateInfo();
			}
		} 
		#endregion

		#region Misc
		public void LeaveRoom()
		{
			//If we're a host (which means we're both the server AND a client), we stop the host
			if (NetworkServer.active && NetworkClient.isConnected)
			{
				GameManager.instance.lobbyManager.StopHost();
			}
			else
			{
				//In THIS case, we don't have dedicated servers, so if we're not a host, we're definetely a client
				GameManager.instance.lobbyManager.StopClient();
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

		public void GoToLevelSelect()
		{
			MenuManager.instance.ShowPanel(MenuManager.PanelType.LevelSelect);
		}
		#endregion
	}
}