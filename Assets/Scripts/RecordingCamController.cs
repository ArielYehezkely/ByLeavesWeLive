using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RecordingCamController : MonoBehaviour {

    public Camera recorderCamera;
    public XRNode xrNode = XRNode.Head;

	// Use this for initialization
	void Start ()
    {
        XRDevice.DisableAutoXRCameraTracking(recorderCamera, true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition = InputTracking.GetLocalPosition(xrNode);
        transform.localRotation = InputTracking.GetLocalRotation(xrNode);
    }
}
