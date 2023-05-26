using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] Transform[] _spawner;
    [SerializeField] Transform _player;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _player.position = _spawner[0].position;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _player.position = _spawner[1].position;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _player.position = _spawner[2].position;
        if (Input.GetKeyDown(KeyCode.Keypad3))
            _player.position = _spawner[3].position;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            _player.position = _spawner[4].position;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            _player.position = _spawner[5].position;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            _player.position = _spawner[6].position;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            _player.position = _spawner[7].position;
        if (Input.GetKeyDown(KeyCode.Alpha8))
            _player.position = _spawner[8].position;

        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
