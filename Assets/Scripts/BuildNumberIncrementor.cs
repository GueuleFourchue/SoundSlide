using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
#endif

namespace IronUtilities
{
    #if UNITY_EDITOR
    public class BuildNumberIncrementor : IPreprocessBuild
    {
        public int callbackOrder { get { return 0; } }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            string[] bundleVersionSplit = PlayerSettings.bundleVersion.Split('.');

            if (bundleVersionSplit.Length != 3)
            {
                Debug.Log("IronUtilities - BuildNumberIncrementor Initialization");
                PlayerSettings.bundleVersion = "1.0.0";
                PlayerSettings.Android.bundleVersionCode = 100;
                PlayerSettings.iOS.buildNumber = "1.0.0";
                PlayerSettings.macOS.buildNumber = "1.0.0";
            }

            bundleVersionSplit = PlayerSettings.bundleVersion.Split('.');

            var first = int.Parse(bundleVersionSplit[0]);
            var second = int.Parse(bundleVersionSplit[1]);
            var third = int.Parse(bundleVersionSplit[2]);

            if (third != 9)
                third++;
            else
            {
                third = 0;

                if (second != 9)
                    second++;
                else
                {
                    second = 0;

                    first++;
                }
            }

            var version = first + "." + second + "." + third;

            PlayerSettings.bundleVersion = version;
            PlayerSettings.Android.bundleVersionCode = int.Parse(first.ToString() + second.ToString() + third.ToString());
            PlayerSettings.iOS.buildNumber = version;
            PlayerSettings.macOS.buildNumber = version;

            Debug.Log("IronUtilities - Build Number Incremented: " + PlayerSettings.bundleVersion);
        }
    }
    #endif
}