using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public bool addSegment;
    [SerializeField] private Transform head;
    [SerializeField] private float moveRate;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private TextMeshProUGUI sizeText;

    private Vector3 Position
    {
        get => head.position;
        set => head.position = value;
    }

    private Vector3 _direction = Vector3.right;

    private void Start()
    {
        StartCoroutine(UpdateMovementRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector3.up)
            _direction = Vector3.down;
        if (Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector3.down)
            _direction = Vector3.up;
        if (Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector3.left)
            _direction = Vector3.right;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector3.right)
            _direction = Vector3.left;

        if (Physics2D.BoxCast(Position, Vector2.one * .5f, 0, Vector2.zero, 0, layerToHit))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private IEnumerator UpdateMovementRoutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1f / moveRate);
            var lastSegmentPosition = transform.GetChild(transform.childCount - 1).position;
            for (var i = transform.childCount - 1; i > 0; i--)
            {
                transform.GetChild(i).position = transform.GetChild(i - 1).position;
            }

            Position += _direction;

            if (!addSegment) continue;
            var newSegment = Instantiate(head, lastSegmentPosition, Quaternion.identity, transform);
            newSegment.gameObject.layer = LayerMask.NameToLayer("Obstacle");
            newSegment.localScale *= .7f;
            addSegment = false;
            sizeText.text = (transform.childCount - 1).ToString();
        }
    }
}