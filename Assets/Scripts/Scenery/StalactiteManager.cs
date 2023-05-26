using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalactiteManager : MonoBehaviour
{

    #region Fields

    [SerializeField] Vector3 _viewPosition1;
    [SerializeField] Vector3 _viewPosition2;
    [SerializeField] LayerMask _viewMask;
    [SerializeField] float _maxViewDistance;
    [SerializeField] bool _fallActivated;
    [SerializeField] bool _allowSecond;

    #endregion

    #region Cached Components

    Rigidbody2D _rb;
    Transform _transform;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {

        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

    }

    private void Start()
    {

        _rb.bodyType = RigidbodyType2D.Kinematic;

    }

    private void Update()
    {
        
        if(Physics2D.Linecast(new Vector2(transform.position.x + _viewPosition1.x, transform.position.y),
            new Vector2(transform.position.x + _viewPosition1.x, transform.position.y + _maxViewDistance), _viewMask) && !_fallActivated)
        {

            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            _fallActivated = true;

        }

        if (Physics2D.Linecast(new Vector2(transform.position.x + _viewPosition2.x, transform.position.y),
            new Vector2(transform.position.x + _viewPosition2.x, transform.position.y + _maxViewDistance), _viewMask) && !_fallActivated && _allowSecond)
        {

            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            _fallActivated = true;

        }

    }

    #endregion

    #region Collider Method

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
        else if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    #endregion

    #region Gizmo Method

    private void OnDrawGizmos()
    {

        Gizmos.DrawLine(new Vector2(transform.position.x + _viewPosition1.x, transform.position.y),
            new Vector2(transform.position.x + _viewPosition1.x, transform.position.y + _maxViewDistance));

        if(_allowSecond)
            Gizmos.DrawLine(new Vector2(transform.position.x + _viewPosition2.x, transform.position.y),
            new Vector2(transform.position.x + _viewPosition2.x, transform.position.y + _maxViewDistance));

    }

    #endregion

}
