using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

namespace LobbyTest
{
	public class LobbyPlayer : NetworkRoomPlayer
	{
		[SyncVar(hook = nameof(OnPlayerNameChanged))]
		public string playerName;

		public event Action infoChanged;

		private void OnDestroy()
		{
			infoChanged = null;
		}

		#region Server
		[Command]
		private void CmdChangeName(string newName)
		{
			playerName = newName;
		}
		#endregion

		#region Client
		public override void OnClientEnterRoom()
		{
			if (hasAuthority)
			{
				CmdChangeName(GameManager.instance.clientPlayerName);
			}
		}
		public override void OnStartClient()
		{
			Debug.Log("Client initialized!");
			GameManager.instance.lobbyManager.ClientJoined();
		}
		#endregion

		private void OnPlayerNameChanged(string _old, string _new)
		{
			infoChanged?.Invoke();
		}

		public void ToggleReady()
		{
			CmdChangeReadyState(!readyToBegin);
		}

		public override void ReadyStateChanged(bool _, bool newReadyState)
		{
			infoChanged?.Invoke();
		}
	}
}
