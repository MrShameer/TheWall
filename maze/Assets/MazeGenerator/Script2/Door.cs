using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform Trans;
    //public GameObject Cycle;
    Vector3 Lock,Open;
    float tod;
    static TimeOfDays TD;
    [Range(0.01f, 10)] public float speed = 0.1f;
    //public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    //int elapsedFrames = 0;
    bool day = false;

    // Start is called before the first frame update
    void Start()
    {
        TD = GameObject.Find("Directional Light").GetComponent<TimeOfDays>();
        Lock =Trans.position;
        if (Trans.localRotation.eulerAngles.y == 0f)
        {
            Open = Lock + new Vector3(0, 0, 3.1f);
        }
        else if(Trans.localRotation.eulerAngles.y == 90f)
        {
            Open = Lock + new Vector3(3.1f, 0, 0);
        }
        else if (Trans.localRotation.eulerAngles.y == 180f)
        {
            Open = Lock + new Vector3(0, 0, -3.1f);
        }
        else if (Trans.localRotation.eulerAngles.y == 270f)
        {
            Open = Lock + new Vector3(-3.1f, 0, 0);
        }

        //Open = Lock + new Vector3(0, 0, 3.1f);
        //localPosition

        //Lock
    }

    // Update is called once per frame
    void Update()
    {
        tod = TD.time;
        if (tod > 6f && tod < 19.5f)
        {
            transform.position = Vector3.MoveTowards(Trans.position, Open, Time.deltaTime*speed);
            //day = true;
            //UnityEngine.Debug.Log(tod);
        }

        if(tod <= 6f || tod > 19.5f)
        {
            transform.position = Vector3.MoveTowards(Trans.position, Lock, Time.deltaTime*speed);
           // day = false;
        }

        // {
        //float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        //transform.position = Vector3.Lerp(loc, Vector3.left, interpolationRatio);

        //elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
        // }

        //transorm.position += Vector3.left;
        
        
    }

    void open()
    {

    }

    void close()
    {

    }
}
