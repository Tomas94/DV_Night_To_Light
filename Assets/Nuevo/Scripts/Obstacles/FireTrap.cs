using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    Renderer _render;
    Material _material;
    [SerializeField] ParticleSystem _firePS;
    BoxCollider _boxCollider;

    [SerializeField] float _cooldown = 2f;
    [SerializeField] float _damage;
    [SerializeField] bool _isBurning;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _render = GetComponent<Renderer>();
        _material = _render.material;
        StartCoroutine(nameof(Encender));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(nameof(Encender));
        }
    }

    IEnumerator Encender()
    {
        float timer = 0;

        while (timer < 1)
        {
            _material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.white, timer));
            timer += Time.deltaTime;
            yield return null;
        }
        _material.SetColor("_EmissionColor", Color.white);
        _firePS.Play();

        _boxCollider.enabled = true;
        yield return new WaitForSeconds(_firePS.main.duration);
        _boxCollider.enabled = false;
        _isBurning = false;

        timer = 0;
        while (timer < 1)
        {
            _material.SetColor("_EmissionColor", Color.Lerp(Color.white, Color.black, timer));
            timer += Time.deltaTime;
            yield return null;
        }
        _material.SetColor("_EmissionColor", Color.black);

        yield return new WaitForSeconds(_cooldown);

        StartCoroutine(nameof(Encender));

    }

    IEnumerator BurningDamage(Entity entity)
    {
        while (_isBurning)
        {
            entity.TakeDamage(_damage);
            Debug.Log("Recibi daño");
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity _entity))
        {
            _isBurning = true;
            StartCoroutine(BurningDamage(_entity));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity _entity))
        {
            _isBurning = false;
        }
    }
}
