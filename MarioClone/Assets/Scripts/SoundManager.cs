using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SoundManager : MonoBehaviour
{

	// Holds the single instance of the SoundManager that 
	// you can access from any script
	public static SoundManager Instance = null;

	// All sound effects in the game
	// All are public so you can set them in the Inspector
	public AudioClip jump;
	public AudioClip getCoin;
	public AudioClip rockSmash;
	public AudioClip mannyDies;

	// Refers to the audio source added to the SoundManager
	// to play sound effects
	private AudioSource soundEffectAudio;

	// Use this for initialization
	void Start()
	{

		// This is a singleton that makes sure you only
		// ever have one Sound Manager
		// If there is any other Sound Manager created destroy it
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		AudioSource theSource = GetComponent<AudioSource>();
		soundEffectAudio = theSource;

	}

	// Other GameObjects can call this to play sounds
	public void PlayOneShot(AudioClip clip)
	{
		soundEffectAudio.PlayOneShot(clip);
	}

	public void PlayGetCoin()
	{
		soundEffectAudio.PlayOneShot(getCoin);
	}
}