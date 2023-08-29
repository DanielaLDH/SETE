using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;
    public float speed;
    public float offset;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {

        Vector3 newCamPosition = new Vector3(Player.position.x + offset, Player.position.y + offset, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newCamPosition, speed * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }
}
