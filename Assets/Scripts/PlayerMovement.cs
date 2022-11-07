using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody playerRigidBody;
	AudioManager audioManager;
	AudioSource audioSource;
	[SerializeField]int thrustPower = 1000;
	[SerializeField]int steeringPower = 10;
	[SerializeField]ParticleSystem RocketThrustFX;
	[SerializeField]ParticleSystem LeftThruster;
	[SerializeField]ParticleSystem RightThruster;
    // Start is called before the first frame update
    void Start()
	{
		audioSource = GetComponent<AudioSource>();
		playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
	    ProcessThrust();
	    ProcessRotation();
    }
    
	void ProcessThrust()
	{
		
		if (Input.GetKey(KeyCode.Space))
		{
			playerRigidBody.AddRelativeForce(Vector3.up *thrustPower* Time.deltaTime);
			if(!audioSource.isPlaying)
			{
				audioSource.Play();
				RocketThrustFX.Play();
			}
		}
		else
		{
			audioSource.Stop();
			RocketThrustFX.Stop();
		}
	}
	
	void ProcessRotation()
	{
		if (Input.GetKey(KeyCode.A))
		{
			RotateBody(steeringPower);
			if(!RightThruster.isPlaying)
			{
				RightThruster.Play();
			}
		}
		else if (Input.GetKey(KeyCode.D))
		{
			RotateBody(-steeringPower);
			if(!LeftThruster.isPlaying)
			{
				LeftThruster.Play();
			}
		}
		else
		{
			RightThruster.Stop();
			LeftThruster.Stop();
		}
	}
	void RotateBody(int input)
	{
		playerRigidBody.freezeRotation = true; //Freezing rotation when being used;
		transform.Rotate(0,0,input * Time.deltaTime);
		playerRigidBody.freezeRotation = false;
	}
}
