using System;
using System.Collections;
using System.Numerics;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

[System.Serializable]
public class A
{
    public float min_position { get; set; }
    public float max_position { get; set; }
    public float current_position { get; set; }
    public string units { get; set; }
}
[System.Serializable]
public class Axes
{
    public X x { get; set; }
    public Y y { get; set; }
    public Z z { get; set; }
    public A a { get; set; }
    public C c { get; set; }
}
[System.Serializable]
public class C
{
    public float min_position { get; set; }
    public float max_position { get; set; }
    public float current_position { get; set; }
    public string units { get; set; }
}
[System.Serializable]
public class CncMachine
{
    public string machine_id { get; set; }
    public string model { get; set; }
    public Axes axes { get; set; }
    public Spindle spindle { get; set; }
    public ToolChanger tool_changer { get; set; }
    public WorkpieceZero workpiece_zero { get; set; }
    public DateTime timestamp { get; set; }
    public string status { get; set; }
}
[System.Serializable]
public class Root
{
    public CncMachine cnc_machine { get; set; }
}
[System.Serializable]
public class Spindle
{
    public float position { get; set; }
    public float speed { get; set; }
    public string units { get; set; }
}
[System.Serializable]
public class ToolChanger
{
    public float current_tool { get; set; }
    public string position { get; set; }
}
[System.Serializable]
public class WorkpieceZero
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}
[System.Serializable]
public class X
{
    public float min_position { get; set; }
    public float max_position { get; set; }
    public float current_position { get; set; }
    public string units { get; set; }
}
[System.Serializable]
public class Y
{
    public float min_position { get; set; }
    public float max_position { get; set; }
    public float current_position { get; set; }
    public string units { get; set; }
}
[System.Serializable]
public class Z
{
    public float min_position { get; set; }
    public float max_position { get; set; }
    public float current_position { get; set; }
    public string units { get; set; }
}

public class JSON_Parser : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public Uri apiUrl = new Uri("https://karasevv.com/test/mt_data.json");

    [Header("Debug")]
    [SerializeField] private Root parsedData = new Root();
    [SerializeField] private bool autoFetchOnStart = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (autoFetchOnStart)
        {
            StartCoroutine(FetchData());
        }
    }


    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    string responseText = request.downloadHandler.text;
                    ProcessPhpArrayData(responseText);
                    Debug.Log("Данные успешно обновлены!");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Ошибка обработки данных: {e.Message} ");
                }
            }
            else
            {
                Debug.LogError($"Ошибка запроса: {request.error} ");
            }
            
        }
    }

    private void ProcessPhpArrayData(string responceText) 
    {
        parsedData = JsonConvert.DeserializeObject<Root>(responceText);
        Debug.Log("Данные успешно обработанны. Модель машины: " + parsedData.cnc_machine.model);
    }

    public UnityEngine.Vector3 GetModelPosition()
    {
        UnityEngine.Vector3 responce = new UnityEngine.Vector3(parsedData.cnc_machine.axes.x.current_position, parsedData.cnc_machine.axes.y.current_position, parsedData.cnc_machine.axes.z.current_position);
        Debug.Log("Данные успешно переданы!" + responce.x);
        return responce;
    }

    public UnityEngine.Vector3 GetAngle()
    {
        UnityEngine.Vector3 responce = new UnityEngine.Vector3(parsedData.cnc_machine.axes.a.current_position, parsedData.cnc_machine.axes.c.current_position, 0);
        return responce;
    }

    public void printPosition()
    {
        UnityEngine.Vector3 responce = new UnityEngine.Vector3(parsedData.cnc_machine.axes.x.current_position, parsedData.cnc_machine.axes.y.current_position, parsedData.cnc_machine.axes.z.current_position);
        Debug.Log("X:" + responce.x + " Y: " + responce.y + " Z: " + responce.x + " Z: ");
    }

    public void printAngle()
    {
        UnityEngine.Vector3 responce = new UnityEngine.Vector3(parsedData.cnc_machine.axes.a.current_position, parsedData.cnc_machine.axes.c.current_position, 0);
        Debug.Log("X:" + responce.x + " Y: " + responce.y + " Z: " + responce.x + " Z: ");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
