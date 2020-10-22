using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LobbyTest
{
	public class GamePlayer : NetworkBehaviour
	{
		[SyncVar(hook = nameof(NameChanged))]
		public string playerName;
		public TMP_Text nameText;
		public float movSpeed;
		public float rotSpeed;

		private void NameChanged(string _, string _new)
		{
			nameText.text = _new;
		}

		private void Update()
		{
			if (!hasAuthority)
				return;

			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			transform.Translate(Vector3.forward * movSpeed * input.y * Time.deltaTime, Space.Self);
			transform.Rotate(Vector3.up, rotSpeed * input.x * Time.deltaTime, Space.Self);
		}
	}
}