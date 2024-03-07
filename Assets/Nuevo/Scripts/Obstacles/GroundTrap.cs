using UnityEngine;

public class GroundTrap : MonoBehaviour
{
    PlayerLogic _player;
    Animator _anim;
    [SerializeField] BoxCollider _spikeHitbox;
    [SerializeField] float _startingTime = 0.1f;
    [SerializeField] float _damage = 20f;
    //[SerializeField] bool _deactivated = false;

    [SerializeField] float _distance;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        // if(!_deactivated) Invoke(nameof(StartMovementAnim), _startingTime);
    }

    private void Start()
    {
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < _distance) Invoke(nameof(StartMovementAnim), _startingTime);
        else { IdlePositionAnim(); }
    }

    void StartMovementAnim() => _anim.SetBool("Active", true);
    void IdlePositionAnim() => _anim.SetBool("Active", false);

    void OnObjectHit() => _spikeHitbox.enabled = false;
    void ReactivateSpikesHitBox() => _spikeHitbox.enabled |= true;

    void PlaySound(string sound) => AudioManager.Instance.PlaySFX(sound);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity _entity))
        {
            OnObjectHit();
            _entity.TakeDamage(_damage);
            Debug.Log("Hizo " + _damage + " de Daño");
        }
    }
}
