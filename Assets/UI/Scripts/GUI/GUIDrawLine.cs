using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIDrawLine : GUIPlayWindow
{
    public RectTransform ArrowTr;
    public RectTransform LineTr;

    private float StartSpace = 0.1f;
    private float EndSpace = 0.1f;

    private float LineWidth = 10.0f;
    
    /// <summary>
    /// if it create,arrow must be in the last index
    /// 생성되었다면 항상 마지막 인덱스에는 arrow가 들어가 있어야 한다.
    /// </summary>
    List<RectTransform> LineList = new List<RectTransform>();

    /// <summary>
    /// Last Vector2 Position
    /// 마지막에 작업한 Vector 위치
    /// </summary>
    [SerializeField]
    private Vector2 oriStartVector;
    [SerializeField]
    private Vector2 oriEndVector;
    [SerializeField]
    private Vector2 startVector;
    [SerializeField]
    private Vector2 endVector;
    [SerializeField]
    private bool isEnd;

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void Init()
    {
        base.Init();

        ArrowTr.gameObject.SetActive(false);
        LineTr.gameObject.SetActive(false);
    }
    /// <summary>
    /// clear List
    /// 처음 오브젝트(index 0), 마지막 오브젝트(index n)은 삭제하면 안되므로 제외하고 삭제한다.
    /// </summary>
    public void Clear()
    {
        for (int i = 1; i < LineList.Count - 1; i++)
        {
            Destroy(LineList[i].gameObject);
        }
        LineList.Clear();
        Init();
    }
    /// <summary>
    /// Draw only one line
    /// </summary>
    public void DrawLine()
    {
        float width = Vector3.Distance(startVector, endVector);

        LineTr.SetParent(MYPanel.transform);
        LineTr.localPosition = startVector;
        LineTr.rotation = GetRotation(startVector, endVector);
        LineTr.sizeDelta = new Vector2(width, LineWidth);
        LineTr.localScale = Vector3.one;


        LineTr.gameObject.SetActive(true);
        //LineList.Add(LineTr);

        if (isEnd)
        {
            ArrowTr.SetParent(MYPanel.transform);
            ArrowTr.eulerAngles = LineTr.eulerAngles;
            ArrowTr.localPosition = endVector;
            ArrowTr.localScale = Vector3.one;
            ArrowTr.gameObject.SetActive(true);
            //LineList.Add(ArrowTr);
        }
    }

    /// <summary>
    /// Draw a line connecting the start and end points.(Vector2)
    /// if 'isEnd' is true, draw an Arrow. but if not, draw only line.
    /// </summary>
    /// <param name="startLine"></param>
    /// <param name="endLine"></param>
    /// <param name="isend"></param>
    public void DrawLine(Vector2 startLine, Vector2 endLine, bool isLast = true)
    {
        //startVector = startLine - OffsetVec;
        //endVector = endLine - OffsetVec;
        this.oriStartVector = startLine;
        this.oriEndVector = endLine;
        this.isEnd = isLast;
        SetSpace();

        DrawLine();
    }

    //public void AddLine()
    //{
    //    //startVector = startVector - Offset;
    //    //endVector = endVector - Offset;
    //    float width = Vector3.Distance(startVector, endVector);

    //    RectTransform lineTr = Instantiate(LineTr);
    //    lineTr.SetParent(MYPanel.transform);
    //    lineTr.localPosition = startVector;
    //    //lineTr.eulerAngles = new Vector3(0, 0, angle);
    //    Quaternion q = Quaternion.LookRotation(endVector - startVector);
    //    Vector3 rot = q.eulerAngles;
    //    lineTr.eulerAngles = new Vector3(0, 0, rot.x * -1);
    //    lineTr.sizeDelta = new Vector2(width, LineWidth);
    //    lineTr.localScale = Vector3.one;
    //    lineTr.gameObject.SetActive(true);
    //    LineList.Add(lineTr);

    //    if (isEnd)
    //    {
    //        ArrowTr.SetParent(MYPanel.transform);
    //        //arrowTr.eulerAngles = new Vector3(0, 0, angle);
    //        ArrowTr.eulerAngles = LineTr.eulerAngles;
    //        ArrowTr.localPosition = endVector;
    //        ArrowTr.localScale = Vector3.one;
    //        ArrowTr.gameObject.SetActive(true);
    //        LineList.Add(ArrowTr);
    //    }
    //}

    /// <summary>
    /// Draw a line connecting the start and end points.(Vector2)
    /// if 'isEnd' is true, draw an Arrow. but if not, draw only line.
    /// </summary>
    /// <param name="startLine"></param>
    /// <param name="endLine"></param>
    /// <param name="isend"></param>
    //public void AddLine(Vector2 startLine, Vector2 endLine, bool isLast = true)
    //{    
    //    //startVector = startLine - OffsetVec;
    //    //endVector = endLine - OffsetVec;
    //    this.startVector = startLine;
    //    this.endVector = endLine;
    //    this.isEnd = isLast;
    //    AddLine();
    //}

    private void SetSpace()
    {
        startVector = Vector2.Lerp(oriStartVector, oriEndVector, StartSpace);
        endVector = Vector2.Lerp(oriStartVector, oriEndVector, 1f - EndSpace);
    }

    public override void SetOffset()
    {
        base.SetOffset();
        //this.transform.localPosition = Offset;
    }

    Quaternion GetRotation(Vector2 start, Vector2 end)
    {
        Vector2 vec = end - start;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public MPXObject start;
    public MPXObject end;

    private void Update()
    {
        if (IsShow && start != null && end != null)
        {
            Vector3 startPos = transform.InverseTransformPoint(MPXCamera.CurrentCamera.WorldToScreen(start.transform.position));
            Vector3 endPos = transform.InverseTransformPoint(MPXCamera.CurrentCamera.WorldToScreen(end.transform.position));

            DrawLine(startPos, endPos);
        }
    }
}
