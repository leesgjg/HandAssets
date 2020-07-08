//Saves an instanteneous log of data containing information such as the current altitude, the number of falls, the framerate, etc.

using UnityEngine;
using System;
using System.IO;


public class CommonDataLogger : MonoBehaviour
{

    public GameObject main_camera;
    public GameObject left_tracker;
    public GameObject right_tracker;

    private string path;

    string LocalPosition_to_string(GameObject _gameObject)
    {
        string result = "(";
        float _x = _gameObject.transform.localPosition.x;
        float _y = _gameObject.transform.localPosition.y;
        float _z = _gameObject.transform.localPosition.z;

        result += _x;
        result += ",";
        result += _y;
        result += ",";
        result += _z;
        result += ")";

        return result;
    }

    string LocalRotation_to_string(GameObject _gameObject)
    {
        string result = "(";
        float _x = _gameObject.transform.localRotation.x;
        float _y = _gameObject.transform.localRotation.y;
        float _z = _gameObject.transform.localRotation.z;
        float _w = _gameObject.transform.localRotation.w;

        result += _x;
        result += ",";
        result += _y;
        result += ",";
        result += _z;
        result += ",";
        result += _w;
        result += ")";

        return result;
    }

    // Use this for initialization
    void Start()
    {
        if (main_camera == null)
            throw new NullReferenceException("main camera tracker missing");

        path = "Log-" + DateTime.Now + ".csv";

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (main_camera == null)
        {
            main_camera = GameObject.Find("Camera (eye)");
            string create_text = "AppTime;HMDPos;HMDRot;LeftPos;LeftRot;RightPos;RightRot;" + Environment.NewLine;
            File.WriteAllText(path, create_text);
        }

        string new_line = Time.realtimeSinceStartup.ToString();
        new_line += LocalPosition_to_string(main_camera);
        new_line += ";";
        new_line += LocalRotation_to_string(main_camera);
        new_line += ";";

        if (left_tracker != null && left_tracker.activeInHierarchy)
        {
            new_line += LocalPosition_to_string(left_tracker);
            new_line += ";";
            new_line += LocalRotation_to_string(left_tracker);
            new_line += ";";
        }
        else
        {
            new_line += ";;";
        }

        if (right_tracker != null && right_tracker.activeInHierarchy)
        {
            new_line += LocalPosition_to_string(right_tracker);
            new_line += ";";
            new_line += LocalRotation_to_string(right_tracker);
            new_line += ";";
        }
        else
        {
            new_line += ";;";
        }

        new_line += Environment.NewLine;
        File.AppendAllText(path, new_line);
    }
}
