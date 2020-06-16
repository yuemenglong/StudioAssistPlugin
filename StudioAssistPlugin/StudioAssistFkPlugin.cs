using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using StudioAssistPlugin.Util;
using UnityEngine;

namespace StudioAssistPlugin
{
    [BepInPlugin("io.github.yuemenglong.assist.fk", "StudioAssistFKPlugin", "1.0.0.0")]
    public class StudioAssistFKPlugin : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            
        }


        private Vector2 lastMousePos;

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

            float angle = 1.0f;
            float dist = 0.003f;
//            if (Input.anyKeyDown)
//            {
//                UndoRedoHelper.Record();
//            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                angle /= 4;
                dist /= 4;
            }

//            _counter++;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
            {
                go.Reset();
                go.SetSelected();
            }
            //
            else if (Input.GetKey(KeyCode.E) && Input.GetMouseButton(0))
            {
                go.Rotate(angle, 0, 0);
            }
            else if (Input.GetKey(KeyCode.E) && Input.GetMouseButton(1))
            {
                go.Rotate(-angle, 0, 0);
            }
            //
            else if (Input.GetKey(KeyCode.S) && Input.GetMouseButton(0))
            {
                go.Rotate(0, angle, 0);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetMouseButton(1))
            {
                go.Rotate(0, -angle, 0);
            }
            //
            else if (Input.GetKey(KeyCode.D) && Input.GetMouseButton(0))
            {
                go.Rotate(0, 0, angle);
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetMouseButton(1))
            {
                go.Rotate(0, 0, -angle);
            }
            //
            // else if (Input.GetKey(KeyCode.X) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.Forward(go, dist);
            // }
            // else if (Input.GetKey(KeyCode.X) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.Forward(go, -dist);
            // }
            // //
            // else if (Input.GetKey(KeyCode.C) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.Tangent(go, angle);
            // }
            // else if (Input.GetKey(KeyCode.C) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.Tangent(go, -angle);
            // }
            // //
            // else if (Input.GetKey(KeyCode.V) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.Normals(go, angle);
            // }
            // else if (Input.GetKey(KeyCode.V) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.Normals(go, -angle);
            // }
            // //
            // else if (Input.GetKey(KeyCode.B) && Input.GetMouseButton(0))
            // {
            //     FkJointAssist.Revolution(go, angle);
            // }
            // else if (Input.GetKey(KeyCode.B) && Input.GetMouseButton(1))
            // {
            //     FkJointAssist.Revolution(go, -angle);
            // }
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
