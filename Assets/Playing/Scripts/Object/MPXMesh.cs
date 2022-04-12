using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MegaShape))]
public class MPXMesh : MPXDrawObject
{
    public const string MY_TAG = "Wall";

    public const string PROPERTY_THICKNESS = "Thickness";
    public const string PROPERTY_HEIGHT = "Height";

    public const string PROPERTY_START_POS_X = "StartPosX";
    public const string PROPERTY_START_POS_Y = "StartPosY";
    public const string PROPERTY_START_POS_Z = "StartPosZ";

    public const string PROPERTY_END_POS_X = "EndPosX";
    public const string PROPERTY_END_POS_Y = "EndPosY";
    public const string PROPERTY_END_POS_Z = "EndPosZ";

    public MegaShape shape;

    public Vector3 StartPos;
    public Vector3 EndPos;

    public MeshCollider ColMesh;

    private void Start()
    {
        tag = MY_TAG;
        Draw();
    }


    public override void Init()
    {
        base.Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag(MY_TAG))
        {
            //print(collision.transform.name);
        }
    }

    public override void Remove()
    {
        if (Col != null)
            Destroy(Col);

        base.Remove();
    }

    public override void Draw(List<MPXProperty> properties)
    {
        base.Draw(properties);
    }

    public override void Draw()
    {
        if (shape != null && (EndPos - StartPos).sqrMagnitude > 0.1f)
        {
            transform.position = Vector3.Lerp(StartPos, EndPos, 0.5f);
            Vector3 pos = transform.position;

            shape.splines[0].knots[0].p = StartPos - pos;
            shape.splines[0].knots[0].invec = StartPos - pos;
            shape.splines[0].knots[0].outvec = StartPos - pos;

            shape.splines[0].knots[1].p = EndPos - pos;
            shape.splines[0].knots[1].invec = EndPos - pos;
            shape.splines[0].knots[1].outvec = EndPos - pos;

            shape.handleType = MegaHandleType.Free;
            shape.cap = true;
            shape.drawKnots = false;
            shape.drawHandles = false;
            shape.drawspline = false;

            shape.makeMesh = true;
            shape.meshType = MeshShapeType.Box;

            MPXProperty p1 = GetProperty(PROPERTY_THICKNESS);
            MPXProperty p2 = GetProperty(PROPERTY_HEIGHT);

            shape.boxwidth = p1.Value;
            shape.boxheight = p2.Value;
            shape.offset = p2.Value / 2 * -1;

            shape.ReCalcLength();
            shape.ResetMesh();

            if (ColMesh != null)
                Destroy(ColMesh);

            ColMesh = gameObject.AddComponent<MeshCollider>();
            ColMesh.convex = true;
            //Col.isTrigger = true;
            ColMesh.sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        }
    }
}
