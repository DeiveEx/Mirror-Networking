 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LobbyTest
{
	public class GameManager : Singleton<GameManager>
	{
		public LobbyNetworkManager lobbyManager;
		public string clientPlayerName;
	}
}