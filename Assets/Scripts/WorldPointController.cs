using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class WorldPointController : MonoBehaviour {

	public Transform cameraTransform;
	public Vector3 targetPosition;
	public float targetYRotation;

	public Transform target;
	public Transform cameraFollow;
	public Transform worldPointCalc1;
	public Transform worldPointCalc2;

	GestureRecognizer GestureRecognizer;

	void OnEnable()
	{
		GestureRecognizer = new GestureRecognizer();
		GestureRecognizer.Tapped += GestureRecognizer_Tapped;
		GestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
		GestureRecognizer.StartCapturingGestures();
	}

	void OnDisable()
	{
		if (GestureRecognizer != null)
		{
			GestureRecognizer.Tapped -= GestureRecognizer_Tapped;
			GestureRecognizer.Dispose();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))//(Input.GetMouseButtonDown(0))
		{
			ResetToTarget();
		}
	}

	private void GestureRecognizer_Tapped(TappedEventArgs args)
	{
		//ResetToTarget();
	}

	public void ResetToTarget()
	{
		//transform.localPosition = Vector3.zero - cameraTransform.localPosition + targetPosition;
		//transform.localRotation = Quaternion.Euler(0f, 0 - cameraTransform.localEulerAngles.y + targetYRotation, 0f);
		cameraFollow.localPosition = cameraTransform.localPosition;
		cameraFollow.localEulerAngles = new Vector3(0, cameraTransform.localEulerAngles.y, 0);
		worldPointCalc1.position = transform.position;
		worldPointCalc1.rotation = transform.rotation;
		worldPointCalc2.localPosition = worldPointCalc1.localPosition;
		worldPointCalc2.localRotation = worldPointCalc1.localRotation;
		transform.position = worldPointCalc2.position;
		transform.rotation = worldPointCalc2.rotation;
	}
}
