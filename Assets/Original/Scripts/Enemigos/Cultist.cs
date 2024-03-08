using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Cultist : Entity
{
    PlayerLogic _player;
    [SerializeField] Animator _anim;
    [SerializeField] NavMeshAgent _cultistNavM;

    [SerializeField] int _attackRange;
    [SerializeField] float _damage;

    [SerializeField] bool _detectado;
    [SerializeField] bool _canAttack;

    [Header("Materiales muerte")]
    [SerializeField] Material _deadBody;
    [SerializeField] Material _deadMask;
    [SerializeField] SkinnedMeshRenderer _body;
    [SerializeField] SkinnedMeshRenderer _mask;

    private void Awake()
    {
        currentHP = _maxHP;
    }

    void Start()
    { 
        _anim = GetComponentInParent<Animator>();
        _cultistNavM = GetComponentInParent<NavMeshAgent>();
        _player = GameManager.Instance.Player;
        _cultistNavM.speed = speed;
        _canAttack = true;
    }

    void DetectPlayer()
    {
        _detectado = true;
        _anim.SetBool("EnRango", true);
    }

    void LostPlayer()
    {
        _detectado = false;
        _anim.SetBool("EnRango", false);
        _anim.SetBool("AtackRange", false);
        _cultistNavM.isStopped = true;
    }

    void ChasePlayer()
    {
        if (GetDistance() <= _attackRange)
        {
            _cultistNavM.isStopped = true;

            if (_canAttack)
            {
                _anim.SetBool("AtackRange", true);
                StartCoroutine(AttackPlayer());
            }
            _anim.SetBool("EnRango", false);
        }
        else
        {
            _cultistNavM.isStopped = false;
            _anim.SetBool("AtackRange", false);
            _anim.SetBool("EnRango", true);
            _cultistNavM.SetDestination(_player._interactionPoint.transform.position);
        }
    }

    public IEnumerator AttackPlayer()
    {
        _canAttack = false;
        yield return new WaitForSeconds(2);
        _player.TakeDamage(_damage);
        yield return new WaitForSeconds(1);
        _canAttack = true;
    }

    float GetDistance()
    {
        Vector3 distanceVector = _player._interactionPoint.transform.position - transform.position;
        float distanceSquared = distanceVector.sqrMagnitude;
        return Mathf.Sqrt(distanceSquared); //distancia
    }
   
    private void OnTriggerEnter(Collider other)
    {
        DetectPlayer();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_detectado) ChasePlayer();
    }

    private void OnTriggerExit(Collider other)
    {
        LostPlayer();
    }

    public override void Die()
    {
        _body.material = _deadBody;
        _mask.material = _deadMask;
        _anim.enabled = false;
        Destroy(this);
    }

    //Vector3 GetDirection()
    //{
    //    return _player._interactionPoint.transform.position - transform.position;
    //}
}
