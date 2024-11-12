using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        // 글로벌 좌표계 기준 회전
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
