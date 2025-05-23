using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{

	//References to components;
	PlayerController controller;
	private AudioSource _audioSource;

	public float footstepDistance = 0.2f;
	float currentFootstepDistance = 0f;
	private float currentFootStepValue = 0f;

	[Range(0f, 1f)]
	public float audioClipVolume = 0.1f;
	public float relativeRandomizedVolumeRange = 0.2f;


	//Audio clips;
	[Header("Player")]
	public AudioClip[] footStepClips;
	public AudioClip jumpClip;

	public float footStepThreshold = 0.3f;
	public float footStepRate = 0.5f;
	private float footStepTime;

	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	//Setup;
	void Start () {
		controller = FindObjectOfType<PlayerController>();
		//Connecting events to controller events;

		if(controller != null)
		{
			controller.OnJump += OnJump;
		}
	}
	
	//Update;
	void Update () {
        if(controller.IsGrounded() && Mathf.Abs(controller.GetVelocity().y) < 0.1f)
        {
            if(controller.GetVelocity().magnitude > footStepThreshold)
            {
                if(Time.time - footStepTime > footStepRate)
                {
                    footStepTime = Time.time;
                    _audioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)], audioClipVolume);
                }
            }
        }
	}

	void OnJump()
	{
		_audioSource.PlayOneShot(jumpClip, audioClipVolume);
	}
}


