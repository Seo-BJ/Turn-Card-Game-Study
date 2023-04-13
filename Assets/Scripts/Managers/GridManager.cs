using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;


public class GridManager : MonoBehaviour
{
    [FormerlySerializedAs("_redCircle")] [SerializeField] Sprite redCircle;
    [FormerlySerializedAs("_blueCircle")] [SerializeField] Sprite blueCircle;
    [FormerlySerializedAs("_yellowCircle")] [SerializeField] Sprite yellowCircle;

    [FormerlySerializedAs("_tileFolder")] [SerializeField] GameObject tileFolder;
    [FormerlySerializedAs("_grid")] [SerializeField] Grid grid;
    public List<Vector3> test = new List<Vector3>();
    Vector3 _pos = Vector3.zero;
    int[] _dir = { 1, 2, 3, 4, 5, 6 };
    int[] _min = { 1, 1, 1, 1, 1, 1 };
    int[] _max = { 1, 1, 1, 1, 1, 1 };
    public static GridManager Inst { get; private set; }
    void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        test = IntToCellWorldPos(_pos, _dir, _min, _max);
    }


    void Update()
    {
        
    }
    


    public void InstantiateGridUI(List<Vector3> targetPosList, string type)
    {
        for (int i = 0; i < targetPosList.Count; i++)
        {
            InstantiateCircleTile(targetPosList[i], type);
        }

    }

    void InstantiateCircleTile(Vector3 targetPos, string type)
    {
        GameObject tile = Instantiate(Resources.Load<GameObject>($"Tile/{SelectCircleByType(type)}"), targetPos, Utils.Qi);
        tile.transform.SetParent(tileFolder.transform, true);

    }
    string SelectCircleByType(string type)
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
    public void DestroyAllColorTile() //parent(����)������ child�� ���� �ı�
    {
        Transform[] childList = tileFolder.GetComponentsInChildren<Transform>();
        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++) // ���� : 0���� �����ϸ� parent�� �ı���
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
    }








    public List<Vector3> IntToCellWorldPos(Vector3 pos, int[] dir, int[] min, int[] max)
    {
        List<Vector3> targetPos = new List<Vector3>();
        for (int i = 0; i < 6; i++)
        {
            if (dir[i] != 0)
            {
                for (int j = min[i]; j <= max[i]; j++)
                {
                    Vector3 resultpos = pos + IntToWorldPosition(dir[i], j); ;
                    if (Mathf.Abs(resultpos.x) + Mathf.Abs(resultpos.y) <= 36)
                    {
                        targetPos.Add(resultpos);
                    }
                }
            }
        }
        return targetPos;
    }
    
    Vector3 CellPosToWorldPos(Vector3 cellPos)
    {
        Vector3 worldPos = new Vector3(0, 0, 0);
        if (cellPos.y % 2 == 0) 
        {
            worldPos.y = cellPos.y * 6;
            worldPos.x = cellPos.x * 12;
        }
        else
        {
            worldPos.y = cellPos.y * 6;
            worldPos.x = cellPos.x * 12 + 6;
        }
        return worldPos; 

    }
    public Vector3 MousePosToCellWorldPos(Vector3 mousePos)
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cellPos = grid.WorldToCell(mousePos);
        Vector3 worldPos = CellPosToWorldPos(cellPos);
        return worldPos;
    }
    public EnemyStat DetectEnemy(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            GameObject clickObj = hit.transform.gameObject;
            if (clickObj.tag == "Enemy")
            {
                EnemyStat tagetStat = clickObj.GetComponent<EnemyStat>();
                return tagetStat;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    List<Vector3> IsThereObstacleOnMainCell(Vector3 pos, int direnction, int min, int max) //min, max�� main cell ������.
    {//���̾� ����ũ �̿��ϴ� ���  
        List<Vector3> targets = new List<Vector3>(); //�����ɽ�Ʈ�� ��ġ(origin)�� ���� ����Ʈ
        List<Vector3> canmovepos = new List<Vector3>();
        RaycastHit2D hit;

        for (int i = min; i <= max; i++)
        {
            Vector3 result = pos + IntToWorldPosition(direnction, i);
            targets.Add(result);
        }
        foreach (Vector3 target in targets)
        {
            hit = Physics2D.Raycast(target, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject clickObj = hit.transform.gameObject;
                if (clickObj.tag == "Enemy" || clickObj.tag == "Obstacle")
                {
                    canmovepos.Add(clickObj.transform.position);
                }
            }
        }
        return canmovepos;
    }

    Vector3 IntToWorldPosition(int direction, int distance) //����, ����-> �������� * ũ�� ���·� ��ȯ
    {
        Vector3 result = new Vector3(0, 0, 0);

        if (direction == 1)
        {
            result += new Vector3(6, 6, 0) * distance;
        }
        else if (direction == 2)
        {
            result += new Vector3(12, 0, 0) * distance;
        }
        else if (direction == 3)
        {
            result += new Vector3(6, -6, 0) * distance;
        }
        else if (direction == 4)
        {
            result += new Vector3(-6, -6, 0) * distance;
        }
        else if (direction == 5)
        {
            result += new Vector3(-12, 0, 0) * distance;
        }
        else if (direction == 6)
        {
            result += new Vector3(-6, 6, 0) * distance;
        }

        return result;
    }


}
