//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

////[CustomEditor(typeof(BodyConfig))]
//public class BodyConfig_Editor : Editor
//{
//    OrbitController orbitController;

//    override public void OnInspectorGUI()
//    {
//        DrawDefaultInspector();


//        var bodyConfig = target as BodyConfig;

//        bodyConfig.isSun = EditorGUILayout.Toggle("Is Sun", bodyConfig.isSun);
        

//        if (bodyConfig.isSun)
//        {
//            bodyConfig.orbitingSun = null;
//        }
//        else
//        {
//            bodyConfig.orbitingSun = EditorGUILayout.ObjectField("Orbiting Sun", bodyConfig.orbitingSun, typeof(Transform), true) as Transform;

//            if (bodyConfig.orbitingSun && bodyConfig.orbitingSun.GetComponent<BodyConfig>().isSun)
//            {
//                if (!bodyConfig.gameObject.GetComponent<OrbitController>())
//                {
//                    orbitController = bodyConfig.gameObject.AddComponent<OrbitController>();
//                    orbitController.orbitOrigin = bodyConfig.orbitingSun;
//                }
//            }
//            else
//            {
//                //bodyConfig.orbitingSun = null;
//            }
//        }


//        bodyConfig.overrideSize = EditorGUILayout.Toggle("Override Size", bodyConfig.overrideSize);


//        if (bodyConfig.overrideSize)
//        {
//            bodyConfig.scaleModifier = EditorGUILayout.Slider("Scale Modifier", bodyConfig.scaleModifier, 1f, 100f);
//        }
//        else
//        {
//            bodyConfig.scaleModifier = 1f;
//        }
//    }
//}
