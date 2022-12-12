using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables RigidBody
    Rigidbody _playerRB;
    #endregion

    #region Variables Vida y Ataque
    private const float MAX_HEALTH = 100f;
    private const int MAX_LIVES = 3;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isAttacking = false;
    [SerializeField]
    private bool _attackEnemy = false;
    [SerializeField]
    private HealthBarManager healthBar;
    [SerializeField]
    private string healthBarColorType;
    private bool isProtected = false;
    
    private LivesManager _livesManager;
    private ScoreManager _scoreManager;
    #endregion

    #region Variables Movimiento
    [SerializeField]
    private float _maxSpeed = 8f;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _forwardInput, _horizontalInput;
    #endregion

    #region Variables Brinco
    [SerializeField]
    private bool _jumpRequest = false;
    [SerializeField]
    private float _jumpForce = 5;
    [SerializeField]
    private int _availableJumps = 0, _maxJumps = 2;
    #endregion

    #region Variables Animation
    private PlayerAnimation _playerAnimation;
    private bool _isRunning;

    #endregion

    #region Variables Reaparición
    [SerializeField]
    private Transform spawnPoint;
    private bool isAlive = true;
    #endregion

    #region Variables Salvaditos
    [SerializeField]
    private int _hostages = 0;
    #endregion

    #region Variables de Efectos de sonido
    private AudioManager _audioManager;
    #endregion

    private void Start()
    {

        #region Obtener RigidBody
        _playerRB = GetComponent<Rigidbody>();
        if (_playerRB == null)
        {
            Debug.LogWarning("No hay RigidBody");
        }
        #endregion

        #region Obtener Player Animation
        _playerAnimation = GetComponent<PlayerAnimation>();
        if (_playerAnimation == null)
        {
            Debug.LogWarning("No hay Player Animation en el jugador");
        }
        #endregion

        #region Establecer isRunning
        _isRunning = true;
        #endregion
        
        #region Establecer la salud y color en la barra de vida
        healthBar.SetHealthBarColor(healthBarColorType);
        healthBar.UpdateHealthBar(MAX_HEALTH, _health);
        #endregion

        #region Inicializar el lives manager y score manager
        _livesManager = GameObject.Find("LivesCounter").GetComponent<LivesManager>();
        _scoreManager = GameObject.Find("HostagesScore").GetComponent<ScoreManager>();
        #endregion

        #region Inicializar el audio manager
        _audioManager = GameObject.Find("Sound_Effects").GetComponent<AudioManager>();
        #endregion

        #region Establecer los 3 corazones de vida y cantidad de vidas
        _livesManager.UpdateLivesImages(_lives);
        this._lives = MAX_LIVES;
        #endregion

        #region Establecer 0 salvaditos en un inicio
        _scoreManager.UpdateSalvaditosImages(_hostages);
        #endregion

    }
  

    
    // Update is called once per frame
    void Update()
    {

        #region Caminar/Correr
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isRunning = !_isRunning;
            isProtected = !isProtected;
        }

        if (_isRunning)
        {
            _speed = _maxSpeed;
        }
        else
        {
            _speed = _maxSpeed / 3;
        }
        #endregion

        #region Movimiento
        _horizontalInput = Input.GetAxis("Horizontal"); // izq, der, A, D   
        _forwardInput = Input.GetAxis("Vertical"); // arr, ab, W, S

        float velocity = Mathf.Max(Mathf.Abs(_horizontalInput), Mathf.Abs(_forwardInput));
        if (_isRunning)
        {
            _playerAnimation.SetSpeed(velocity);
        }
        else
        {
            _playerAnimation.SetSpeed(velocity / 3);
        }

        // X, Y, Z
        Vector3 movement = new Vector3(_horizontalInput, 0, _forwardInput);
        if (isAlive)
        {
            transform.Translate(movement * _speed * Time.deltaTime);
        }
        #endregion

        #region Petición Brinco
        if (Input.GetKeyDown(KeyCode.Space) && _availableJumps > 0 && isAlive)
        {
            _jumpRequest = true;
        }
        #endregion

        #region Petición Ataque
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isAttacking = true;
            _attackEnemy = true;
        }
        #endregion

        #region Actualizar la salud en la barra de vida
        healthBar.UpdateHealthBar(MAX_HEALTH, _health);
        #endregion

        #region Llamar a actualizar vidas
        _livesManager.UpdateLivesImages(_lives);
        #endregion

        #region Establecer 0 salvaditos en un inicio
        _scoreManager.UpdateSalvaditosImages(_hostages);
        #endregion

    }


    private void ResetPlayer()
    {
        #region Restablecer jugador
        _playerAnimation.SetRespawn();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        _health = 100;
        isAlive = true;
        #endregion
    }

    private void GameOverPlayer()
    {
        Debug.Log("Muerte del personaje");
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    private void WinPlayer()
    {
        Debug.Log("Victoria");
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    private void FixedUpdate()
    {
        #region Brincar
        if (_jumpRequest)
        {
            _playerRB.velocity = Vector3.up * _jumpForce;
            _audioManager.PlayAudio("PlayerJump");
            if (_availableJumps == 1)
            {
                _playerAnimation.SetDoubleJump();
            }
            else
            {
                _playerAnimation.SetJump();
            }
            _availableJumps--;
            _jumpRequest = false;
        }
        #endregion

        #region Perdida de vidas del jugador y revivirlo
        if (_health <= 0 && isAlive)
        {

            _playerAnimation.SetDeath();
            isAlive = false;
            Invoke("ResetPlayer", 2);
            _lives--;    
        }
        #endregion

        #region Muere el jugador
        // Ir a la pantalla de Gameover
        if (_lives <= 0)
        {
            Invoke("GameOverPlayer", 1.5f);
        }
        #endregion

        #region Atacar
        if (_isAttacking)
        {
            _playerAnimation.SetAttack();
            _audioManager.PlayAudio("PlayerAttack");
            _isAttacking = false;
        }
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {
        #region Colisión con el piso
        if (collision.gameObject.CompareTag("Ground"))
        {
            _availableJumps = _maxJumps;
        }
        #endregion

        #region Colisión con las balas
        if (collision.gameObject.CompareTag("Bullet") && !isProtected)
        {
            if (_health > 0)
            {
                _health -= 10;
                _audioManager.PlayAudio("PlayerHit");
            }
        }
        #endregion

        #region Colisión con la posión de salud
        if (collision.gameObject.CompareTag("Health"))
        {
            if (_health < 100)
            {
                _health += 10;
            }
            if (_health > 100)
            {
                _health = 100;
            }
            _audioManager.PlayAudio("PlayerHeal");
        }
        #endregion

        #region Colision salvaditos
        if (collision.gameObject.CompareTag("Hostage"))
        {
            _hostages++;
            _audioManager.PlayAudio("PlayerScore");
        }
        #endregion

        #region Colision portal
        if (collision.gameObject.CompareTag("Portal") && _hostages == 3)
        {
            _audioManager.PlayAudio("EnterPortal");
            Invoke("WinPlayer", 2f);
        }
        #endregion

    }

    private void OnCollisionStay(Collision collision)
    {
        #region Colision con enemigo
        if (collision.gameObject.CompareTag("Enemy") && _attackEnemy)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(50);
            _audioManager.PlayAudio("EnemyDamage");
            _attackEnemy = false;
        }
        #endregion
    }
}
