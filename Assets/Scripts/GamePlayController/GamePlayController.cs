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

    private int _score;
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
        this._score = _score;
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
    
    public void _OnClaim() {
        WAM.Mint(_score, _ReMenu);
    }
    private void _ReMenu() {
        SceneManager.LoadScene("Menu");
    }
}
