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
			Room
		}

		[System.Serializable]
		public class MenuPanel
		{
			public PanelType panelType;
			public GameObject panelObj;
		}

		public MenuPanel[] panels;

		public LobbyNetworkManager lobbyManager { get; private set; }


		protected override void Awake()
		{
			base.Awake();
			lobbyManager = FindObjectOfType<LobbyNetworkManager>();
		}

		private void OnEnable()
		{
			ShowPanel(PanelType.Name);	
		}

		public void ShowPanel(PanelType type)
		{
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
