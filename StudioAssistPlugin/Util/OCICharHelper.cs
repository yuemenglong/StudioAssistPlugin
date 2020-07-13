using System;
using System.Security;
using Studio;
using UnityEngine;
using static Studio.OIBoneInfo;
using static Studio.OICharInfo;

namespace StudioAssistPlugin.Util
{
    public static class OCICharHelper
    {
        public class AnimeT
        {
            public int group;
            public int category;
            public int no;
            public float normalizedTime;
        }

        public static AnimeT myGetAnime(this OCIChar ch)
        {
            var info = ch.oiCharInfo.animeInfo;
            var ret = new AnimeT();
            ret.group = info.group;
            ret.category = info.category;
            ret.no = info.no;
            ret.normalizedTime = ch.charAnimeCtrl.normalizedTime;
            return ret;
        }
        public static void mySetAnime(this OCIChar ch, AnimeT a)
        {
            ch.LoadAnime(a.group, a.category, a.no, a.normalizedTime);
        }

        public static void myRestartAnime(this OCIChar ch)
        {
            ch.RestartAnime();
        }

        public static float myGetAnimeSpeed(this OCIChar ch)
        {
            return ch.animeSpeed;
        }

        public static void mySetAnimeSpeed(this OCIChar ch, float speed)
        {
            ch.animeSpeed = speed;
        }

        public static bool myIsAnimeOver(this OCIChar ch)
        {
            return ch.charAnimeCtrl.normalizedTime >= 1;
        }

        public class ClipInfoT
        {
            public float second;
            public int frame;
            public float fps;
        }

        public static ClipInfoT myGetAnimeLength(this OCIChar ch)
        {
            var clipInfos = ch.charAnimeCtrl.animator.GetCurrentAnimatorClipInfo(0);
            var clip = clipInfos[0].clip;
            var ret = new ClipInfoT();
            ret.second = clip.length;
            ret.frame = (int)Math.Round(clip.length * clip.frameRate);
            ret.fps = clip.frameRate;
            return ret;
        }

        public static void mySetFKActive(this OCIChar ch, bool active)
        {
            ch.ActiveKinematicMode(KinematicMode.FK, active, true);
        }

        public static void mySetIKActive(this OCIChar ch, bool active)
        {
            ch.ActiveKinematicMode(KinematicMode.IK, active, false);
        }

        public static bool myGetFKEnable(this OCIChar ch)
        {
            return ch.oiCharInfo.enableFK;
        }

        public static bool myGetIKEnable(this OCIChar ch)
        {
            return ch.oiCharInfo.enableIK;
        }

        public static void myCopyBone(this OCIChar ch)
        {
            ch.fkCtrl.CopyBone();
        }

        //     def set_look_neck(self, ptn):
        // # ptn for CharaStudio: 0: front, 1: camera, 2: hide from camera, 3: by anime, 4: fix
        // # ptn for PHStudio: 0: front, 1: camera, 2: by anime, 3: fix
        // self.objctrl.ChangeLookNeckPtn(ptn)
        public static void mySetLookNeckPtn(this OCIChar ch, int pth){
            ch.charInfo.ChangeLookNeckPtn(pth);
        }

        public static int myGetLookNeckPtn(this OCIChar ch){
            return ch.charInfo.GetLookNeckPtn();
        }

        public static bool myIsFemale(this OCIChar ch)
        {
            return ch is OCICharFemale;
        }
        public static bool myIsMale(this OCIChar ch)
        {
            return ch is OCICharMale;
        }

        public static OCIChar.BoneInfo myGetBoneHips(this OCIChar ch)
        {
            OCIChar.BoneInfo ret = null;
            var index = 0;
            ch.listBones.ForEach(b => {
                Tracer.Log(index, b.guideObject.transformTarget.name);
                if (b.guideObject.transformTarget.name.EndsWith("_J_Hips"))
                {
                    ret = b;
                }
                index += 1;
            });
            return ret;
        }
    }
}