using UnityEngine;
using UnityEditor;
using System.Collections;
using qtools.qhierarchy;
using System.Linq;
using qtools.qhierarchy.phierarchy;
//using qtools.qhierarchy.phierarchy;
using UnityEditor.SceneManagement;

public class GlitchHotkeys
{
    [MenuItem("GlitchLibrary/Toggle Selected GameObject &LEFT")]
    static void EnableDisable()
    {
        if (Selection.gameObjects.Length > 0)
        {
            //var scene = EditorSceneManager.GetActiveScene();
            QHierarchyInitializer.SetVisibility(Selection.gameObjects.ToList(), !Selection.gameObjects[0].activeSelf, Selection.gameObjects[0].activeSelf);
        }
    }
/*
    [MenuItem("GlitchLibrary/Toggle Selected GameObject alt &RIGHT")]
    static void EnableDisable2()
    {
        if (Selection.gameObjects.Length > 0)
        {
            var scene = EditorSceneManager.GetActiveScene();
            var objectList = QHierarchy.createObjectListInScene(scene);
            QHierarchy.setVisibility(objectList, Selection.gameObjects.ToList(), !Selection.gameObjects[0].activeSelf, !Selection.gameObjects[0].activeSelf);
        }
    }
    */


    [MenuItem("GlitchLibrary/TimeScale/1x &1")]
    static void SetTimescale1x() { SetTimescale(1); }

    [MenuItem("GlitchLibrary/TimeScale/2x &2")]
    static void SetTimescale2x() { SetTimescale(2); }

    [MenuItem("GlitchLibrary/TimeScale/3x &3")]
    static void SetTimescale3x() { SetTimescale(3); }

    [MenuItem("GlitchLibrary/TimeScale/4x &4")]
    static void SetTimescale4x() { SetTimescale(4); }

    [MenuItem("GlitchLibrary/TimeScale/5x &5")]
    static void SetTimescale10x() { SetTimescale(10); }

    [MenuItem("GlitchLibrary/TimeScale/0.5x &6")]
    static void SetTimescale05x() { SetTimescale(0.5f); }

    [MenuItem("GlitchLibrary/TimeScale/0.1x &7")]
    static void SetTimescale01x() { SetTimescale(0.1f); }


    static void SetTimescale(float val)
    {
        if (Application.isPlaying)
        {
            Time.timeScale = val;
            Debug.Log("[GlitchHotkeys] Timescale set to " + val + ".");
        }
        else
        {
            Debug.Log("[GlitchHotkeys] Timescale only works in play mode.");
        }
    }
}
