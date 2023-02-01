using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Controller
{
    //Scene model scriptable object
    [SerializeField]
    private SceneModel _sceneModel;

    //List of loading opertations
    private List<AsyncOperation> _loadOperations = new List<AsyncOperation>();

    private void Start()
    {
        //Init game state and starting scene
        _sceneModel.Init(this);

        //Set game target FPS
        UnityEngine.Application.targetFrameRate = 60;
    }

    // Start button pressed for changing the game state and scene to starting
    private void OnStartBtnPressed()
    {
        _sceneModel.ChangeState(StateAction.Starting);
    }

    // Exit the game app
    private void OnExitBtnPressed()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        UnityEngine.Application.Quit();
    }

    private void OnRestartBtnPressed()
    {
        _sceneModel.ChangeState(StateAction.Restarting);
    }

    /**
     * Scene loading/unloading helper functions
     */
    public void LoadScene(GameScene scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);

        if (ao == null)
        {
            Debug.LogError("[GameManager] Error loading level." + scene);
            return;
        }

        _loadOperations.Add(ao);
        ao.completed += OnSceneLoadComplete;
    }

    public void UnloadScene(GameScene scene)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync((int)scene);

        ao.completed += OnSceneUnloadComplete;
    }

    private void OnSceneLoadComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

        }

        Debug.Log("Level load completed!");

        if (_sceneModel.ScenesLoaded == 1)
        {
            gameObject.AddComponent<ApplicationControllerRoot>();
        }

        _sceneModel.ScenesLoaded++;
    }

    private void OnSceneUnloadComplete(AsyncOperation ao)
    {
        Debug.Log("Level unloaded completed!");
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.StartingGame:
                OnStartBtnPressed();
                break;
            case ApplicationEvents.ExitingGame:
                OnExitBtnPressed();
                break;
            case ApplicationEvents.RestartingGame:
                OnRestartBtnPressed();
                break;
        }
    }
}
