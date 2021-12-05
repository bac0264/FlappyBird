using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {
    public static GamePlayController instance;
    [SerializeField]
    private Button InstructionButton;
    [SerializeField]
    private Text score, bestScore, yourScore;
    [SerializeField]
    private GameObject panel;
	 //Use this for initialization
	void Awake () {
        _makeInstance();
        //Time.timeScale = 0;
	}

    public void _makeInstance() {
        if (instance == null) instance = this;
    }
	public void _Instruction() {
        Time.timeScale = 1;
        InstructionButton.gameObject.SetActive(false);
	}
    public void _setScore(int _score) {
        score.text = _score.ToString();
    }
    public void _setPanel(int _score) {
        panel.SetActive(true);
        yourScore.text = _score.ToString();
        if (_score > _GameManager.instance._getHighScore())
        {
            _GameManager.instance._setHighScore(_score);
        }
        bestScore.text = _GameManager.instance._getHighScore().ToString();
    }
    public void _ReGamePlay() {
        SceneManager.LoadScene("GamePlay");
    }
    public void _ReMenu() {
        SceneManager.LoadScene("Menu");
    }
}
