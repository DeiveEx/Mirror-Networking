using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LobbyTest
{
	public class LevelSelect : MonoBehaviour
	{
		public void GoToLevel(string levelName)
		{
			MenuManager.instance.lobbyManager.ServerChangeScene(levelName);
		}

		public void BackToLobby()
		{
			MenuManager.instance.ShowPanel(MenuManager.PanelType.Room);
		}
	}
}