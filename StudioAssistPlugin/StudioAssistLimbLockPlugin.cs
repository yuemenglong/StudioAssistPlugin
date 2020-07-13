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
    [BepInPlugin("plugin.studio.assist.limb.lock", "StudioAssistLimbLockPlugin", "1.0.0.0")]
    public class StudioAssistLimbLockPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            Tracer.Log("StudioAssistLimbLockPlugin");
        }

        struct LockRecord
        {
            public FkBone.FkBone Bone;
            public Vector3 Pos;
            public Quaternion Rot;
        }

        private static List<FkChara> _headLocks = new List<FkChara>();

        private static List<LockRecord> _lockRecord = new List<LockRecord>();

        private Rect _windowRect = new Rect(
            Screen.width * 0.8f,
            Screen.height * 0.8f,
            Screen.width * 0.2f,
            Screen.height * 0.2f);

        public static bool UseGUI()
        {
            return _lockRecord.Count > 0 || _headLocks.Count > 0;
        }

        public static void ShowWindow(int id)
        {
            _lockRecord.ForEach(r =>
            {
                GUIX.Label(r.Bone.GuideObject.transformTarget.name, 12);
            });
            _headLocks.ForEach(chara =>
            {
                GUIX.Label(chara.Head.GuideObject.transformTarget.name, 12);
            });
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
                else if (go != null && (go.IsHead()))
                {
                    _headLocks.Add(FkCharaMgr.BuildChara(go));
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                _lockRecord.Clear();
                _headLocks.Clear();
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
            _headLocks.ForEach(chara =>
            {
                var go = new GameObject();
                var target = Context.MainCamera().transform.position - chara.Head.Transform.position;
                var v1 = go.transform.forward;
                var v2 = new Vector3(target.x, 0, target.z);
                var axis = Vector3.Cross(v1, v2);
                var angel = Vector3.Angle(v1, v2);
                go.transform.RotateAround(go.transform.position, axis, angel);
                axis = Vector3.Cross(v2, target);
                angel = Vector3.Angle(v2, target);
                go.transform.RotateAround(go.transform.position, axis, angel);
                chara.Head.TurnTo(go.transform.rotation);
                GameObject.Destroy(go);
            });
        }
    }
}
