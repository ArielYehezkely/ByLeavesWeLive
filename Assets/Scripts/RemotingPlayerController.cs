using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.WSA;
using UnityEngine.UI;

public class RemotingPlayerController : MonoBehaviour {

    public InputField IPText;
    public Text connectionText;

    private bool connected;

    private void Awake()
    {
        XRSettings.enabled = false;
    }
    
    public void Connect()
    {
        if (HolographicRemoting.ConnectionState == HolographicStreamerConnectionState.Disconnected)
        {
            HolographicRemoting.Connect(IPText.text);
        }
    }

    public void Disconnect()
    {
        if (HolographicRemoting.ConnectionState != HolographicStreamerConnectionState.Disconnected)
        {
            HolographicRemoting.Disconnect();
        }
    }

    void Update()
    {
        if (connectionText.text != HolographicRemoting.ConnectionState.ToString())
        {
            connectionText.text = HolographicRemoting.ConnectionState.ToString();
        }
        if (!connected && HolographicRemoting.ConnectionState == HolographicStreamerConnectionState.Connected)
        {
            connected = true;

            StartCoroutine(LoadDevice("WindowsMR"));
        }
    }

    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }
}
