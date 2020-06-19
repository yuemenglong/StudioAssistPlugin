using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using Studio;
using StudioAssistPlugin.FkBone;
using StudioAssistPlugin.Util;
using UnityEngine;

namespace StudioAssistPlugin
{
    [BepInPlugin("plugin.studio.assist.anime", "StudioAssistAnimePlugin", "1.0.0.0")]
    public class StudioAssistAnimePlugin : BaseUnityPlugin
    {
        // Awake is called once when both the game and the plug-in are loaded
        void Awake()
        {
            Tracer.Log("StudioAssistAnimePlugin");
        }

        private static bool show = false;
        private static OCIChar ch = null;
        private static float delta = 1;
        private static float time = 0;
        private static float max = 0;

        private static String deltaS = "1.0";

        private static void reset()
        {
            show = false;
            ch = null;
            delta = 1;
            time = 0;
            max = 0;
            deltaS = "1.0";
        }

        private static void init(OCIChar c)
        {
            show = true;
            ch = c;
            delta = 1;
            time = 0;
            max = c.myGetAnimeLength().second;
            deltaS = "1.0";
        }

        public static bool UseGUI()
        {
            return show;
        }

        public static void ShowWindow(int id)
        {
            if (ch == null)
            {
                return;
            }
            GUIX.Horizontal(() =>
            {
                GUIX.Label(time.ToString(), 4);
                GUIX.Label("/", 4);
                GUIX.Label(max.ToString(), 4);
            });
            GUIX.Horizontal(() =>
            {
                GUIX.Button("<", 4);
                deltaS = GUIX.TextField(deltaS, 4);
                GUIX.Button(">", 4);
            });
            try
            {
                delta = float.Parse(deltaS);
            }
            catch (Exception e)
            {

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
            if (Input.GetKey(KeyCode.O))
            {
                if (show)
                {
                    reset();
                    return;
                }

                var c = Context.GetSelectedOCIChar();
                if (c == null)
                {
                    return;
                }
                init(c);
            }
        }
    }
}
