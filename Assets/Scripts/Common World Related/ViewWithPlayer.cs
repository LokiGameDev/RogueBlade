using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ViewWithPlayer : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private Vector3 _offSetPosition;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = _player.transform.position + _offSetPosition;
    }
}
