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
		public event Action playerDisconnected;

		private void OnDestroy()
		{
			infoChanged = null;
			playerDisconnected?.Invoke();
		}

		public override void OnStartClient()
		{
			RoomPanel room = MenuManager.instance.GetPanel(MenuManager.PanelType.Room).GetComponent<RoomPanel>();
			RoomPlayerMenu playerMenu = room.GetFreePanel();

			if(playerMenu != null)
			{
				playerMenu.SetOwner(this);
				infoChanged?.Invoke();
			}
		}

		public override void OnClientEnterRoom()
		{
			if (hasAuthority)
			{
				CmdChangeName(GameManager.instance.clientPlayerName);
			}
		}

		[Command]
		private void CmdChangeName(string newName)
		{
			playerName = newName;
		}

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
