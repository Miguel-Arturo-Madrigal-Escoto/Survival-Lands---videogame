using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    #region Variables navmesh
    [SerializeField]
    private UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;
    #endregion

    #region Variables patrullar
    [SerializeField]
    private Vector3 walkPoint;
    [SerializeField]
    private bool walkPointSet;
    [SerializeField]
    private float walkPointRange;
    #endregion

    #region Variables atacar
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private bool alreadyAttacked;
    [SerializeField]
    private GameObject projectile;
    #endregion

    #region Variables vida
    [SerializeField]
    private float health;
    [SerializeField]
    private HealthBarManager healthBar;
    [SerializeField]
    private string healthBarColorType;
    #endregion

    #region Variables animacion
    private EnemyAnimation _enemyAnimation;
    #endregion

    #region Variables extra
    [SerializeField]
    private float sightRange, attackRange;
    [SerializeField]
    private bool playerInSightRange, playerInAttackRange;
    private const float MAX_HEALTH = 100f;
    #endregion

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar rangos
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        #region Actualizar la salud en la barra de vida
        healthBar.UpdateHealthBar(MAX_HEALTH, health);
        #endregion
    }


    private void Patroling()
    {
        _enemyAnimation.SetWalk();

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            _agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Destino alcanzado
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        // Calcular punto aleatorio en el rango
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _enemyAnimation.SetWalk();
        _agent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        // Modificar animación y destino del enemigo para atacar al jugador
        _enemyAnimation.SetIdle();
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);

        if (!alreadyAttacked)
        {
            // Cambiar animación a ataque
            _enemyAnimation.SetAttack();

            // Instanciar objeto (proyectil)
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 28f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            _enemyAnimation.SetDie();
            Invoke(nameof(DestroyEnemy), 1.3f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void Start()
    {
        #region Obtener Enemy Animation
        _enemyAnimation = GetComponent<EnemyAnimation>();
        if (_enemyAnimation == null)
        {
            Debug.LogWarning("No hay Enemy Animation en el enemigo");
        }
        #endregion

        #region Establecer la salud y color en la barra de vida
        healthBar.SetHealthBarColor(healthBarColorType);
        healthBar.UpdateHealthBar(MAX_HEALTH, health);
        #endregion
    }
}
