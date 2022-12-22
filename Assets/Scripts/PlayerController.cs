using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	public int health;
	public int damage;
	public float jumpForce;
	public float moveSpeed;
	private float moveInput;

	public KeyCode rightInput;
	public KeyCode leftInput;
	public KeyCode jumpInput;
	public KeyCode downInput;

	public float knockback;
	public float knockbackLenght;
	private float knockbackCount;
	private bool knockbackFromRight;

	private bool isCooldown;
	public float cooldownLenght;
	private float cooldownTime;

	private bool isAttack;
	public Transform attackPos;
	public float attackRange;
	public LayerMask whatIsEnemies;

	private bool isGrounded;
	public Transform feetPos;
	public float checkRadius;

	public LayerMask whatIsGround;
	private Animator anim;
	private SpriteRenderer sprite;
	private IEnumerator coroutine;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
		coroutine = Flashing ();
	}

	void FixedUpdate() {
		if (knockbackCount <= 0) {
			rb.velocity = new Vector2 (moveInput * moveSpeed, rb.velocity.y);
		} else {
			if (knockbackFromRight) {
				rb.velocity = new Vector2 (-knockback, knockback);
			} else {
				rb.velocity = new Vector2 (knockback, knockback);
			}
			knockbackCount -= Time.deltaTime;
		}

		if (Input.GetKey (rightInput)) {
			moveInput = 1;
		} else if (Input.GetKey (leftInput)) {
			moveInput = -1;
		} else if (!Input.GetKey (rightInput) && !Input.GetKey (leftInput)) {
			moveInput = 0;
		}
	}

	void Update() {
		isGrounded = Physics2D.OverlapCircle (feetPos.position, checkRadius, whatIsGround);

		if (Input.GetKey (rightInput)) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else if (Input.GetKey (leftInput)) {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}

		//jump code
		if (isGrounded && Input.GetKeyDown(jumpInput)) {
			rb.velocity = Vector2.up * jumpForce;
		}

		if (!isGrounded && Input.GetKeyDown(downInput)) {
			rb.velocity = Vector2.down * jumpForce;
		}

		if (health <= 0) {
			Physics2D.IgnoreLayerCollision (0, 9, false);
			Destroy (gameObject);
		}
			
		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger ("Attack");
			Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll (attackPos.position, attackRange, whatIsEnemies);
			for (int i = 0; i < enemiesToDamage.Length; i++) {
				enemiesToDamage [i].GetComponent<Enemy> ().TakeDamage ();
			}
		}
			
		if (isCooldown) {
			if (cooldownTime <= 0) {
				Physics2D.IgnoreLayerCollision (0, 9, false);
				StopCoroutine (coroutine);
				sprite.color = Color.white;
				isCooldown = false;
			} else {
				cooldownTime -= Time.deltaTime;
			}
		}
	}
		
	public void Attacked(int enemyDamage) {
		health -= enemyDamage;
		knockbackCount = knockbackLenght;

		//player cooldown
		isCooldown = true;
		cooldownTime = cooldownLenght;
		Physics2D.IgnoreLayerCollision (0, 9, true);
		StartCoroutine (coroutine);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (attackPos.position, attackRange);
	}

	public int getHealth() {
		return health;
	}

	public int getDamage() {
		return damage;
	}

	public void attackAnimation() {
		anim.SetTrigger ("Attack");
	}

	public bool IsKnockbackFromRight {
		get { return knockbackFromRight; }
		set { knockbackFromRight = value; }
	}

	public bool IsAttack {
		get { return isAttack; }
		set { isAttack = value; }
	}

	public bool IsCooldown {
		get { return isCooldown; }
		set { isCooldown = value; }
	}

	IEnumerator Flashing(){
		while (true) {
			sprite.color = Color.clear;
			yield return new WaitForSeconds (0.1f);
			sprite.color = Color.white;
			yield return new WaitForSeconds (0.1f);
		}
	}
}