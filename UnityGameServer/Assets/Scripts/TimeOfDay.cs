using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDay : MonoBehaviour
{
    public int playables = 1;
    public bool start = false;
    [Range(0, 24)] public float timeofday = 0f;
    public float speed = 100.01f;
    //public bool day = false;
    public void Update()
    {
        if (Server.CurrentPlayers >= playables || start)
        {
            start = true;
            timeofday += Time.deltaTime * speed;
            timeofday %= 24;
            ServerSend.TimeOfDays(timeofday/24);
            //Debug.Log(timeofday);
        }
    }
}
