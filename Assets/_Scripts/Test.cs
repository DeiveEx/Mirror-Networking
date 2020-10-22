using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : NetworkBehaviour
{
    [SyncVar]
    public int test;
	public TMP_Text counter;

	public override void OnStartServer()
	{
		InvokeRepeating(nameof(TestCounter), 1, 1);
	}

	private void TestCounter()
	{
		test++;
	}
}
