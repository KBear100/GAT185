using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayOnAwake : MonoBehaviour
{
	public AudioData audioData;
	
	void Awake()
	{
		if (audioData != null)
		{
			audioData.Play(transform);
		}
	}
}
