using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatformManager : MonoBehaviour
{

    #region Fields

    [SerializeField] float _timeToDestroy;
    [SerializeField] float _timeToReturn;
    [SerializeField] bool _isDestroyed;
    float _currentTimeToDestroy;
    float _currentTimeToReturn;

    #endregion

    #region Cached Components

    BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {

        if (_isDestroyed)
        {

            _currentTimeToDestroy += Time.deltaTime;
            if(_currentTimeToDestroy >= _timeToDestroy)
            {

                _boxCollider.enabled = false;
                _spriteRenderer.enabled = false;

                _currentTimeToReturn = 0;
                _isDestroyed = false;

            }

        }
        else if(!_isDestroyed && _currentTimeToReturn <= _timeToReturn)
        {

            _currentTimeToReturn += Time.deltaTime;
            if( _currentTimeToReturn >= _timeToReturn)
            {
                _boxCollider.enabled = true;
                _spriteRenderer.enabled = true;
            }

        }

    }

    #endregion

    #region Collider Methods

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && !_isDestroyed)
        {

            _currentTimeToDestroy = 0;
            _isDestroyed = true;

        }

    }

    #endregion

}
