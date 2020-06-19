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
        private static float delta = 0.2f;
        private static float time = 0;
        private static float max = 0;
        private static bool change = false;

        private static void reset()
        {
            show = false;
            ch = null;
            delta = 0.2f;
            time = 0;
            max = 0;
            change = false;
        }

        private static void init(OCIChar c)
        {
            show = true;
            ch = c;
            delta = 0.2f;
            time = 0;
            max = c.myGetAnimeLength().second;
            change = false;
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
                if (GUIX.Button("<<", 3))
                {
                    time -= delta * 5;
                    change = true;
                }
                if (GUIX.Button("<", 3))
                {
                    time -= delta;
                    change = true;
                }
                if (GUIX.Button(">", 3))
                {
                    time += delta;
                    change = true;
                }
                if (GUIX.Button(">>", 3))
                {
                    time += delta * 5;
                    change = true;
                }
                if (time < 0)
                {
                    time = 0;
                }
                if (time > max)
                {
                    time = max;
                }
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
            if (show && change && ch!=null)
            {
                ch.mySetAnimeSpeed(0);
                var a = ch.myGetAnime();
                a.normalizedTime = time / max;
                ch.mySetAnime(a);
                ch.mySetFKActive(true);
                ch.myCopyBone();
            }
            change = false;
        }
    }
}
