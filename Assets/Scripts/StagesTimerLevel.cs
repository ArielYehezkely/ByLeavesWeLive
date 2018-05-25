using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesTimerLevel : MonoBehaviour
{

    public GameObject[] StagesObject;
    public float[] StagesTime;
    public float[] StagesDuration;
    public int TotalStages = 13;
    public AudioSource backgroundSound;
    public Animation seaAnimation;

    private float StartTime = 0;
    private float TimeLevel;
    private int StageLevel = 0;


    // Use this for initialization
    void Start()
    {
        //StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DeactiveStages();
            StageLevel = 0;
            StartTime = Time.time;
            backgroundSound.Stop();
            backgroundSound.Play();
        }
        else if (StartTime == 0)
        {
            return;
        }
        TimeLevel = Time.time - StartTime;
        if (StageLevel < TotalStages)
        {
            if (StagesTime[StageLevel] <= TimeLevel)
            {
                StagesObject[StageLevel].SetActive(true);
                /*               if (StageLevel == 11)
                               {
                                   seaAnimation.Play("Stage12");
                               }*/
                StageLevel++;
            }
        }
    }

    private void DeactiveStages()
    {
        foreach (var stageObj in StagesObject)
        {
            stageObj.SetActive(false);
        }
    }
}
