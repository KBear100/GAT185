using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour
{
	public AudioData.Type type;

	private AudioSource audioSource;
	private Transform parent;
	private bool active = false;
		
	void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

		
	void LateUpdate()
	{
		if (active && !audioSource.isPlaying)
		{
			Stop();
		}

		if (parent != null) 
		{
			transform.position = parent.position;
		}

	}

	public void SetSourceProperties(AudioClip clip, float volume, float picth, bool loop, float spacialBlend)
	{
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.pitch = picth;
		audioSource.loop = loop;
		audioSource.spatialBlend = spacialBlend;
	}

	public void SetParent(Transform parent)
	{
		this.parent = parent;
	}

	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}

	public void Play()
	{
		active = true;
		audioSource.Play();
	}

	public void Stop()
	{
		audioSource.Stop();
		Reset();
		AudioManager.Instance.ReturnController(this);
	}

	private void Reset()
	{
		active = false;
		parent = null;
	}
}
