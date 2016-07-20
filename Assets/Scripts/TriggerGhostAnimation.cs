using UnityEngine;
using Holojam;
using System.Collections;

public class TriggerGhostAnimation :  Synchronizable {

	public KeyCode Key;

	protected override void Sync() {
		if (sending && Input.GetKeyDown(Key)) {
			Debug.Log("BEGIN!!!");
			synchronizedInt = 1;
		}
	}

}
