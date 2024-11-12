using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// GameManager는 일반적으로 싱글톤 패턴
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private int coin = 0;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

     [SerializeField]
    private GameObject gameClearPanel;

    [HideInInspector]
    public bool isGameOver = false;
    
    //instance 초기화
    void Awake() { // Start()보다 더 빨리 호출되는 메소드
        if (instance == null) {
            instance = this;
        }
    }

    public void IncreaseCoin() {
        coin++;
        text.SetText(coin.ToString());

        if (coin % 10 == 0) {
            Player player = FindAnyObjectByType<Player>(); // 게임 내의 객체를 찾아오기
            if (player != null) player.Upgrade();
        }
    }

    public void SetGameOver(bool isTrue) {
        isGameOver = true;

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        if (enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
        }

        if (isTrue) Invoke("ShowGameOverPanel", 0.5f); //0.5s 뒤에 호출
        else Invoke("ShowGameClearPanel", 0.5f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    void ShowGameClearPanel() {
        gameClearPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
