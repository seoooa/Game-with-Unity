using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    [SerializeField]
    private TextMeshProUGUI stageCountText;
    [SerializeField]
    private TextMeshProUGUI playerCountText;

    void Awake() {
        stageCountText.SetText("/ "+ totalItemCount);
    }
    
    public void GetItem(int count) {
        playerCountText.SetText(count.ToString());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") SceneManager.LoadScene(stage);
    }
}
