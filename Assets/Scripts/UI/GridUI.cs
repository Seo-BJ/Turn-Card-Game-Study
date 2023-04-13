using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridUI : MonoBehaviour
{

    [FormerlySerializedAs("_redCircle")] [SerializeField] Sprite redCircle;
    [FormerlySerializedAs("_blueCircle")] [SerializeField] Sprite blueCircle;
    [FormerlySerializedAs("_yellowCircle")] [SerializeField] Sprite yellowCircle;
    [FormerlySerializedAs("_tileFolder")] [SerializeField] GameObject tileFolder;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void InstantiateGridUI(List<Vector3> targetPosList, string type, List<bool> conditionList)
    {
        for (int i = 0; i < targetPosList.Count; i++)
        {
            InstantiateCircleTile(targetPosList[i], type, conditionList[i]);
        }

    }

    void InstantiateCircleTile(Vector3 targetPos, string type, bool checkCondition)
    {
        GameObject tile = Instantiate(Resources.Load<GameObject>($"Tile/{SelectCircleByType(type, checkCondition)}"), targetPos, Utils.Qi);
        tile.transform.SetParent(tileFolder.transform, true);

    }
    string SelectCircleByType(string type, bool checkCondition)
    {
        if (type == "Attack")
            return "RedCircle";
        else if (type == "Move")
            return "BlueCircle";
        else if (type == "Power")
            return "YellowCircle";
        else
            return null;
    }
    public void DestroyAllColorTile() //parent(폴더)산하의 child를 전부 파괴
    {
        Transform[] childList = tileFolder.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++) // 주의 : 0부터 시작하면 parent도 파괴됨
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }
}
