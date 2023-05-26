using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{

    #region Fields

    [SerializeField] SpriteRenderer _doorSprite;
    [SerializeField] int _sceneToLoad;

    #endregion

    #region MonoBehaviour Methods

    private void Update()
    {
        
        if(_doorSprite.enabled && Input.GetButtonDown("Fire1"))
        {

            SceneManager.LoadScene(_sceneToLoad);

        }

    }

    #endregion

    #region Collider Methods

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        _doorSprite.enabled = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        _doorSprite.enabled = false;

    }

    #endregion

}
