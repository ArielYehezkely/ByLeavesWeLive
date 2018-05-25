
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeavesAnimation : MonoBehaviour {

    public int StageLevel;
    public GameObject Durations;
    public GameObject[] LeavesObjects;
   
    private float duration;
    private float StartTime;
    private float TimeLevel;
    private float TimeLevelAnim;
    private int LeavesLevel = 0;
    private List<HObjects> Zedim;
 
    public class HObjects
    {
        public float Zed;
        public int Index;
        public GameObject hgameObject;

        public HObjects(float Zedx, int Indexx, GameObject hgameObjectx)
        {
            this.Zed = Zedx;
            this.Index = Indexx;
            this.hgameObject = hgameObjectx;
        }
    }

    // Use this for initialization
    void OnEnable () {
        /*
        LeavesObjects = new GameObject[ transform.childCount ];
        GetElements();
        */
        //*
        UnactiveLeaves();
        StartTime = Time.time;
        LeavesLevel = 0;
        float durationLevel = Durations.GetComponent<StagesTimerLevel>().StagesDuration[StageLevel];
        TimeLevel = durationLevel / LeavesObjects.Length;
//        */
    }

    void UnactiveLeaves()
    {
        foreach (var leavesObj in LeavesObjects)
        {
            leavesObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
   //     /*
        TimeLevelAnim = Time.time - StartTime;
        float timeLevel = TimeLevel * LeavesLevel;
        int leavesLength = LeavesObjects.Length;
        if ((timeLevel <= TimeLevelAnim) && (LeavesLevel < leavesLength))
        {
            LeavesObjects[LeavesLevel].SetActive(true);
            LeavesLevel++;
        }
    //    */
    }	

    void GetElements()
    {
        Zedim = new List<HObjects>();
        List<float> ZedimX = new List<float>();
 
        for (int i = 0; i < transform.childCount; i++)
        {
            Zedim.Add(new HObjects(transform.GetChild(i).gameObject.transform.localPosition.z,i, transform.GetChild(i).gameObject));
            ZedimX.Add(transform.GetChild(i).gameObject.transform.localPosition.z);
        }

        ZedimX.Sort();


        for (int i = 0; i < transform.childCount; i++)
        {
            foreach (HObjects obj in Zedim)
            {
                if (ZedimX[i] == obj.Zed)
                {
                  //  int j = transform.childCount - obj.Index - 1;
                    int j = obj.Index;
                    LeavesObjects[j] = obj.hgameObject;
                    LeavesObjects[j].SetActive(false);
                }
            }
        }
    }
}
