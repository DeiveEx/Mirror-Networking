using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : NetworkBehaviour
{
	public Text statusTextObj;
	public TestPlayer playerScript;

	[SyncVar(hook = nameof(OnStatusTextChanged))]
	public string statusText;

	private void OnStatusTextChanged(string oldText, string newText)
	{
		statusTextObj.text = newText;
	}

	public void ButtonSendMessage()
	{
		if(playerScript != null)
		{
			playerScript.CmdSendPlayerMessage();
		}
	}
}
