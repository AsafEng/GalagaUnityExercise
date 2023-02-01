using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : Controller
{
    //List of levels
    [SerializeField]
    private List<LevelDataModel> _levels;

    [SerializeField]
    private LevelModel _model;

    [SerializeField]
    private GridController _gridController;
    
    private void Awake()
    {
        //Init a new current level model
        _model = new LevelModel();
    }

    private void Start()
    {
        ActivateLevel(_model.CurrentLevel);
    }

    private void ActivateLevel(int levelIndex)
    {
        app.Notify(ApplicationEvents.StartingLevel, this);

        //Spawn enemies
        for (int i = 0; i < _levels[levelIndex].SpawnModels.Count; i++)
        {
            StartCoroutine(SpawnModel(i * _model.TimeBetweenSpawns, _levels[levelIndex].SpawnModels[i]));
        }

        //Update model's count
        _model.CurrentEnemyCount = _levels[levelIndex].SpawnModels.Count;

        StartCoroutine(OnFinishedSpawning((_levels[levelIndex].SpawnModels.Count + 1) * _model.TimeBetweenSpawns));
    }

    private IEnumerator OnFinishedSpawning(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        app.Notify(ApplicationEvents.FilledGrid, this);
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.EnemyKilled:
                //Remove enemy count from the model
                _model.CurrentEnemyCount--;
                if (_model.CurrentEnemyCount == 0)
                { 
                    _model.CurrentLevel++;

                    //Check if the game has ended
                    if (_model.CurrentLevel < _levels.Count)
                    {
                        ActivateLevel(_model.CurrentLevel);
                    }
                    else
                    {
                        app.Notify(ApplicationEvents.FinishedGame, this);
                    }
                }
                break;
            case ApplicationEvents.GainedScore:
                //Add game score to the model
                int gainedScore = (int)p_data[0];
                _model.CurrentScore += gainedScore;
                break;
            case ApplicationEvents.RestartingGame:
                //Save game score
                object[] objectsToPass = { _model.CurrentScore };
                app.Notify(ApplicationEvents.SavingNewScore, this, objectsToPass);
                break;
            default:
                break;
        }
    }

    private IEnumerator SpawnModel(float time, EnemySpawnModel enemyModel)
    {
        yield return new WaitForSecondsRealtime(time);
        object[] objectsToPass = { enemyModel, _gridController.GetNextEmptyCell(), _gridController.GetNextEmptyCellByGrid() };
        app.Notify(ApplicationEvents.SpawningEnemy, this, objectsToPass);
    }
}
