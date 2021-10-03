using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    [SerializeField] private LayerMask headLayer;
    [SerializeField] private Vector2Int bounds;

    private void Start()
    {
        RandomizePosition();
    }

    private void Update()
    {
        var hit = Physics2D.BoxCast(transform.position, Vector2.one * .5f, 0, Vector2.zero, 0, headLayer);
        if (!hit) return;
        RandomizePosition();
        hit.transform.GetComponentInParent<Snake>().addSegment = true;
    }

    private void RandomizePosition()
    {
        transform.position = new Vector3(Random.Range(-bounds.x + 1, bounds.y) / 2, Random.Range(-bounds.x + 1, bounds.y) / 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(bounds.x, bounds.y));
    }
}