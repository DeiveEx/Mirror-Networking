using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LobbyTest
{
    public class HostPanel : MonoBehaviour
    {
		public TMP_InputField input;

		public void HostGame()
		{
			MenuManager.instance.lobbyManager.StartHost();
		}

		public void JoinGame()
		{
			MenuManager.instance.lobbyManager.networkAddress = input.text;
			MenuManager.instance.lobbyManager.StartClient();
		}
	}
}
