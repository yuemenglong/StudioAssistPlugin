using System;
using System.Collections.Generic;
using System.Reflection;
using Studio;
using UnityEngine;
using static Studio.OIBoneInfo;
using static Studio.OICharInfo;

namespace StudioAssistPlugin.Util
{
    public class Context : MonoBehaviour
    {
        private static bool paused = false;
        public static void test()
        {
            TreeNodeObject selectNode = Studio().treeNodeCtrl.selectNode;
            if (!Studio().dicInfo.ContainsKey(selectNode))
            {
                return;
            }
            ObjectCtrlInfo objCtrl = Studio().dicInfo[selectNode];
            if (objCtrl is OCIChar)
            {
                var ch = (OCIChar)objCtrl;
                Tracer.Log(ch.isAnimeMotion, ch.animeSpeed, ch.fkCtrl);
                if (paused == false)
                {
                    ch.animeSpeed = 0;
                    ch.fkCtrl.CopyBone();
                    ch.ActiveKinematicMode(KinematicMode.FK, true, false);
                    paused = true;
                }
                else
                {
                    ch.ActiveKinematicMode(KinematicMode.FK, false, false);
                    ch.animeSpeed = 1;
                    paused = false;
                }
            }
            else
            {
            }
        }
        public static Studio.Studio Studio()
        {
            return Singleton<Studio.Studio>.Instance;
        }

        
        public static OCIChar GetSelectedOCIChar()
        {
            TreeNodeObject selectNode = Studio().treeNodeCtrl.selectNode;
            if (!Studio().dicInfo.ContainsKey(selectNode))
            {
                return null;
            }
            ObjectCtrlInfo objCtrl = Studio().dicInfo[selectNode];
            if (objCtrl is OCIChar)
            {
                var ch = (OCIChar)objCtrl;
                return ch;
            }
            else
            {
                return null;
            }
        }

        public static GuideObjectManager GuideObjectManager()
        {
            return Singleton<GuideObjectManager>.Instance;
        }

        public static Dictionary<Transform, GuideObject> DicGuideObject()
        {
            return GuideObjectManager().GetPrivateField<Dictionary<Transform, GuideObject>>("dicGuideObject");
        }

        public static Camera MainCamera()
        {
            return Studio().cameraCtrl.mainCmaera;
        }

        public static HashSet<GuideObject> HashSelectObject()
        {
            return GuideObjectManager().GetPrivateField<HashSet<GuideObject>>("hashSelectObject");
        }

        public static UndoRedoManager UndoRedoManager()
        {
            return Singleton<UndoRedoManager>.Instance;
        }

        public static OCIChar[] Characters()
        {
            var list = new List<OCIChar>();
            foreach (var objectCtrlInfo in Studio().dicInfo.Values)
            {
                if (objectCtrlInfo.kind == 0)
                {
                    OCIChar ocichar = objectCtrlInfo as OCIChar;
                    if (ocichar != null)
                    {
                        list.Add(ocichar);
                    }
                }
            }
            return list.ToArray();
        }

        private static List<TreeNodeObject> GetCharaNodes<CharaType>()
        {
            var studio = Studio();
            var treeNodeCtrl = studio.treeNodeCtrl;
            List<TreeNodeObject> charaNodes = new List<TreeNodeObject>();

            int n = 0;
            TreeNodeObject nthNode;
            while ((nthNode = treeNodeCtrl.GetNode(n)) != null)
            {
                ObjectCtrlInfo objectCtrlInfo = null;
                if (nthNode.visible && studio.dicInfo.TryGetValue(nthNode, out objectCtrlInfo))
                {
                    if (objectCtrlInfo is CharaType)
                    {
                        charaNodes.Add(nthNode);
                    }
                }
                n++;
            }

            return charaNodes;
        }
    }
}