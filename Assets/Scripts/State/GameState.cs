public class GameState : SceneState
{
    public GameState(SceneController sceneController) : base(sceneController)
    {
    }

    public override void Start()
    {
        SceneController.LoadScene(GameScene.Game);
        SceneController.UnloadScene(GameScene.Menu);
        AudioController.Instance?.Play("Background");
    }
}
