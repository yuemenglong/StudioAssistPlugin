using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using StudioAssistPlugin.Util;
using UnityEngine;

namespace StudioAssistPlugin
{
    [BepInPlugin("plugin.studio.assist.ik", "StudioAssistIKPlugin", "1.0.0.0")]
    public class StudioAssistIKPlugin : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            Tracer.Log("StudioAssistIKPlugin");
        }


        private Vector2 lastMousePos;

        private void Update()
        {
            try
            {
                // Rotate();
               Move();
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

        private void Move(Vector3 delta)
        {
//            var go = Context.GuideObjectManager().selectObject;
            //            if (go == null || !go.enablePos)
            //            {
            //                return;
            //            }

            Camera camera = MainCamera();
            if (camera == null)
            {
                return;
            }

            Vector3 vector31 = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, Input.mousePosition.z);
            Ray ray = camera.ScreenPointToRay(vector31);
            ray.direction = new Vector3(ray.direction.x, 0, ray.direction.z);
            Vector3 vector32 = ray.direction * -1 * delta.z;
            ray.direction = Quaternion.LookRotation(ray.direction) * Vector3.right;
            Vector3 vector33 = vector32 + ray.direction * -1 * delta.x;
            vector33.y = delta.y;
            delta = vector33;
            delta = delta * 20.0f;
            Context.GuideObjectManager().selectObjects.Foreach(go =>
            {
                if (go.enablePos)
                {
                    go.Move(delta);
                }
            });
        }

        private void Move()
        {
            Vector2 sub = GetMousePos() - lastMousePos;
            if (Input.GetKey(KeyCode.G))
            {
                Vector3 delta = new Vector3(-sub.x, 0, -sub.y);
                Move(delta);
            }
            else if (Input.GetKey(KeyCode.H))
            {
                Vector3 delta = new Vector3(0, sub.y, 0);
                Move(delta);
            }
            lastMousePos = GetMousePos();
        }
        
        private Vector2 GetMousePos()
        {
            Vector2 vector2 = Input.mousePosition;
            return new Vector2(vector2.x / Screen.width, vector2.y / Screen.height);
        }
    }
}
