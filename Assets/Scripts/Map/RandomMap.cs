using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEditor.PlayerSettings;
using Random = UnityEngine.Random;

public class RandomMap : MonoBehaviour
{
    public Dictionary<List<int>, List<int>> MapConnectDic = new Dictionary<List<int>, List<int>>();
    [FormerlySerializedAs("_xcoordList")] public List<int> xcoordList;
    [FormerlySerializedAs("_circle")] [SerializeField] GameObject circle;
    public Vector3 scale = new Vector3(20, 20, 1);
    void Start()
    {
        xcoordList = MakeXCoordList();
        InstantiateCircle();
    }
    List<int> MakeXCoordList()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 15; i++)
        {
            int randomInt = Random.Range(2, 6);
            list.Add(randomInt);
        }
        return list;
    }
    void InstantiateCircle()
    {
        for(int i = 0; i < 15; i++) 
        {
            for(int k = 0; k < xcoordList[i]; k++)
            {
                List<Vector3> targetPos = ChooseXYCoord(xcoordList[i], i - 8);
                Instantiate(circle, targetPos[k], Utils.Qi);
            }
            


        }



    }
    List<Vector3> ChooseXYCoord(int x, int y)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 1; i < x + 1; i++)
        {
            Vector3 spawmPos = new Vector3(100*y, 100*i, 0);
            result.Add(spawmPos);
        }
        return result;
    }
}
