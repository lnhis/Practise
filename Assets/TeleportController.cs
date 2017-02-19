using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

	public static Dictionary<string, TeleportController> Teleports = new Dictionary<string, TeleportController> ();

	public string TeleportID = "notset";
	public string TeleportsTo = "notset";

	public bool Active = false;

	private Vector2 groundPos;

	void Awake()
	{
		if (Teleports.ContainsKey (TeleportID)) {
			Debug.LogError ("Two teleports with ID: " + TeleportsTo);
		}

		Teleports.Add (TeleportID, this);
	}
	// Use this for initialization
	void Start () 
	{
		int layerMask = LayerMask.GetMask ("Ground");
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 10f, layerMask);
		groundPos = hit.point;
		GameObject go = new GameObject ();
		go.transform.position = groundPos;
		go.name = "DebugMe!";

	}
	public Vector3 GetSpawnPos()
	{
		return groundPos + new Vector2 (0f, 1.75f);
	}
	// Update is called once per frame
	void Update () {
		
	}
	void Destroy()
	{
		Teleports.Remove (TeleportID);
	}

	public void OnTriggerEnter2D(Collider2D whoHitMe)
	{
		if (Active)
			return;
		Debug.LogWarning ("Flag 1: " + whoHitMe.transform.name);
		TeleportController target = Teleports [TeleportsTo];
		target.Active = true;
		whoHitMe.transform.position = target.GetSpawnPos ();//target.transform.position;
	}
	public void OnTriggerExit2D(Collider2D whoLeftMe)
	{
		Debug.LogWarning ("Flag 2: " + whoLeftMe.transform.name);
		Active = false;
	}

}
