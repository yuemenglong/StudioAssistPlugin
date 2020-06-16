using System.Collections.Generic;
using StudioAssistPlugin.Util;
using Studio;
using UnityEngine;
using Utility;
using StudioAssistPlugin.FKJoint;

namespace StudioAssistPlugin.FkBone
{
    public class FkCharaMgr
    {
//        public static FkChara[] Charas = new FkChara[0];
//        public static bool IsMarkerEnabled = false;
        private static string regex = @"^cha[FM]\d{2}$";

        public static FkChara BuildChara(GuideObject go)
        {
            var transform = go.transformTarget;
//            var regex = @"^c[fm]_J_Hips$";
            var root = transform.FindParentLoopByRegex(regex);
            if (root == null)
            {
                root = transform.FindChildLoopByRegex(regex);
                if (root == null)
                {
                    return null;
                }
            }
            if (!Context.DicGuideObject().ContainsKey(root))
            {
                return null;
            }
            Tracer.Log(root);
            return new FkChara(root);
        }

        public static FkChara FindSelectChara()
        {
            if (Context.GuideObjectManager().selectObject == null)
            {
                return null;
            }
            var guideObject = Context.GuideObjectManager().selectObject;
            var transform = guideObject.transformTarget.FindParentLoopByRegex(regex);
            if (transform == null)
            {
                transform = guideObject.transformTarget.FindChildLoopByRegex(regex);
                if (transform == null)
                {
                    return null;
                }
            }
            if (!Context.DicGuideObject().ContainsKey(transform))
            {
                return null;
            }
            return new FkChara(transform);
        }

        public static FkChara[] FindSelectCharas()
        {
            var set = new HashSet<GuideObject>();
            if (Context.GuideObjectManager().selectObjects == null)
            {
                return new FkChara[] { };
            }
            foreach (var guideObject in Context.GuideObjectManager().selectObjects)
            {
                var transform = guideObject.transformTarget.FindParentLoopByRegex(regex);
                if (transform == null)
                {
                    transform = guideObject.transformTarget.FindChildLoopByRegex(regex);
                    if (transform == null)
                    {
                        continue;
                    }
                }
                if (!Context.DicGuideObject().ContainsKey(transform))
                {
                    continue;
                }
                set.Add(Context.DicGuideObject()[transform]);
            }
            var list = new List<FkChara>();
            foreach (var guideObject in set)
            {
                list.Add(new FkChara(guideObject.transformTarget));
            }
            return list.ToArray();
        }

        public static FkLimbRotater BuildFkJointRotater(GuideObject go)
        {
            //var chara = FkCharaMgr.BuildChara(go.transformTarget);
            var chara = FkCharaMgr.BuildChara(go);
            var point = chara.DicTransBones[go.transformTarget];
            if (go.IsLimb())
            {
                return new FkLimbRotater(point.Parent.Parent, point.Parent, point);
            }

            return null;
        }
    }
}