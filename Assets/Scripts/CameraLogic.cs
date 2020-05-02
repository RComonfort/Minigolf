using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 10f;

    Player player;
    float initialZ;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        initialZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = Vector3.Lerp(transform.position, player.transform.position, lerpSpeed);
        target.z = initialZ;
        transform.position = target;
    }
}
