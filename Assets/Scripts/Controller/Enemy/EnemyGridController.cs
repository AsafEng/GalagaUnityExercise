using System.Collections;
using UnityEngine;

public class EnemyGridController : Controller
{
    [SerializeField]
    private EnemyGridModel _model;

    [SerializeField]
    private EnemyGridView _view;

    void Start()
    {
        //First grid move after a few seconds
        StartCoroutine(ChooseNextMove(Random.Range(_model.MinTime, _model.MaxTime)));
    }

    private IEnumerator ChooseNextMove(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        //Check if moveable
        if (_model.ReadyToMove)
        {
            //Save last position
            _model.LastPosition = _view.transform.position;

            //Select next move randomly
            int randomChoice = Random.Range(0, 4);
            switch (randomChoice)
            {
                case 0:
                    _view.transform.position += Vector3.right;
                    break;
                case 1:
                    _view.transform.position += Vector3.down;
                    break;
                case 2:
                    _view.transform.position += Vector3.left;
                    break;
                case 3:
                    _view.transform.position += Vector3.up;
                    break;
            }
        }

        //Schedule next revert randomly
        StartCoroutine(MoveToOldPosition(Random.Range(_model.MinTime, _model.MaxTime)));
    }

    private IEnumerator MoveToOldPosition(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        //Go back to old grid position
        _view.transform.position = _model.LastPosition;

        //Schedule next grid move randomly
        StartCoroutine(ChooseNextMove(Random.Range(_model.MinTime, _model.MaxTime)));
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.FilledGrid:
                _model.ReadyToMove = true;
                break;
            case ApplicationEvents.StartingLevel:
                _model.ReadyToMove = false;

                //Go back to old grid position
                _view.transform.position = _model.LastPosition;
                break;
            default:
                break;
        }
    }
}
