using System;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;
//using System.Media;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //LightingManager LM = new LightingManager();
    //public static LightingManager instance;
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [Range(0,5)]public float speed=0.1f;
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    public GameObject mazecentre;
    public Vector3 GhostCentreMaze;

    public GameObject mazetopright;
    public Vector3 GhostTopRightMaze;

    public GameObject mazetopleft;
    public Vector3 GhostTopLeftMaze;

    public GameObject mazebottomleft;
    public Vector3 GhostBottomLeftMaze;

    public GameObject mazebottomright;
    public Vector3 GhostBottomRightMaze;

    public GameObject Ghost;

    public Transform trans;

    public Material Day;
    public Material Night;
    
    Boolean spawn = false;

    GameObject mc;
    GameObject mtr;
    GameObject mtl;
    GameObject mbl;
    GameObject mbr;

    GameObject[] Gh=new GameObject[10];

    //playercontroller pc = Ghost.GetComponent<playercontroller>();
    // GameObject Gh1;

    /*private void Start()
    {
        for(int i = 0; i < Gh.Length; i++)
        {
            Gh[i] = new GameObject();
        }
    }*/

     /*private void Update()
     {
         if (Preset == null)
             return;

         /*if (Application.isPlaying)
         {
             //(Replace with a reference to the game time)
             TimeOfDay += Time.deltaTime*speed;
             TimeOfDay %= 24; //Modulus to ensure always between 0-24
             UpdateLighting(TimeOfDay / 24f);
             //UnityEngine.Debug.Log(TimeOfDay);
             if(TimeOfDay>6f && TimeOfDay<6.5f && spawn)//(TimeOfDay<6f || TimeOfDay > 19f)
             {
                 //mc =Instantiate(mazecentre, trans.position, Quaternion.identity) as GameObject;
                 //mtr = Instantiate(mazetopright, trans.position, Quaternion.identity) as GameObject;
                 //mtl = Instantiate(mazetopleft, trans.position, Quaternion.identity) as GameObject;
                 //mbl = Instantiate(mazebottomleft, trans.position, Quaternion.identity) as GameObject;
                 // mbr = Instantiate(mazebottomright, trans.position, Quaternion.identity) as GameObject;

                 RenderSettings.skybox = Day;

                 Gh[0] = Instantiate(Ghost, GetRandomPoint(GhostCentreMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[0].GetComponent<EnemyController>().centre = GhostCentreMaze;
                 Gh[1] = Instantiate(Ghost, GetRandomPoint(GhostCentreMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[1].GetComponent<EnemyController>().centre = GhostCentreMaze;

                 Gh[2] = Instantiate(Ghost, GetRandomPoint(GhostTopRightMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[2].GetComponent<EnemyController>().centre = GhostTopRightMaze;
                 Gh[3] = Instantiate(Ghost, GetRandomPoint(GhostTopRightMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[3].GetComponent<EnemyController>().centre = GhostTopRightMaze;

                 Gh[4] = Instantiate(Ghost, GetRandomPoint(GhostTopLeftMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[4].GetComponent<EnemyController>().centre = GhostTopLeftMaze;
                 Gh[5] = Instantiate(Ghost, GetRandomPoint(GhostTopLeftMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[5].GetComponent<EnemyController>().centre = GhostTopLeftMaze;

                 Gh[6] = Instantiate(Ghost, GetRandomPoint(GhostBottomLeftMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[6].GetComponent<EnemyController>().centre = GhostBottomLeftMaze;
                 Gh[7] = Instantiate(Ghost, GetRandomPoint(GhostBottomLeftMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[7].GetComponent<EnemyController>().centre = GhostBottomLeftMaze;

                 Gh[8] = Instantiate(Ghost, GetRandomPoint(GhostBottomRightMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[8].GetComponent<EnemyController>().centre = GhostBottomRightMaze;
                 Gh[9] = Instantiate(Ghost, GetRandomPoint(GhostBottomRightMaze, 40f), Quaternion.identity) as GameObject;
                 Gh[9].GetComponent<EnemyController>().centre = GhostBottomRightMaze;

                 spawn = false;
                 //UnityEngine.Debug.Log("Spawn");
             }
             if(TimeOfDay<=6f || TimeOfDay > 19.5f && !spawn)
             {
                 //UnityEngine.Debug.Log("Night");
                 RenderSettings.skybox = Night;
                 Destroy(mc);
                 Destroy(mtr);
                 Destroy(mtl);
                 Destroy(mbl);
                 Destroy(mbr);

                 Destroy(Gh[0]);
                 Destroy(Gh[1]);
                 Destroy(Gh[2]);
                 Destroy(Gh[3]);
                 Destroy(Gh[4]);
                 Destroy(Gh[5]);
                 Destroy(Gh[6]);
                 Destroy(Gh[7]);
                 Destroy(Gh[8]);
                 Destroy(Gh[9]);

                 spawn = true;
                 //UnityEngine.Debug.Log("Destroy");
             }
         }*/
         //else
        // {
             
        // }
     //}


    public void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * maxDistance + center;
        randomPos.y = center.y;

        return randomPos;
    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}