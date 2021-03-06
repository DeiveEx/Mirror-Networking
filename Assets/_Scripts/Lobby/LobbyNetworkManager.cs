﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace LobbyTest
{
	public class LobbyNetworkManager : NetworkRoomManager
	{
		//These events will only be called on the server
		public event Action playerJoined;
		public event Action playerLeft;
		public event Action<bool> playersReady;

		#region Monobehavior

		#endregion

		#region Server
		public override void OnRoomServerConnect(NetworkConnection conn)
		{
			Debug.Log("A client connected");
		}

		public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnection conn)
		{
			GameObject roomPlayer = Instantiate(roomPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);
			return roomPlayer;
		}

		public override void OnRoomServerDisconnect(NetworkConnection conn)
		{
			Debug.Log("A client disconnected");
			playerLeft?.Invoke();
		}

		public override void OnRoomServerPlayersReady()
		{
			Debug.Log("All players ready!");
			playersReady?.Invoke(true);
		}

		public override void OnRoomServerPlayersNotReady()
		{
			Debug.Log("Players not ready");
			playersReady?.Invoke(false);
		}

		public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
		{
			GamePlayer gp = gamePlayer.GetComponent<GamePlayer>();

			if (!gamePlayer)
			{
				Debug.LogError("Could not create game player!");
				return false;
			}

			gp.playerName = roomPlayer.GetComponent<LobbyPlayer>().playerName;

			return true;
		}

		public void ClientJoined()
		{
			playerJoined?.Invoke();
		}
		#endregion

		#region Client
		public override void OnRoomStartClient()
		{
			ChangePanel(MenuManager.PanelType.Loading);
		}
		public override void OnRoomStopClient()
		{
			ChangePanel(MenuManager.PanelType.HostOrJoin);
		}

		////This will be called on the client when it successfully connects to the server (dont forget that the host is also a client)
		//public override void OnRoomClientConnect(NetworkConnection conn)
		//{
		//	ChangePanel(MenuManager.PanelType.Room);
		//}
		#endregion

		#region Misc
		private void ChangePanel(MenuManager.PanelType panelType)
		{
			//Check if we're in the scene with the room
			if (!IsSceneActive(RoomScene))
			{
				return;
			}

			MenuManager.instance.ShowPanel(panelType);
		}
		#endregion
	}
}