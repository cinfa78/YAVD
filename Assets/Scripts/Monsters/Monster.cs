using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour, IDamageable {
	public SMonsterBrain brain;
	public SMonsterSharedStats stats;

	[HideInInspector] public float hp;
	[HideInInspector] public bool canAttack = true;
	[HideInInspector] public bool canMove = true;

	[FormerlySerializedAs("allerted")] public bool alerted = false;
	public Vector3 target;
	public Vector3 aim;
	public NavMeshAgent agent;
	public SEvent monsterKilled;

	public MonsterState state;
	public MonsterState previousState;

	Rigidbody rigidBody;
	public AudioSource audioSource;

	private void OnEnable() {
		aim = Vector3.forward;
		agent.speed = stats.speed;
	}

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		hp = stats.health;
		agent = GetComponent<NavMeshAgent>();
		state = MonsterState.idle;
		previousState = MonsterState.idle;
		rigidBody = GetComponent<Rigidbody>();
		agent.speed = stats.speed;
		agent.acceleration = stats.speed;
	}

	public void GetAllerted(Vector3 target) {
		alerted = true;
		SetTarget(target);
		state = MonsterState.alerted;
	}

	public void SetTarget(Vector3 target) {
		this.target = target;
	}

	public void StopAlert() {
		alerted = false;
		state = MonsterState.idle;
		target = Vector3.up;
	}

	public void Melee() {
		if (canAttack)
			StartCoroutine(MeleeCoroutine());
	}

	IEnumerator MeleeCoroutine() {
		canAttack = false;
		yield return new WaitForSeconds(stats.attackCooldown);
		canAttack = true;
	}

	public void Ranged() {
		if (canAttack)
			StartCoroutine(RangedCoroutine());
	}

	IEnumerator RangedCoroutine() {
		//print("Ranged");
		canAttack = false;
		yield return new WaitForSeconds(stats.attackCooldown);
		canAttack = true;
	}

	public void PauseMovement(float seconds) {
		if (canMove)
			StartCoroutine(PauseMovementCoroutine(seconds));
	}

	IEnumerator PauseMovementCoroutine(float seconds) {
		canMove = false;
		agent.velocity *= 0.5f;
		yield return new WaitForSeconds(seconds);
		canMove = true;
	}

	public void GoTo(Vector3 destination) {
		if (canMove)
			agent.SetDestination(destination);
	}

	public void Rotate(float yAngle) {
		if (canMove) {
			transform.Rotate(Vector3.up * yAngle * (Time.deltaTime * 100f));
			agent.updateRotation = true;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(target, 4f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(aim, 3f);
	}

	void Update() {
		brain.Think(this);
		Debug.DrawLine(transform.position, target);
		Debug.DrawLine(transform.position, aim);
	}

	public void Die() {
		state = MonsterState.dead;
		monsterKilled.Raise();
		Destroy(gameObject);
	}

	public void Hit(float damageReceived, Vector3 hitter) {
		//aggiunge una forza di deflect 
		if (rigidBody != null) {
			aim = hitter;
			target = hitter;
			StartCoroutine(HitKickback(hitter));
		}
		//e poi considera il colpo in maniera normale
		Hit(damageReceived);
	}

	IEnumerator HitKickback(Vector3 hitPosition) {
		float t = Time.time;
		float endt = t + 1f;
		state = MonsterState.hurt;
		canMove = false;
		agent.enabled = false;
		rigidBody.isKinematic = false;
		rigidBody.AddExplosionForce(3000f, hitPosition, 32f);
		while (t < endt) {
			t += Time.deltaTime;
			yield return null;
		}
		rigidBody.isKinematic = true;
		agent.enabled = true;
		canMove = true;
		state = MonsterState.alerted;
	}

	public void Hit(float damageReceived) {
		print(name + " has been Hit");
		GameObjectPool.instance.Spawn("Blood", transform.position + Vector3.up * 8f, transform.rotation, Vector3.one);
		hp -= damageReceived;
		if (hp <= 0) {
			hp = 0f;
		}
	}

	public void EnableAgent(bool enabled) {
		agent.enabled = enabled;
	}

	public float Damage(float damage, Vector3 from) {
		return brain.Damage(this, damage, from);
	}
}