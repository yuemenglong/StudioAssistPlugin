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
            if (!go.IsLimb())
            {
                return;
            }
            var rotater = FkCharaMgr.BuildFkJointRotater(go);
            if (rotater == null)
            {
                return;
            }
            rotater.Forward(dist);
        }

        public static void Revolution(this GuideObject go, float angle)
        {
            if (!go.IsLimb())
            {
                return;
            }
            var rotater = FkCharaMgr.BuildFkJointRotater(go);
            if (rotater == null)
            {
                return;
            }
            rotater.Revolution(angle);
        }

        public static void Tangent(this GuideObject go, float angle)
        {
            if (!go.IsLimb())
            {
                return;
            }
            var rotater = FkCharaMgr.BuildFkJointRotater(go);
            if (rotater == null)
            {
                return;
            }
            rotater.Tangent(angle);
        }

        public static void Normals(this GuideObject go, float angle)
        {
            if (!go.IsLimb())
            {
                return;
            }
            var rotater = FkCharaMgr.BuildFkJointRotater(go);
            if (rotater == null)
            {
                return;
            }
            rotater.Normals(angle);
        }

    }
}