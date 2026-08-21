using UnityEngine;

public class EXPPickup : PickupBase
{
    private GameObject player;

    [SerializeField] private float attractSpeed = 8f;
    [SerializeField] private float acceleration = 20f;
    private float curSpeed;

    public override void Start()
    {
        base.Start();
        player = PlayerSingleton.instance.gameObject;
    }

    private void FixedUpdate()
    {
        if(player == null ) return;
        Vector3 toPlayer = player.transform.position - transform.position;

        float distance = toPlayer.magnitude;

        curSpeed = Mathf.MoveTowards(curSpeed, attractSpeed, acceleration * Time.fixedDeltaTime);

        Vector3 direction = toPlayer.normalized;

        transform.position += direction * curSpeed * Time.fixedDeltaTime;
    }
}
