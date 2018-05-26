using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class WorldPointController : MonoBehaviour {

	public Transform cameraTransform;

	public Transform target;
	public Transform cameraFollow;
	public Transform worldPointCalc1;
	public Transform worldPointCalc2;

    public float speed = 0.1f;
    public float rotationSpeed = 1f;

    private Vector3 targetDefaultPosition;
    private Vector3 targetDefaultRotation;

	GestureRecognizer GestureRecognizer;

    private void Awake()
    {
        targetDefaultPosition = target.position;
        targetDefaultRotation = target.eulerAngles;
        LoadPrefs();
    }

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetDefault();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            NewPrefs();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            DeletePrefs();
        }
        if (Input.GetKeyDown(KeyCode.R))
		{
			ResetToTarget();
		}
        if (Input.GetKey(KeyCode.Alpha1))
        {
            var distance = speed * Time.deltaTime;
            target.Translate(distance, 0, 0, Space.World);
            transform.Translate(distance, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            var distance = -speed * Time.deltaTime;
            target.Translate(distance, 0, 0, Space.World);
            transform.Translate(distance, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            var distance = speed * Time.deltaTime;
            target.Translate(0, distance, 0, Space.World);
            transform.Translate(0, distance, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            var distance = -speed * Time.deltaTime;
            target.Translate(0, distance, 0, Space.World);
            transform.Translate(0, distance, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            var distance = speed * Time.deltaTime;
            target.Translate(0, 0, distance, Space.World);
            transform.Translate(0, 0, distance, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            var distance = -speed * Time.deltaTime;
            target.Translate(0, 0, distance, Space.World);
            transform.Translate(0, 0, distance, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            var angle = rotationSpeed * Time.deltaTime;
            //target.RotateAround(transform.position, new Vector3(0, 1, 0), angle);// 0, angle, 0, Space.World);
            target.Rotate(0, angle, 0, Space.World);
            transform.RotateAround(target.position, new Vector3(0, 1, 0), angle);// 0, angle, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            var angle = -rotationSpeed * Time.deltaTime;
            target.Rotate(0, angle, 0, Space.World);
            transform.RotateAround(target.position, new Vector3(0, 1, 0), angle);// 0, angle, 0, Space.World);
        }
    }

	private void GestureRecognizer_Tapped(TappedEventArgs args)
	{
		//ResetToTarget();
	}

    public void GetDefault()
    {
        target.position = targetDefaultPosition;
        target.eulerAngles = targetDefaultRotation;
    }

    public void NewPrefs()
    {
        var posJson = JsonUtility.ToJson(target.position);
        var rotJson = JsonUtility.ToJson(target.eulerAngles);
        PlayerPrefs.SetString("targetPosition", posJson);
        PlayerPrefs.SetString("targetRotation", rotJson);
    }

    public void LoadPrefs()
    {
        if (PlayerPrefs.HasKey("targetPosition") || PlayerPrefs.HasKey("targetRotation"))
        {
            var posJson = JsonUtility.ToJson(targetDefaultPosition);
            var rotJson = JsonUtility.ToJson(targetDefaultRotation);
            target.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("targetPosition", posJson));
            target.eulerAngles = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("targetRotation", rotJson));
        }
    }

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteKey("targetPosition");
        PlayerPrefs.DeleteKey("targetRotation");
    }

    public void ResetToTarget()
	{
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
