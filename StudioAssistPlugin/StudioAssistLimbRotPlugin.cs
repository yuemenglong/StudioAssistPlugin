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
            if (Context.GuideObjectManager() == null)
            {
                return;
            }
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
            float dist = 0.02f;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                angle /= 4;
                dist /= 4;
            }
            if (Input.GetKey(KeyCode.X) && Input.GetMouseButton(0))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Forward(dist);
            }
            else if (Input.GetKey(KeyCode.X) && Input.GetMouseButton(1))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Forward(-dist);
            }
            //
            else if (Input.GetKey(KeyCode.C) && Input.GetMouseButton(0))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Tangent(angle);
            }
            else if (Input.GetKey(KeyCode.C) && Input.GetMouseButton(1))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Tangent(-angle);
            }
            //
            else if (Input.GetKey(KeyCode.V) && Input.GetMouseButton(0))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Normals(angle);
            }
            else if (Input.GetKey(KeyCode.V) && Input.GetMouseButton(1))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Normals(-angle);
            }
            //
            else if (Input.GetKey(KeyCode.B) && Input.GetMouseButton(0))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Revolution(angle);
            }
            else if (Input.GetKey(KeyCode.B) && Input.GetMouseButton(1))
            {
                var rotater = FkCharaMgr.BuildFkJointRotater(go);
                if (rotater == null)
                {
                    return;
                }
                rotater.Revolution(-angle);
            }
        }

        private Vector2 GetMousePos()
        {
            Vector2 vector2 = Input.mousePosition;
            return new Vector2(vector2.x / Screen.width, vector2.y / Screen.height);
        }
    }
}
