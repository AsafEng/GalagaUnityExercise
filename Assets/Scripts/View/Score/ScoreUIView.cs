using UnityEngine.UI;

//Class for showing the current game score on the UI
public class ScoreUIView : ApplicationElement 
{ 
    private Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponentInChildren<Text>();
    }

    //Update score's text field
    public void UpdateScoreText(int amount)
    {
        int totalAmount = int.Parse(_scoreText.text) + amount;
        _scoreText.text = totalAmount.ToString();
    }
}
