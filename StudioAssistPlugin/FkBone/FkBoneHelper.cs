using StudioAssistPlugin.Util;
using Studio;
using UnityEngine;
using Utility;
using StudioAssistPlugin.FkBone;

namespace StudioAssistPlugin.FKJoint
{
    public static class FkBoneHelper
    {
        public static void Forward(this GuideObject go, float dist)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).Forward(dist);
        }

        public static void Revolution(this GuideObject go, float angle)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).Revolution(angle);
        }

        public static void Tangent(this GuideObject go, float angle)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).Tangent(angle);
        }

        public static void Normals(this GuideObject go, float angle)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).Normals(angle);
        }

        public static void MoveEndX(this GuideObject go, float dist)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).MoveTo(go.transformTarget.position + new Vector3(dist, 0, 0));
            else
                go.Move(new Vector3(dist * 4, 0, 0));
        }

        public static void MoveEndY(this GuideObject go, float dist)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).MoveTo(go.transformTarget.position + new Vector3(0, dist, 0));
            else
                go.Move(new Vector3(0, dist * 4, 0));
        }

        public static void MoveEndZ(this GuideObject go, float dist)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).MoveTo(go.transformTarget.position + new Vector3(0, 0, dist));
            else
                go.Move(new Vector3(0, 0, dist * 4));
        }

        public static void MoveEnd(this GuideObject go, Vector3 pos)
        {
            if (go.IsLimb())
                FkCharaMgr.BuildFkJointRotater(go).MoveTo(pos);
        }
    }
}