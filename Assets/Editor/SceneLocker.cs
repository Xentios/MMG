
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using UnityEngine.SceneManagement;

[UnityEditor.InitializeOnLoad]
public static class SceneLocker
{

    static LockedSceneList lockedSceneList;
    static readonly bool debugLogs = false;

    static SceneLocker()
    {
        if(debugLogs) Debug.Log("SceneLocker started");
        lockedSceneList = Resources.Load<LockedSceneList>("LockedSceneList");
        if (lockedSceneList == null) CreateScriptableObject();

       
        //UnityEditor.SceneView.duringSceneGui += DisableSceneEditing;//This WORKS but only for Scene Window.
        EditorSceneManager.sceneOpened += OnsceneOpened;
        //EditorApplication.projectChanged += OnprojectWindowChanged;
        //EditorApplication.update += OnUpdate;
        //SceneManager.sceneLoaded += OnSceneLoaded;
        //EditorSceneManager.sceneLoaded += OnSceneLoaded;
        //EditorApplication.hierarchyChanged += OnHierarchyChange;
        //EditorSceneManager.sceneSaving += OnSceneSaved;
        //EditorSceneManager.sceneLoaded += OnSceneLoaded2;
        //SceneViewOpener x = ScriptableObject.CreateInstance<SceneViewOpener>();
        //LockedScenes = ScriptableObject.CreateInstance<ScriptableObject>();
        EditorApplication.update += OnEditorUpdate;
        EditorApplication.quitting += OnEditorQuitting;
    }

    private static void OnEditorQuitting()
    {
        Debug.Log("OnEditorQuitting started");
        EditorApplication.update -= OnEditorUpdate;
        EditorSceneManager.sceneOpened -= OnsceneOpened;
        EditorApplication.quitting -= OnEditorQuitting;
    }

    private static void OnEditorUpdate()
    {
        if (debugLogs) Debug.Log("OnEditorUpdate started");
        foreach (var sceneNames in lockedSceneList.LockedSceneNames)
        {
            LockThisScene(sceneNames);
        }
        EditorApplication.update -= OnEditorUpdate;
    }

    

    private static void OnsceneOpened(Scene scene, OpenSceneMode mode)
    {
        if (debugLogs) Debug.Log("sceneOpened started in "+ scene.name);

        foreach (var sceneNames in lockedSceneList.LockedSceneNames)
        {
            LockThisScene(sceneNames);
        }

    }

    private static void DisableSceneEditing(SceneView obj)
    {
        if (debugLogs)  Debug.Log("duringSceneGui started");
    }

    private static void CreateScriptableObject()
    {
        LockedSceneList temp = ScriptableObject.CreateInstance<LockedSceneList>(); 
        AssetDatabase.CreateAsset(temp, "Assets/Resources/LockedSceneList.asset");
        AssetDatabase.SaveAssets();
        lockedSceneList= Resources.Load<LockedSceneList>("LockedSceneList");
        if (lockedSceneList == null) Debug.LogError("Lock Error");
     }

    private static bool LockThisScene(string name)
    {
        Scene activeScene = EditorSceneManager.GetActiveScene();
        if (debugLogs) Debug.Log("---" + activeScene.name + "---");
        if (activeScene.name != name) return false;
        
        Debug.LogError(activeScene.name+" is LOCKED!");
        //var dummyScene = SceneManager.GetSceneByName("DummyScene");
        EditorSceneManager.OpenScene("Assets/Scenes/DummyScene.unity");
        EditorSceneManager.CloseScene(activeScene, false);
        return true;
    }
}
