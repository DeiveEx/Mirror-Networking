using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LobbyTest
{
	public class GamePlayer : NetworkBehaviour
	{
		public TMP_Text nameText;
		public float movSpeed;
		public float rotSpeed;

		private void Update()
		{
			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			transform.Translate(Vector3.forward * movSpeed * Time.deltaTime, Space.Self);
			transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime, Space.Self);
		}
	}
}