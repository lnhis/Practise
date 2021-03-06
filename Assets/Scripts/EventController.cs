﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{

	public static HashSet<EventController> ActivatedEvents = new HashSet<EventController>();

	public static bool CanActivateEvent(EventController ev)
	{
		if (ActivatedEvents.Contains (ev))
        {
			if (ev.CanActivateAgain > 0f && Time.time - ev.ActivationTime > ev.CanActivateAgain)
            {
				return true;
			}
			return false;
		}

		ActivatedEvents.Add (ev);

		return true;
	}

	public float ActivationDelay = 0f;
	public float CanActivateAgain = 0f;
	public GameObject[] ActivateObjects;
	public GameObject[] DeactivateObjects;
	public string PlaySound;
	public string PlayMusic;
	public bool StopMusic;

	public float ClosePanelAfter = 1f;
	public string OpenPanel;
    public string LoadScene;

	public bool TeleportSonic = false;
	public Vector2 TeleportPosition;

	[HideInInspector]
	public float ActivationTime= 0f;

  	private IEnumerator ActivateAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);

		ActivateEvent();
	}

	public void OnTriggerEnter2D(Collider2D whoHitMe)
	{
		Character c = whoHitMe.GetComponent<Character> ();
		if (c == null)
			return;
		
		if (CanActivateEvent (this)) 
		{
            ActivationTime = Time.time;

            StartCoroutine (ActivateAfterSeconds (ActivationDelay));
		}

	}

	private void ActivateEvent()
	{
        if(!string.IsNullOrEmpty(LoadScene))
        {
            SceneManager.LoadScene(LoadScene);
        }
		foreach (GameObject go in DeactivateObjects)
        {
			go.SetActive (false);
		}
		foreach (GameObject go in ActivateObjects)
        {
			go.SetActive (true);
		}
		PanelSpawnerController.Instance.Spawn (OpenPanel, ClosePanelAfter);

        if(StopMusic)
        {
            SoundSystem.Instance.StopMusic();
        }
        SoundSystem.Instance.PlayMusic(PlayMusic);
        SoundSystem.Instance.PlaySound(PlaySound);
    }
}
