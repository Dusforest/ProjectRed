using UnityEngine;
using System;

public class InputEntity
{
	public string keyPurpose { get; protected set; }
	public KeyCode keyCode { get; protected set; }
	public Action KeyAction { get; protected set; }

	public InputEntity(string keyPurpose, KeyCode keyCode, Action keyAction)
	{
		this.keyPurpose = keyPurpose;
		this.keyCode = keyCode;
		KeyAction = keyAction;
	}
}
