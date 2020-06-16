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
    [BepInPlugin("plugin.studio.assist.select", "StudioAssistFKPlugin", "1.0.0.0")]
    public class StudioAssistSelectPlugin : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            
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
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
            {
                var minDist = double.MaxValue;
                FkBone.FkBone minBone = null;
                FkCharaMgr.FindSelectCharas().Foreach(c =>
                {
                    c.MainBones().Foreach(b =>
                    {
                        var screenPoint = Context.MainCamera().WorldToScreenPoint(b.Transform.position);
                        var dist = (screenPoint - Input.mousePosition).magnitude;
                        if (dist < minDist)
                        {
                            minDist = dist;
                            minBone = b;
                        }
                    });
                });
                if (minBone != null)
                {
                    Context.GuideObjectManager().selectObject = minBone.GuideObject;
                }
            }
        }
    }
}
