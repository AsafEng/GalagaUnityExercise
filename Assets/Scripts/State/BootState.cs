public class BootState : SceneState
{
    public BootState(SceneController sceneController) : base(sceneController)
    {
    }

    public override void Start()
    {
        SceneController.LoadScene(GameScene.Persistant);
        SceneController.LoadScene(GameScene.Menu);
    }
}
