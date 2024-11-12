using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    [SerializeField]
    private GameObject coin;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed; // 변수명이 같을 땐 멤버 변수에 this를 붙여야 함
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY) Destroy(gameObject);
    }

    // private void OnCollisionEnter2D(Collision2D other) { //isTrigger를 체크하지 않았을 때 충돌 감지 사용
    // }
    private void OnTriggerEnter2D(Collider2D other) { //isTrigger를 체크했을 때 충돌 감지 사용
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;

            if (hp <= 0) {
                if (gameObject.tag == "Boss") {
                    GameManager.instance.SetGameOver(false);
                }
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject);
        }

    }
    
}
