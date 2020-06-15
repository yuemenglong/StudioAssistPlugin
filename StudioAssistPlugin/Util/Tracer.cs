using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StudioAssistPlugin.Util
{
    public static class Tracer
    {
        static FileStream fs;

        static Tracer()
        {
#if DEBUG
            fs = new FileStream("D:/hs2-log.txt", FileMode.Append);
#endif
        }

        public static void Log(params object[] ss)
        {
            var list = new List<String>();
            foreach (var o in ss)
            {
                if (o == null)
                {
                    list.Add("NULL");
                }
                else if (o is Vector3)
                {
                    list.Add(Kit.VecStr((Vector3) o));
                }
                else if (o is Quaternion)
                {
                    list.Add(Kit.QuatStr((Quaternion) o));
                }
                else if (o is GameObject)
                {
                    list.Add(Kit.GetGameObjectPath((GameObject) o));
                }
                else if (o is Transform)
                {
                    list.Add(Kit.GetGameObjectPath(((Transform) o).gameObject));
                }
                else if (o is Exception)
                {
                    var e = (Exception) o;
                    list.Add(e.Message);
                    list.Add(e.StackTrace);
                }
                else
                {
                    list.Add(o.ToString());
                }
            }
            var msg = "[FkPlugin] " + String.Join(", ", list.ToArray());
            Debug.Log(msg);

#if DEBUG
            byte[] data = System.Text.Encoding.Default.GetBytes(msg + "\n");
            fs.Write(data, 0, data.Length);
            fs.Flush();
#endif
        }
    }
}