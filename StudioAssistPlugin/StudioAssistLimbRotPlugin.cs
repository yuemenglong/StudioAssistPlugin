using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using StudioAssistPlugin.FkBone;
using StudioAssistPlugin.Util;
using UnityEngine;

namespace StudioAssistPlugin
{
    [BepInPlugin("plugin.assist.limb.rot", "StudioAssistLimbRotPlugin", "1.0.0.0")]
    public class StudioAssistLimbRotPlugin : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
              Tracer.Log("StudioAssistLimbRotPlugin");
        }

        private void Update()
        {
            
            try
            {
                Rotate();
            }
            catch (Exception e)
            {
                Tracer.Log(e);
            }
        }
        public static Camera MainCamera()
        {
            return Context.Studio().cameraCtrl.mainCmaera;
        }

        private void Rotate()
        {
            var go = Context.GuideObjectManager().selectObject;
            if (go == null)
            {
                return;
            }
            if (!go.IsLimb())
            {
                return;
            }

            float angle = 1.0f;
            float dist = 0.003f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                angle /= 4;
                dist /= 4;
            }
            if (Input.GetKey(KeyCode.X) && Input.GetMouseButton(0))
            {
                FkCharaMgr.BuildFkJointRotater(go).Forward(dist);
            }
            else if (Input.GetKey(KeyCode.X) && Input.GetMouseButton(1))
            {
                FkCharaMgr.BuildFkJointRotater(go).Forward(-dist);
            }
            //
            else if (Input.GetKey(KeyCode.C) && Input.GetMouseButton(0))
            {
                FkCharaMgr.BuildFkJointRotater(go).Tangent(angle);
            }
            else if (Input.GetKey(KeyCode.C) && Input.GetMouseButton(1))
            {
                FkCharaMgr.BuildFkJointRotater(go).Tangent(-angle);
            }
            //
            else if (Input.GetKey(KeyCode.V) && Input.GetMouseButton(0))
            {
                FkCharaMgr.BuildFkJointRotater(go).Normals(angle);
            }
            else if (Input.GetKey(KeyCode.V) && Input.GetMouseButton(1))
            {
                FkCharaMgr.BuildFkJointRotater(go).Normals(-angle);
            }
            //
            else if (Input.GetKey(KeyCode.B) && Input.GetMouseButton(0))
            {
                FkCharaMgr.BuildFkJointRotater(go).Revolution(angle);
            }
            else if (Input.GetKey(KeyCode.B) && Input.GetMouseButton(1))
            {
                FkCharaMgr.BuildFkJointRotater(go).Revolution(-angle);
            }
            // //
            // else if (Input.GetKey(KeyCode.G) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.MoveEndX(go, dist);
            // }
            // else if (Input.GetKey(KeyCode.G) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.MoveEndX(go, -dist);
            // }
            // //
            // else if (Input.GetKey(KeyCode.Y) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.MoveEndY(go, dist);
            // }
            // else if (Input.GetKey(KeyCode.Y) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.MoveEndY(go, -dist);
            // }
            // //
            // else if (Input.GetKey(KeyCode.H) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.MoveEndZ(go, dist);
            // }
            // else if (Input.GetKey(KeyCode.H) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.MoveEndZ(go, -dist);
            // }
            //
            //            else
            //            {
            //                if (_counter > 1)
            //                {
            //                    UndoRedoHelper.Finish();
            ////                    FinishRotate();
            //                }
            //
            //                _counter = 0;
            ////                _oldRot = CollectOldRot();
            //            }
        }

        private Vector2 GetMousePos()
        {
            Vector2 vector2 = Input.mousePosition;
            return new Vector2(vector2.x / Screen.width, vector2.y / Screen.height);
        }
    }
}
