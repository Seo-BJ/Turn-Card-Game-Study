using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Gets the filename with the slot appended.
    string Filename
    {
        get { return "SaveFile" + SaveSlot.Slot + ".es3"; }
    }

    void Awake()
    {
        if(ES3.KeyExists("position", Filename))
            transform.position = ES3.Load<Vector3>("position", Filename);
    }

    // This will be called when the application quits.
    // Note that this isn't called on all platforms.
    void OnApplicationQuit()
    {
        // Save our data, appending our save slot to the filename.
        ES3.Save("position", transform.position, Filename);
    }
}
