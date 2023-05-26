using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class InteractableManager : MonoBehaviour
{

    #region Fields

    enum InteractableFunction
    {

        None,
        SceneChanger

    }

    [SerializeField] InteractableFunction _interactableFunction;

    [Header("Scene Changer Function")]
    [SerializeField] int _sceneToLoad;
    [Space]
    [SerializeField] GameObject[] _objectsToToggle;
    [SerializeField] bool _toggle;

    #endregion

    #region MonoBehaviour Methods

    private void Update()
    {
        
        if(_toggle && Input.GetButtonDown("Fire1") && _interactableFunction != InteractableFunction.None)
            switch (_interactableFunction)
            {

                case InteractableFunction.SceneChanger:
                    SceneManager.LoadScene(_sceneToLoad);
                    break;

            }

    }

    #endregion

    #region Coroutines

    IEnumerator ToggleObjects()
    {

        for(int i = 0;  i < _objectsToToggle.Length; i++)
        {

            _objectsToToggle[i].SetActive(_toggle);

        }

        yield return null;

    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(ToggleObjects());
            _toggle = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(ToggleObjects());
            _toggle = false;

        }

    }

}
