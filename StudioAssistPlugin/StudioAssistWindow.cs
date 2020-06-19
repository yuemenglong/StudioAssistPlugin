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
    }
}
