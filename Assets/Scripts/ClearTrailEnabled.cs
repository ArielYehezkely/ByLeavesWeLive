using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrailEnabled : MonoBehaviour {

    public TrailRenderer trail;

    private void OnEnable()
    {
        if (trail == null)
        {
            trail = gameObject.GetComponent<TrailRenderer>();
        }
        trail.Clear();
    }
}
