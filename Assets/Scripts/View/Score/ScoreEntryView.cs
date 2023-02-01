using UnityEngine;
using UnityEngine.UI;

//Class for score board entry UI
public class ScoreEntryView : ApplicationElement
{
    [SerializeField] 
    private Text _nameText = null;

    [SerializeField] 
    private Text _scoreText = null;

    //Init text fields
    public void Initialise(ScoreEntryModel scoreEntryModel)
    {
        _scoreText.text = scoreEntryModel.Score.ToString();
        _nameText.text = scoreEntryModel.Name;
    }
}
