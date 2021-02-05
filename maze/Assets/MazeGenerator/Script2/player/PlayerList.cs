using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerList : MonoBehaviour
{
    
    public static PlayerList instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject player;
}
