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
    [BepInPlugin("plugin.studio.assist.window", "StudioAssistLimbRotPlugin", "1.0.0.0")]
    public class StudioAssistWindow : BaseUnityPlugin
    {
        void Awake()
        {
            Tracer.Log("StudioAssistWindow");
        }

        private Rect _windowRect = new Rect(
            Screen.width * 0.8f,
            Screen.height * 0.8f,
            Screen.width * 0.2f,
            Screen.height * 0.2f);

        private int wid = 13579;

        private void OnGUI()
        {
            var useGUI = StudioAssistLimbLockPlugin.useGUI();
            if (useGUI)
            {
                _windowRect = GUI.Window(wid, _windowRect, ShowWindow, "LimbLocker");
            }
        }

        private void ShowWindow(int id)
        {
            try
            {
                StudioAssistLimbLockPlugin.ShowWindow(id);
            }
            catch (Exception e)
            {
                Tracer.Log(e);
            }
        }

        private void Update()
        {
            try
            {
                InnerUpdate();
            }
            catch (Exception e)
            {
                Tracer.Log(e);
            }
        }

        private void InnerUpdate()
        {
            if (Input.GetKeyDown(KeyCode.T) &&
                (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
            {
                var chara = FkCharaMgr.FindSelectChara();
                if (chara != null)
                {
                    chara.Limbs().Foreach(bone =>
                    {
                        var r = new LockRecord();
                        r.Bone = bone;
                        r.Pos = bone.Transform.position;
                        r.Rot = bone.Transform.rotation;
                        _lockRecord.Add(r);
                    });
                }
            }
            else if (Input.GetKeyDown(KeyCode.T) &&
                (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                var go = Context.GuideObjectManager().selectObject;
                if (go != null && go.IsLimb())
                {
                    var chara = FkCharaMgr.FindSelectChara();
                    var bone = chara.DicGuideBones[go];
                    var r = new LockRecord();
                    r.Bone = bone;
                    r.Pos = bone.Transform.position;
                    r.Rot = bone.Transform.rotation;
                    _lockRecord.Add(r);
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                _lockRecord.Clear();
            }
            _lockRecord.ForEach(r =>
            {
                if (r.Pos != r.Bone.Transform.position)
                {
                    r.Bone.MoveTo(r.Pos);
                }
                if (r.Rot != r.Bone.Transform.rotation)
                {
                    r.Bone.TurnTo(r.Rot);
                }
            });
        }
    }
}
