using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    Animator _anim;
    [SerializeField] BoxCollider _spikeHitbox;
    [SerializeField] float _startingTime = 0.1f;
    [SerializeField] float _damage = 20f;
    [SerializeField] bool _deactivated = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        if(!_deactivated) Invoke(nameof(StartMovementAnim), _startingTime);
    }

    void StartMovementAnim() => _anim.SetBool("Active", true);
    void IdlePositionAnim() => _anim.SetBool("Active", false);

    //void SwitchAnim() => _anim.SetBool("Active", _anim.GetBool("Active") ? false : true);
    //public void StopMovement() => _anim.speed = 0f;
    //public void ReanudeMovement() => _anim.speed = 1f;

    void OnObjectHit() => _spikeHitbox.enabled = false;
    void ReactivateSpikesHitBox() => _spikeHitbox.enabled |= true;

    void PlaySound(string sound) => AudioManager.Instance.PlaySFX(sound);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity _entity))
        {
            OnObjectHit();
            _entity.TakeDamage(_damage);
            Debug.Log("Hizo Daño");
            //_entity.
        }
    }
}
