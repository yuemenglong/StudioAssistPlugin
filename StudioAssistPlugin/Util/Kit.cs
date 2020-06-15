using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace StudioAssistPlugin.Util
{
    public static class Kit
    {
        public static string GetGameObjectPath(GameObject obj)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }
            return path;
        }

        public static string GetGameObjectPathAndPos(GameObject obj)
        {
            return String.Format("{0} [{1},{2},{3}]",
                GetGameObjectPath(obj),
                obj.transform.position.x,
                obj.transform.position.y,
                obj.transform.position.z
            );
        }

        public static GameObject[] LoopChildren(GameObject obj, int level = 9999)
        {
            if (level == 0)
            {
                return new GameObject[] { };
            }
            var list = new List<GameObject>();
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                var child = obj.transform.GetChild(i);
                list.Add(child.gameObject);
                list.AddRange(LoopChildren(child.gameObject, level - 1));
            }
            return list.ToArray();
        }

        public static String VecStr(Vector3 v)
        {
            var xa = Vector3.Angle(new Vector3(1, 0, 0), v);
            var ya = Vector3.Angle(new Vector3(0, 1, 0), v);
            var za = Vector3.Angle(new Vector3(0, 0, 1), v);
            return String.Format("({0,6:F3}, {1,6:F3}, {2,6:F3}) <{3,5:F2}, {4,5:F2}, {5,5:F2}> [{6,6:F3}]",
                v.x, v.y, v.z, xa, ya, za, v.magnitude);
        }

        public static String QuatStr(Quaternion q)
        {
            return String.Format("({0,6:F3}, {1,6:F3}, {2,6:F3}), {3,6:F3})",
                q.w, q.x, q.y, q.z);
        }

        public static String StackTrace()
        {
            StackTrace st = new StackTrace();
            var list = new List<string>();
            foreach (var frame in st.GetFrames())
            {
                list.Add(String.Format("\n{0}, {1}", frame.GetMethod().DeclaringType.FullName, frame.GetMethod().Name));
            }
            return String.Join("", list.ToArray());
        }

        public static Vector3 ScreenPoint(Vector3 world)
        {
            Vector3 screen = Camera.main.WorldToScreenPoint(world);
            return new Vector3(screen.x, Screen.height - screen.y, screen.z);
        }

        public static float Angle(float a, float b, float c)
        {
            var cos = (a * a + b * b - c * c) / Mathf.Abs(2 * a * b);
            return Mathf.Acos(cos) / Mathf.PI * 180;
        }

        public static float Angle(Vector3 a, Vector3 b, Vector3 c)
        {
            return Angle(a.magnitude, b.magnitude, c.magnitude);
        }

        public static T GetPrivateField<T>(this object instance, string fieldname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T) field.GetValue(instance);
        }

        public static Assembly LoadAssembly(String path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                var buffer = new Byte[fs.Length];
                var pos = 0;
                while (pos < fs.Length - 1)
                {
                    pos += fs.Read(buffer, pos, (int) (fs.Length - pos));
                }
                var assembly = Assembly.Load(buffer);
                return assembly;
            }
        }

        public static Vector3 MapScreenVecToWorld(Vector3 screenVec, Vector3 pos)
        {
            var screenZ = Context.MainCamera().WorldToScreenPoint(pos).z;
            var screenStart = Vector3.zero;
            screenStart.z = screenZ;
            var screenEnd = screenVec;
            screenEnd.z = screenZ;
            var worldStart = Context.MainCamera().ScreenToWorldPoint(screenStart);
            var worldEnd = Context.MainCamera().ScreenToWorldPoint(screenEnd);
            var end = pos + (worldEnd - worldStart);
            return end;
        }
    }
}