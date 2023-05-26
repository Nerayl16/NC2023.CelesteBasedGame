using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region Fields

    [Header("Wind Config")]
    [SerializeField] Rigidbody2D _playerRB;
    [SerializeField] float _windStartTime;
    [SerializeField] float _windStopTime;
    [SerializeField] float _windSpeed;
    [SerializeField] bool _windStarted;
    float _currentWindStartTime;
    float _currentWindStopTime;

    #endregion

    #region MonoBehaviour Methods

    private void Start()
    {

        _playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        // Wind Methods

        if (!_windStarted)
        {

            _currentWindStartTime += Time.deltaTime;
            if (_currentWindStartTime > _windStartTime)
                StartWind();

        }
        else
        {

            _currentWindStopTime += Time.deltaTime;
            if (_currentWindStopTime > _windStopTime)
                StopWind();

        }

    }

    #endregion

    #region Private Methods

    void StartWind()
    {

        PlayerController.instance.windSpeed = _windSpeed;
        _currentWindStopTime = 0;
        _windStarted = true;

    }

    void StopWind()
    {

        PlayerController.instance.windSpeed = 0;
        _currentWindStartTime = 0;
        _windStarted = false;

    }

    #endregion

}
