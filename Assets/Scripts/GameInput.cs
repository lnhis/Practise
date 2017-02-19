using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameInput : MonoBehaviour 
{
    private List<Button> buttons = new List<Button>();
	public Character character;

	void Start () 
    {
        //character = charObj.transform.GetComponent<Character>();
        buttons.Add(new Button(Button.GameButtons.Up, KeyCode.W, character.UpAction, character.StopHorizontalMovement));
        buttons.Add(new Button(Button.GameButtons.Down, KeyCode.S, character.DownAction, character.StopHorizontalMovement));
        buttons.Add(new Button(Button.GameButtons.Left, KeyCode.A, character.LeftAction, character.StopVerticalMovement));
        buttons.Add(new Button(Button.GameButtons.Right, KeyCode.D, character.RightAction, character.StopVerticalMovement));
        buttons.Add(new Button(Button.GameButtons.Jump, KeyCode.Space, character.JumpAction, character.JumpActionEnd));
        buttons[buttons.Count - 1].SetActivateOnTap(true, 0.3f);
	}

	void Update () 
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].ExternalUpdate();
        }
	}



}
