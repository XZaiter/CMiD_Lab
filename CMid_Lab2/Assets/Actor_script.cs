using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Actor_script : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] public JSON_Parser dataFetch;

    public UnityEngine.Vector3 position;
    public UnityEngine.Quaternion angle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dataFetch == null)
        {
            dataFetch = FindObjectOfType<JSON_Parser>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dataFetch == null) return;

        dataFetch.printPosition();
        dataFetch.printAngle();


        //Debug.Log("position: "+ position.x+ " "+ position.y+ " "+ position.z, null);
        //Debug.Log("angle: " + angle.x + " " + angle.y , null);
    }
}
