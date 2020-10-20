using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LobbyTest
{
	public class NamePanel : MonoBehaviour
	{
		public TMP_InputField nameInput;
		public Button confirmButton;

		private void Update()
		{
			confirmButton.interactable = !string.IsNullOrEmpty(nameInput.text);
		}

		public void ConfirmName()
		{
			GameManager.instance.clientPlayerName = nameInput.text;
			MenuManager.instance.ShowPanel(MenuManager.PanelType.HostOrJoin);
		}
	}
}