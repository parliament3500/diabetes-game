using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float smoothing, minPosX, maxPosX, minPosY, maxPosY;
    private Vector3 _offset;
    private GameObject _player;
   
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _offset = transform.position - _player.transform.position;
    }

    void Update()
    {
        Vector3 movement = _player.transform.position + _offset;
        movement.x = Mathf.Clamp(movement.x, minPosX, maxPosX);
        movement.y = Mathf.Clamp(movement.y, minPosY, maxPosY);

        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, minPosX, maxPosX), Mathf.Clamp(transform.position.y, minPosY, maxPosY), transform.position.z);
        transform.position = Vector3.Lerp(pos, movement, smoothing * Time.deltaTime);
    }
}
