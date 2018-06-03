using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamLoader : MonoBehaviour {

    float width;
    float height;
    public bool updateScaleOnRun = true;

	// Use this for initialization
	void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.deviceName = devices[devices.Length-1].name;
        webcamTexture.Play();
        if (updateScaleOnRun)
        {
            width = Screen.width * 1f;
            height = Screen.height * 1f;
            transform.localScale = new Vector3(width / height, 1f, 1f);
        }
    }

    private void Update()
    {
        if (!updateScaleOnRun)
        {
            return;
        }
        if (!width.Equals(Screen.width) || !height.Equals(Screen.height))
        {
            width = Screen.width * 1f;
            height = Screen.height * 1f;
            transform.localScale = new Vector3(width / height, 1f, 1f);
        }
    }
}
