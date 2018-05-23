using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesAnimation : MonoBehaviour {

    public int StageLevel;
    public GameObject Durations;
    public GameObject[] LeavesObjects;
   
    private float duration;
    private float StartTime;
    private float TimeLevel;
    private float TimeLevelAnim;
    private int LeavesLevel = 0;

    // Use this for initialization
    void Start () {
        StartTime = Time.time;
        LeavesLevel = 0;
        float durationLevel = Durations.GetComponent<StagesTimerLevel>().StagesDuration[StageLevel];
        TimeLevel = durationLevel / LeavesObjects.Length;
    }
	
	// Update is called once per frame
	void Update () {
        TimeLevelAnim = Time.time - StartTime;
        float timeLevel = TimeLevel * LeavesLevel;
        int leavesLength = LeavesObjects.Length;
        if ((timeLevel <= TimeLevelAnim) && (LeavesLevel < leavesLength))
        {
            LeavesObjects[LeavesLevel].SetActive(true);
            LeavesLevel++;
        }
		
	}
}
