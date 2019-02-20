﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityScript.Lang;

namespace litefeel.OpenFileWithDefaultApp
{

    public static class PreferencesGUI
    {
        private static readonly string[] Empty = new string[0];

        private const string KEY = "OpenFileWithDefaultApp_Exts";

        
        private static bool s_ArrCached = false;
        private static string[] s_FileExt;
        public static string[] FileExts
        {
            get
            {
                if (s_ArrCached && s_FileExt != null)
                    return s_FileExt;
                else
                {
                    s_ArrCached = true;
                    var str = FileExtsStr;
                    if (string.IsNullOrEmpty(str))
                        s_FileExt = Empty;
                    else
                    {
                        s_FileExt = str.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                        for(var i = 0; i < s_FileExt.Length; ++i)
                        {
                            s_FileExt[i] = "." + s_FileExt[i];
                        }
                    }
                }
                return s_FileExt;
            }
        }

        private static string s_FileExtsStr;
        private static string FileExtsStr
        {
            get
            {
                if(s_FileExtsStr == null)
                    s_FileExtsStr = EditorPrefs.GetString(KEY, "");
                return s_FileExtsStr;
            }
            set
            {
                if(s_FileExtsStr != value && value != null)
                {
                    s_FileExtsStr = value;
                    EditorPrefs.SetString(KEY, s_FileExtsStr);
                    s_ArrCached = false;
                }
            }
        }
        

        // Add preferences section named "My Preferences" to the Preferences window
        [PreferenceItem("Open File With Default App")]
        public static void OnGUI()
        {
            EditorGUILayout.LabelField("Specify the files will open with default application. like (shader txt)");
            EditorGUILayout.LabelField("Separate the patterns with a space.");
            FileExtsStr = EditorGUILayout.TextField("File Extensions", FileExtsStr);
        }
    }
}