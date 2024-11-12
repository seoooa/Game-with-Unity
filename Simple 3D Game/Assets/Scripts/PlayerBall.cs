using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid;
    public float jumpPower = 30;
    public int itemCount;
    bool isJump;
    AudioSource audio;
   
    void Awake() {
        rigid = GetComponent<Rigidbody>();
        isJump = false;
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void Update() {
        if (Input.GetButtonDown("Jump") && !isJump) {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Floor") isJump = false;
    }

     private void OnTriggerEnter(Collider other) {
        GameManagerLogic manager = FindAnyObjectByType<GameManagerLogic>();

        if (other.gameObject.tag == "Item") {
            itemCount++;
            audio.Play();
            if (manager != null) manager.GetItem(itemCount);

            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Finish") {
            if (manager != null && itemCount == manager.totalItemCount)
            {
                //Game Clear!
                if (manager.stage == 1) SceneManager.LoadScene("SampleScene0");
                else SceneManager.LoadScene("SampleScene"+(manager.stage+1).ToString());
            }
            else {
                //Restart
                SceneManager.LoadScene("SampleScene"+manager.stage.ToString());
            }
        }
    }
}
