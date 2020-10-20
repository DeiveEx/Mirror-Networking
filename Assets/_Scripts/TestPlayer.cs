using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

//By inheriting from network behavior, we tell Mirror that we want this object to be synchronized through the network. NOTE: this ONLY works if the game object also has a "Network identity" component!
public class TestPlayer : NetworkBehaviour
{
	public float movSpeed;
	public float rotSpeed;
	public Transform floatinInfo;
	public TextMesh playerNameText;
	public GameObject[] weaponsArray;

	[Header("Network Vars")]
	/* Tells mittor that we want this variable to be synced over all instances of this objects across the network. We also add a "hook", which is a callback for when this value changes.
	 * NOTE:The sync only happens if the variable is changed on the SERVER (so we need to use commands to change these cariables)*/
	[SyncVar(hook = nameof(OnNameChanged))]
	public string playerName;
	[SyncVar(hook = nameof(OnWeaponChanged))]
	public int activeWeaponSynced;

	private SceneScript sceneScript;
	private int selectedWeaponLocal = 1;

	#region Monobehavior
	private void Awake()
	{
		sceneScript = FindObjectOfType<SceneScript>();

		foreach (var item in weaponsArray)
		{
			item.SetActive(false);
		}
	}

	private void Update()
	{
		//If this is NOT a local player (meaning it's a remote player), do this
		if (!isLocalPlayer)
		{
			floatinInfo.LookAt(Camera.main.transform);
			return;
		}

		//If this IS a local player, do this
		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		transform.Rotate(0, input.x * rotSpeed * Time.deltaTime, 0);
		transform.Translate(0, 0, input.z * movSpeed * Time.deltaTime);

		if (Input.GetButtonDown("Fire1"))
		{
			selectedWeaponLocal += 1;

			if(selectedWeaponLocal >= weaponsArray.Length)
			{
				selectedWeaponLocal = 0;
			}

			CmdChangeActiveWeapon(selectedWeaponLocal);
		}
	}
	#endregion

	#region Callbacks
	//Callback for when this object is spawned and is a "local player", meaning that this player object is the object that the client is controlling
	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();

		Camera.main.transform.SetParent(transform);
		Camera.main.transform.localPosition = new Vector3(0, 0, 0);

		string name = $"Player {Random.Range(0, 100)}";

		CmdSetupPlayer(name);
		this.name = name;

		sceneScript.playerScript = this;
	}
	#endregion

	#region Commands
	//Commands are functions that can be called from clients, but are executed on the server. All commands MUST start with "Cmd"
	[Command]
	private void CmdSetupPlayer(string name)
	{
		playerName = name;
		sceneScript.statusText = $"{playerName} joined.";
	}

	[Command]
	public void CmdSendPlayerMessage()
	{
		if(sceneScript != null)
		{
			sceneScript.statusText = $"{playerName} says hello {Random.Range(0, 99)}";
		}
	}

	[Command]
	public void CmdChangeActiveWeapon(int id)
	{
		activeWeaponSynced = id;
	}
	#endregion

	#region Hooks
	//Hooks needs to contain 2 parameters, the old value and the new value
	private void OnNameChanged(string oldName, string newName)
	{
		playerNameText.text = newName;
	}

	private void OnWeaponChanged(int _old, int _new)
	{
		//Disable old weapon
		if(_old < weaponsArray.Length && weaponsArray[_old] != null)
		{
			weaponsArray[_old].SetActive(false);
		}

		//Enable new weapon
		if (_new < weaponsArray.Length && weaponsArray[_new] != null)
		{
			weaponsArray[_new].SetActive(true);
		}
	}

	#endregion
}
