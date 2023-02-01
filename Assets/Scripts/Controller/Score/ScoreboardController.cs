using System.IO;
using UnityEngine;

public class ScoreboardController : Controller
{
    [SerializeField]
    private Transform _container;

    [SerializeField]
    private GameObject _entryPrefab;

    [SerializeField]
    private int _maxEntries = 5;

    private string _savePath => $"{UnityEngine.Application.persistentDataPath}/Highscores.json";

    private string _currentPlayerName = "TempName";

    private void Start()
    {
        //Load saved scores
        ScoreSaveModel savedScores = LoadScore();

        //Update UI
        DrawUI(savedScores);

        //Save to a json file
        SaveScore(savedScores);

        //Deactivate the UI at first
        _container.transform.parent.gameObject.SetActive(false);
    }

    public void AddEntry(string name, int score)
    {
        //Create a new data model from the new entry 
        ScoreEntryModel newModel = new ScoreEntryModel()
        {
            Name = name,
            Score = score
        };

        ScoreSaveModel savedScores = LoadScore();

        bool scoreAdded = false;

        //Check if the score is high enough to be added
        for (int i = 0; i < savedScores.highscores.Count; i++)
        {
            if (score > savedScores.highscores[i].Score)
            {
                savedScores.highscores.Insert(i, newModel);
                scoreAdded = true;
                break;
            }
        }

        //Check if we exceed the max amount of entries
        if (!scoreAdded && savedScores.highscores.Count < _maxEntries)
        {
            savedScores.highscores.Add(newModel);
        }

        //Remove any excess scores 
        if (savedScores.highscores.Count > _maxEntries)
        {
            savedScores.highscores.RemoveRange(_maxEntries, savedScores.highscores.Count - _maxEntries);
        }

        DrawUI(savedScores);

        SaveScore(savedScores);
    }

    private void DrawUI(ScoreSaveModel savedScores)
    {
        //Destroy any existing transforms
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        //Init new transforms to show the high scores
        foreach (ScoreEntryModel highscore in savedScores.highscores)
        {
            Instantiate(_entryPrefab, _container).GetComponent<ScoreEntryView>().Initialise(highscore);
        }
    }

    private ScoreSaveModel LoadScore()
    {
        //Check if the file exists
        if (!File.Exists(_savePath))
        {
            File.Create(_savePath).Dispose();
            return new ScoreSaveModel();
        }

        //Read the saved json file
        using (StreamReader stream = new StreamReader(_savePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<ScoreSaveModel>(json);
        }
    }

    private void SaveScore(ScoreSaveModel scoreboardSaveData)
    {
        //Save data as json
        using (StreamWriter stream = new StreamWriter(_savePath))
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true);
            stream.Write(json);
        }
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            //Press event
            case ApplicationEvents.PressedScoreboard:
                _container.transform.parent.gameObject.SetActive(!_container.transform.parent.gameObject.activeSelf);
                break;
            case ApplicationEvents.SavingNewScore:
                AddEntry(_currentPlayerName, (int)p_data[0]);
                break;
            case ApplicationEvents.RestartingGame:
                _container.transform.parent.gameObject.SetActive(true);
                break;
            case ApplicationEvents.UpdatedPlayerName:
                _currentPlayerName = (string)p_data[0];
                _container.transform.parent.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}

