using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
 	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform cameraTrans;

	// How long the object should shake for.
	public float rumbleTime = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float rumbleAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		if (cameraTrans == null)
		{
			cameraTrans = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = cameraTrans.localPosition;
	}

	void Update()
	{
		
	}

	public void CameraRumble()
	{
		if (rumbleTime > 0)
		{
			cameraTrans.localPosition = originalPos + Random.insideUnitSphere * rumbleAmount;

			rumbleTime -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			rumbleAmount = 0f;
			cameraTrans.localPosition = originalPos;
		}
	}
}
