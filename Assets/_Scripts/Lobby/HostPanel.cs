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
			GameManager.instance.lobbyManager.StartHost();
		}

		public void JoinGame()
		{
			GameManager.instance.lobbyManager.networkAddress = input.text;
			GameManager.instance.lobbyManager.StartClient();
		}
	}
}
