using UnityEngine;
using Holojam;
using System.Collections;

public class SyncGhostAnimation : Synchronizable {

	private int mRunning = 0;

	protected override void Sync() {
		if (!sending) {
			if (synchronizedInt == 1 || mRunning != synchronizedInt) {
				Debug.Log("I have received the message");
				GetComponentInChildren<Animator>().SetTrigger("Begin");
				GetComponent<AudioSource>().Play();

				mRunning = synchronizedInt;
			}
		}
	} 

}
