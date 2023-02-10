using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
	[SerializeField] AudioSourceController audioSourceSFX;
	[SerializeField] AudioSourceController audioSourceMusic;

	// list of available audio source controllers
	private List<AudioSourceController> audioSourceControllers = new List<AudioSourceController>();

	public AudioSourceController GetController(AudioData.Type type)
	{
		AudioSourceController output = null;

		// check for available audio source controllers
		if (audioSourceControllers.Count > 0)
		{
			// check matching type
			output = audioSourceControllers.Find(audioSourceController => audioSourceController.type == type);
			if (output != null)
			{
				// found available, remove from audio source controllers
				audioSourceControllers.Remove(output);

				return output;
			}
		}

		// could not get available audio source controller
		// create new audio source controller
		switch (type)
		{
			case AudioData.Type.SFX:
				return Instantiate(audioSourceSFX);
			case AudioData.Type.MUSIC:
				return Instantiate(audioSourceMusic);
			default:
				return null;
		}
	}

	public void ReturnController(AudioSourceController controller)
	{
		// return audio source controller to available list
		if (audioSourceControllers.Contains(controller) == false)
		{
			audioSourceControllers.Add(controller);
		}
	}
}
