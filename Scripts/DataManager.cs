using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public Material ShinyMaterial;
    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static DataManager instance;

    void Start()
    {
        instance = this;
    }

}
