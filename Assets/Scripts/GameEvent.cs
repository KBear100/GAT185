using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct GameEvent
{
	public enum Type
	{
		GAME_OVER,
		LEVEL_COMPLETE,
		PLAYER_DEAD,
		CHECK_POINT
	}

	public Type type;
	public GameObject sender;

	public GameEvent(Type type)
	{
		this.type = type;
		this.sender = null;
	}

	public GameEvent(Type type, GameObject sender)
	{
		this.type = type;
		this.sender = sender;
	}
}

