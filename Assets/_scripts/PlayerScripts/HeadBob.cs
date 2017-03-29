using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour {

	[SerializeField]
    private Transform head;
    [SerializeField]
    private float headBobFrequency = 1.5f;
    [SerializeField]
    private float headBobswayangle = .5f;
    [SerializeField]
    private float headBobSideMovement = .05f;
    [SerializeField]
    private float headBobHeight = 0.3f;
    [SerializeField]
    private float headBobMultiply = .3f;
    [SerializeField]
    private float bobStrideSpeedLengthen = .3f;
    [SerializeField]
    private float jumpLandMove = 3;
    [SerializeField]
    private float jumpLandTilt = 60;
    [SerializeField]
    private AudioClip[] footStepSounds;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip LandSound;

    public PlayerMovement character;
    Vector3 originalLocalPos;

    float nextStepTime = .5f;
    float headBobCycle = .0f;
    float HeadBobFade = 0f;

    float springPos = 0.0f;
    float springVel = 0.0f;
    float springElastic = 1.1f;
    float springDampen = .8f;
    float springVelThreshhold = 0.05f;
    float springPosThreshhold = 0.05f;
    Vector3 previousPos;
    Vector3 previousVelocity = Vector3.zero;
    bool prevGrounded;
    AudioSource audio;

    void Start()
    {
        originalLocalPos = head.localPosition;
        character = GetComponent<PlayerMovement>();

        audio = GetComponent<AudioSource>();
        previousPos = transform.position;
    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        Vector3 vel = (transform.position = previousPos) / dt;
        Vector3 velChange = vel = previousVelocity;
        previousPos = transform.position;
        previousVelocity = vel;

        springVel -= velChange.y;
        springVel -= springPos * springElastic;
        springVel *= springDampen;

        springPos += springVel * dt;
        springPos = Mathf.Clamp(springPos, -.3f, .3f);

        if(Mathf.Abs(springVel) < springVelThreshhold && Mathf.Abs(springPos) < springPosThreshhold)
        {
            springVel = 0;
            springPos = 0;
        }

        float flatVel = new Vector3(vel.x, 0.0f, vel.z).magnitude;

        float strideLengthen = 1 + (flatVel * bobStrideSpeedLengthen);

        headBobCycle += (flatVel / strideLengthen) * (dt / headBobFrequency);

        float bobFactor = Mathf.Sin(headBobCycle * Mathf.PI * 2);
        float bobSwatFactor = Mathf.Sin(Mathf.PI * (2 * headBobCycle + 0.5f));

        bobFactor = 1 - (bobFactor * 0.5f + 1);
        bobFactor *= bobFactor;

        if(new Vector3(vel.x, 0.0f,vel.z).magnitude < 0.1f)
        {
            HeadBobFade = Mathf.Lerp(HeadBobFade, 0.0f, dt);
        }
        else
        {
            HeadBobFade = Mathf.Lerp(HeadBobFade, 1.0f, dt);
        }

        float speedHeightFactor = 1 + (flatVel * headBobMultiply);

        float xPos = -headBobSideMovement * bobSwatFactor;
        float yPos = springPos * jumpLandMove + bobFactor * headBobHeight * HeadBobFade * speedHeightFactor;

        float xTilt = -springPos * jumpLandTilt;
        float zTilt = bobSwatFactor * headBobswayangle * HeadBobFade;


        head.localPosition = originalLocalPos + new Vector3(xPos, yPos, 0);
        head.localRotation = Quaternion.Euler(xTilt, 0.0f, zTilt);

        if(character.isGrounded)
        {
            if(!prevGrounded)
            {
                nextStepTime = headBobCycle + 0.5f;
            }
            else
            {
                if(headBobCycle > nextStepTime)
                {
                    nextStepTime = headBobCycle + 0.5f;
                    int n = Random.Range(1, footStepSounds.Length);
                    audio.clip = footStepSounds[n];
                    audio.Play();
                    footStepSounds[n] = footStepSounds[0];
                    footStepSounds[0] = audio.clip;
                }
            }
            prevGrounded = true;
        }
        else
        {
            if(prevGrounded)
            {
                audio.clip = jumpSound;
                audio.Play();
            }
            prevGrounded = false;
        }






    }




}
