using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldEntity : InputEntity
{
	public float needHoldTime { get; private set; }
	public float curruntHoldTime;
	public HoldEntity(string keyPurpose, KeyCode keyCode, Action keyAction, float needHoldTime) : base(keyPurpose, keyCode, keyAction)
	{
		this.needHoldTime = needHoldTime;
		curruntHoldTime = 0;
	}
}
