using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    private Vector3 direction;
    public GameObject Parent
    {
        set
        {
            parent = value;
        }

        get
        {
            return parent;
        }
    }

    public Color Color
    {
        set
        {
            sprite.color = value;
        }
    }

    [SerializeField] private float _speed;


    private void Start()
    {
        Destroy(gameObject, 1.4f);
    }

    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != parent && unit)
        {
            Destroy(gameObject);
        }
    }


}

