using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamManager : MonoBehaviour
{

    #region Fields

    [SerializeField] GameObject _virtualCam;
    [SerializeField] GameObject _roomObjects;
    [SerializeField] float _transitionTime;
    float _currentTransitionTime;

    [SerializeField] float _toggleTime;
    float _currentToggleTime;

    [Space]

    [SerializeField] bool _isActivated;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {

        _currentToggleTime = 0;
        _currentTransitionTime = 0;

    }

    private void Update()
    {

        if (!_isActivated && _roomObjects.activeInHierarchy)
        {

            _currentTransitionTime += Time.deltaTime;
            if(_currentTransitionTime >= _transitionTime)
                PlayerController.instance.ReturnMovement();

            _currentToggleTime += Time.deltaTime;
            if (_currentToggleTime >= _toggleTime)
                _roomObjects.SetActive(false);

        }

    }

    #endregion

    #region Collider Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            _virtualCam.SetActive(true);
            _roomObjects.SetActive(true);
            _currentToggleTime = 0;
            _currentTransitionTime = 0;
            _isActivated = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerController.instance.StopMovement();

            _virtualCam.SetActive(false);
            _isActivated = false;

        }

    }

    #endregion

}
