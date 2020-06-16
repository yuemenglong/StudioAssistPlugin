using System.Collections.Generic;
using StudioAssistPlugin.FkBone;
using Studio;
using UnityEngine;

namespace StudioAssistPlugin.Util
{
    public static class UndoRedoHelper
    {
        private static readonly Dictionary<int, GuideObject> _targets = new Dictionary<int, GuideObject>();
        private static readonly Dictionary<int, Vector3> _oldRots = new Dictionary<int, Vector3>();
        private static readonly Dictionary<int, Vector3> _oldPoss = new Dictionary<int, Vector3>();

        private static string _mode = null;

        private static void Record(GuideObject selectObject)
        {
            if (selectObject.enablePos || selectObject.enableRot)
            {
                _targets.Add(selectObject.dicKey, selectObject);
                if (_mode.Equals("pos"))
                {
                    _oldPoss.Add(selectObject.dicKey, selectObject.changeAmount.pos);
                }
                else if (_mode.Equals("rot"))
                {
                    _oldRots.Add(selectObject.dicKey, selectObject.changeAmount.rot);
                }
            }
        }

        public static void RecordAll(string mode)
        {
            _mode = mode;
            _oldPoss.Clear();
            _oldRots.Clear();
            _targets.Clear();
            var boneSet = new HashSet<GuideObject>();
            foreach (var selectObject in FkCharaMgr.FindSelectChara().DicGuideBones.Keys)
            {
                Record(selectObject);
                boneSet.Add(selectObject);
            }
            Context.GuideObjectManager().selectObjects.Filter(go => !boneSet.Contains(go))
                .Foreach(go => Record(go));
        }

        public static void Finish()
        {
            var list = new List<GuideCommand.EqualsInfo>();
            foreach (var kv in _targets)
            {
                var info = new GuideCommand.EqualsInfo();
                info.dicKey = kv.Key;
                if (_mode.Equals("pos"))
                {
                    info.oldValue = _oldPoss[kv.Key];
                    info.newValue = kv.Value.changeAmount.pos;
                }
                else if (_mode.Equals("rot"))
                {
                    info.oldValue = _oldRots[kv.Key];
                    info.newValue = kv.Value.changeAmount.rot;
                }
                list.Add(info);
            }
            var arr = list.ToArray();
            if (_mode.Equals("pos"))
            {
                Context.UndoRedoManager().Push(new GuideCommand.MoveEqualsCommand(arr));
            }
            else if (_mode.Equals("rot"))
            {
                Context.UndoRedoManager().Push(new GuideCommand.RotationEqualsCommand(arr));
            }
        }
    }
}