using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelSpawnerController : MonoBehaviour 
{
	public GameObject[] Panels;

	public static PanelSpawnerController Instance;

	void Awake()
	{
		Instance = this;
	}

	public void Spawn(string panelName, float closeAfterSeconds)
	{
		GameObject go = Panels.FirstOrDefault (xs => xs.name == panelName);
		Debug.LogWarning ("SPAWN PANEL: " + go);
		if (go != null) {
			GameObject newGo = GameObject.Instantiate (go);
			newGo.transform.SetParent (this.transform, false);
			StartCoroutine (CloseAfterSeconds (newGo, closeAfterSeconds));

		}

	}
	private IEnumerator CloseAfterSeconds(GameObject panel, float seconds)
	{
		yield return new WaitForSeconds (seconds);

		GameObject.Destroy (panel);
	}
}
