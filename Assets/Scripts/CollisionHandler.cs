using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	int currentSceneIndex;
	int nextSceneIndex;
	[SerializeField]int timeOfDelay = 2;
	[SerializeField]ParticleSystem CrashVFX;
	[SerializeField]ParticleSystem VictoryVFX;
	AudioManager audioManager;
	bool isControlled = true;
	[SerializeField]bool collisionDisable = false;
	
	protected void Start()
	{
		LoadLevelIndexes();
		audioManager = GetComponent<AudioManager>();
	}
	protected void Update()
	{
		DebugKeys();
	}
	void LoadLevelIndexes()
	{
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		nextSceneIndex = currentSceneIndex + 1;
		if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextSceneIndex = 0;
		}
		
	}
	
	protected void OnCollisionEnter(Collision collisionInfo)
	{
		if (!isControlled || collisionDisable == true) {return;}
		
			switch (collisionInfo.gameObject.tag)
			{	
			case ("Friendly"):
				{
					Debug.Log("Bumped into Friendly");
					break;
				}
			case ("Finish"):
				{
					LevelComplete();
					break;
				}
			default:
				{
					LevelFailed();
					break;
				}
			}
		
	}
	
	void ReloadLevel()
	{
		SceneManager.LoadScene(currentSceneIndex);
	}
	void LoadNextLevel()
	{
		
		SceneManager.LoadScene(nextSceneIndex);
	}
	
	void LevelComplete()
	{
		Debug.Log("You finished the level!");
		Invoke("LoadNextLevel", timeOfDelay);
		GetComponent<PlayerMovement>().enabled = false;
		audioManager.PlayVictory();
		VictoryVFX.Play();
		isControlled = false;
	}
	void LevelFailed()
	{
		GetComponent<PlayerMovement>().enabled = false;
		audioManager.PlayExplosion();
		CrashVFX.Play();
		Debug.Log("You bumped into the surface! Be more careful");
		Invoke("ReloadLevel", timeOfDelay);
		isControlled = false;
	}
	
	void DebugKeys()
	{
		RemoveCollision();
		SkipLevel();
	}
	void RemoveCollision()
	{
		if(Input.GetKey(KeyCode.C))
		{
			collisionDisable = !collisionDisable;
		}
	}
	void SkipLevel()
	{
		if(Input.GetKey(KeyCode.L))
		{
			SceneManager.LoadScene(nextSceneIndex);
		}
	}
}