using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StartMenu : MonoBehaviour
{
    public TMP_Text IntroductionText;
    public GameObject VictorinePrefab;

    private Direction _previousDirection = Direction.None;
    private GameState _state = GameState.SearchingForFace;
    private ARFaceMeshVisualizer _face;
    private Victorine _victorine;
    private int _bestScore = 0;

    private static string Invintation = "Nod to start the game";

    void Start()
    {
        IntroductionText.text = "Please wait...";
    }

    void Update()
    {
        _face = FindObjectOfType<ARFaceMeshVisualizer>();

        switch (_state)
        {
            case GameState.Waiting:
                return;
            case GameState.SearchingForFace:
                if (_face == null) return;
                _state = GameState.MainMenu;

                IntroductionText.text = Invintation;
                Update();

                break;

            case GameState.MainMenu:
                if (!DetectNod()) return;
                _state = GameState.Playing;

                IntroductionText.text = "";
                _victorine = Instantiate(VictorinePrefab).GetComponent<Victorine>();

                break;

            case GameState.Playing:
                if (!_victorine.IsOver) return;
                _state = GameState.Waiting;

                if (_bestScore < _victorine.PointsCount)
                    _bestScore = _victorine.PointsCount;

                var scoreText = $"Score: {_victorine.PointsCount}/{_victorine.QuestionsCount}\n";
                var bestScoreText = $"Best: {_bestScore}/{_victorine.QuestionsCount}\n";
                IntroductionText.text = $"{scoreText}{bestScoreText}\n{Invintation}";
                Destroy(_victorine.gameObject);
                _victorine = null;

                StartCoroutine(WaitBeforeMainMenu(5.0f));

                break;
        }

    }

    private IEnumerator WaitBeforeMainMenu(float time)
    {
        yield return new WaitForSeconds(time);
        _state = GameState.MainMenu;
    }

    private bool DetectNod()
    {
        var dir = FaceControls.LookingTo(_face.gameObject.transform.forward);

        if (dir != Direction.None && dir != _previousDirection)
        {
            switch ((dir, _previousDirection))
            {
                case ((Direction.Up, Direction.Down)):
                case ((Direction.Down, Direction.Up)):
                    return true;
            }
            _previousDirection = dir;
        }

        return false;
    }
}

public enum GameState
{
    Waiting,
    SearchingForFace,
    MainMenu,
    Playing,
}