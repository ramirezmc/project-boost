using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioClip ExplosionSFX;
	[SerializeField] AudioClip VictorySFX;
	[SerializeField] [Range(0,1f)]float volume = 1f;
	AudioSource audioSource;
	
	protected void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void playSFX(AudioClip clip)
	{
		AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
	}
	
	public void PlayExplosion()
	{
		playSFX(ExplosionSFX);
	}
	public void PlayVictory()
	{
		playSFX(VictorySFX);
	}
	
	
}
