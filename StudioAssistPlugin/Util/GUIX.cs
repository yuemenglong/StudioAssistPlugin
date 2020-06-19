using System;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using UnityEngine;

namespace StudioAssistPlugin.Util
{
    public static class GUIX
    {
        private static int WIDTH = 40;
        private static int FONTSIZE = 30;
        private static GUILayoutOption[] _w;
        private static Vector2 _pos = Vector2.zero;

        static GUIX()
        {
            _w = new GUILayoutOption[12];
            for (int i = 0; i < _w.Length; i++)
            {
                _w[i] = GUILayout.Width(WIDTH * (i + 1));
            }
        }

        public static void initDefault()
        {
            GUI.skin.label.fontSize = FONTSIZE;
            GUI.skin.label.normal.textColor = Color.white;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            GUI.skin.button.fontSize = FONTSIZE;
            GUI.skin.toggle.fontSize = FONTSIZE;
        }

        public static void resetDefalut()
        {
        }

        public static void Label(String text, int span = 1)
        {
            GUILayout.Label(text, _w[span - 1]);
        }

        public static void BeginHorizontal()
        {
            GUILayout.BeginHorizontal();
        }

        public static void EndHorizontal()
        {
            GUILayout.EndHorizontal();
        }

        public static void BeginVertical(int n = 6)
        {
            GUILayout.BeginVertical(_w[n - 1]);
        }

        public static void EndVertical()
        {
            GUILayout.EndVertical();
        }

        public static void Horizontal(Action action)
        {
            BeginHorizontal();
            action();
            EndHorizontal();
        }

        public static bool Button(String text, int n = 1)
        {
            return GUILayout.Button(text, _w[n - 1]);
        }

        public static bool RepeatButton(String text, int n = 1)
        {
            return GUILayout.RepeatButton(text, _w[n - 1]);
        }

        public static void ScrollView(Action action)
        {
            BeginScrollView();
            action();
            EndScrollView();
        }

        public static void BeginScrollView()
        {
            _pos = GUILayout.BeginScrollView(_pos);
        }

        public static void EndScrollView()
        {
            GUILayout.EndScrollView();
            _pos = Vector2.zero;
        }

        public static bool Toggle(bool value, String text, int n = 1)
        {
            if (value)
            {
                text = "+" + text;
            }
            else
            {
                text = "_" + text;
            }
            return GUILayout.Toggle(value, text, _w[n - 1]);
        }

        public static String TextField(String text, int n = 1)
        {
            return GUILayout.TextField(text, _w[n - 1]);
        }
    }
}