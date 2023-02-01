using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartState : SceneState
{
    public RestartState(SceneController sceneController) : base(sceneController)
    {
    }

    public override void Start()
    {
        SceneController.UnloadScene(GameScene.Game);
        SceneController.UnloadScene(GameScene.Persistant);
        SceneController.LoadScene(GameScene.Persistant);
        SceneController.LoadScene(GameScene.Menu);
    }
}
