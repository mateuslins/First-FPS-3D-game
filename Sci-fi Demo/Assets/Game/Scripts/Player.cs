using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _muzzle_flash;
    [SerializeField]
    private GameObject _hitMarker;
    [SerializeField]
    private GameObject _weapon;

    private CharacterController _controller;
    private AudioSource _audioSource;
    private UIManager _uiManager;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 150;
    [SerializeField]
    private int coins = 0;

    private bool isRealoading = false;
    private bool isWeaponActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        Shoot();

        Reload();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * _speed;

        velocity.y -= _gravity;
        velocity = transform.transform.TransformDirection(velocity);

        _controller.Move(velocity * Time.deltaTime);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0 && isWeaponActive == true)
        {
            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            currentAmmo--;
            _uiManager.UpdateAmmo(currentAmmo, maxAmmo);

            if (Physics.Raycast(rayOrigin, out hitInfo)) // Ponto de onde parte e ponto de onde colide
            {
                Debug.Log("Raycast hit " + hitInfo.transform.name);
                GameObject hitMarker = Instantiate(_hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                Destroy(hitMarker, 1f);

                // Verificar se atingiu a caixa
                Destructable crate = hitInfo.transform.GetComponent<Destructable>();
                if (crate != null)
                {
                    crate.DestroyCrate();
                }
            }

            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
            _muzzle_flash.SetActive(true);
        }
        else
        {
            _audioSource.Stop();
            _muzzle_flash.SetActive(false);
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && isRealoading == false)
        {
            isRealoading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo, maxAmmo);
        isRealoading = false;
    }

    public void CollectCoin()
    {
        coins++;
    }

    public bool BuyWeapon()
    {
        if (coins != 0)
        {
            coins--;
            _weapon.SetActive(true);
            isWeaponActive = true;
            return true;
        }
        return false;
    }
}
