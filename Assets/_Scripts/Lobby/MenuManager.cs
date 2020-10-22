using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace LobbyTest
{
	public class MenuManager : Singleton<MenuManager>
	{
		public enum PanelType
		{
			Loading,
			Name,
			HostOrJoin,
			Room,
			LevelSelect
		}

		[System.Serializable]
		public class MenuPanel
		{
			public PanelType panelType;
			public GameObject panelObj;
		}

		public PanelType startPanel;
		public MenuPanel[] panels;

		protected override void Awake()
		{
			base.Awake();
			GameManager.instance.lobbyManager.playerJoined += OnPlayerJoined;
		}


		protected override void OnDestroy()
		{
			base.OnDestroy();
			GameManager.instance.lobbyManager.playerJoined -= OnPlayerJoined;
		}

		private void OnEnable()
		{
			ShowPanel(startPanel);
		}

		private void OnPlayerJoined()
		{
			ShowPanel(PanelType.Room);
		}

		public void ShowPanel(PanelType type)
		{
			Debug.Log($"Changing panel to: {type}");
			foreach (var panel in panels)
			{
				panel.panelObj.SetActive(panel.panelType == type);
			}
		}

		public GameObject GetPanel(PanelType type)
		{
			return panels.FirstOrDefault(x => x.panelType == type)?.panelObj;
		}
	}
}
