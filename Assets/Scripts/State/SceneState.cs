public abstract class SceneState
{
    protected SceneController SceneController;

    public SceneState(SceneController sceneController)
    {
        SceneController = sceneController;
    }

    public abstract void Start();
}
