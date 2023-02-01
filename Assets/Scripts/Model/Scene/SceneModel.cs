using UnityEngine;

//Scenes of the application
public enum GameScene
{
    Boot = 0,
    Persistant = 1,
    Menu = 2,
    Game = 3,
}

//Types of possible game state actions
public enum StateAction
{
    Booting = 0,
    Starting = 1,
    Restarting = 2
}

[CreateAssetMenu(fileName = "New Scene Model", menuName = "ScriptableObjects/Scene Model", order = 0)]
public class SceneModel : ScriptableObject
{
    public int ScenesLoaded;

    private SceneState _currentGameState;

    private BootState _bootState;
    private GameState _gameState;
    private RestartState _restartState;

    public void Init(SceneController sceneController)
    {
        //Init all needed states
        _bootState = new BootState(sceneController);
        _gameState = new GameState(sceneController);
        _restartState = new RestartState(sceneController);

        //Init boot state initially
        ChangeState(StateAction.Booting);
    }

    //Function for changing to a different game state
    public void ChangeState(StateAction stateAction)
    {
        switch (stateAction) 
        {
            case StateAction.Booting:
                _currentGameState = _bootState;
                ScenesLoaded = 0;
                break;
            case StateAction.Starting:
                _currentGameState = _gameState;
                break;
            case StateAction.Restarting:
                _currentGameState = _restartState;
                ScenesLoaded = 0;
                break;

        }

        NextState();
    }

    //Execute the next state
    private void NextState()
    {
        _currentGameState.Start();
    }
}
