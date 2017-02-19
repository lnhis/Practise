using UnityEngine;
using System.Collections;
using System;

public class Button 
{
    public enum GameButtons { Up, Down, Left, Right, Jump, Action1, Action2, Cancel}

    private GameButtons gameButton;
    private KeyCode keycode;

    private bool down = false;
    private float pressTime;
    private float releaseTime = 1000;
    private float tapLength = 0;

    private Action action;
    private Action releaseAction = null;

    private bool activateOnTap = false;

    public Button(GameButtons gameButton, KeyCode keycode, Action action, Action releaseAction = null)
    {
        this.gameButton = gameButton;
        this.keycode = keycode;
        this.action = action;
        this.releaseAction = releaseAction;
    }
    public Button() { }
    public void SetActivateOnTap(bool ok, float tapLength) 
    { 
        this.activateOnTap = ok;
        this.tapLength = tapLength;
    }

    public void Press()
    {
        pressTime = Time.time;
        down = true;
    }
    public void Release()
    {
        releaseTime = Time.time;
        down = false;
        if (releaseAction != null) releaseAction.Invoke();
    }

    public void ExternalUpdate()
    {
        if (Input.GetKeyDown(keycode)) Press();
        if (Input.GetKeyUp(keycode)) Release();

        if (activateOnTap)
        {
            if (down == false && releaseTime - pressTime < tapLength)
            {
                action.Invoke();
                releaseTime = 1000;
                pressTime = 0;
            }
        }
        else
        {
            if (down)
            {
                action.Invoke();
            }
        }
    }
 
}
