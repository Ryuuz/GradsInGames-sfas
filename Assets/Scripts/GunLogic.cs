using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    // The Bullet Prefab
    [SerializeField]
    GameObject m_BulletPrefab;

    // The Explosive Bullet Prefab
    [SerializeField]
    GameObject m_ExplosiveBulletPrefab;

    // The Bullet Spawn Point
    [SerializeField]
    Transform m_BulletSpawnPoint;

    // The Bullet Spawn Point
    [SerializeField]
    float m_ShotCooldown = 0.5f;

    bool m_CanShoot = true;

    // VFX
    [SerializeField]
    ParticleSystem m_Flare;

    [SerializeField]
    ParticleSystem m_Smoke;

    [SerializeField]
    ParticleSystem m_Sparks;

    // SFX
    [SerializeField]
    AudioClip m_BulletShot;

    [SerializeField]
    AudioClip m_GrenadeLaunched;

    // The AudioSource to play Sounds for this object
    AudioSource m_AudioSource;

    [SerializeField]
    int m_BulletAmmo = 100;

    [SerializeField]
    int m_GrenadeAmmo = 5;

    UIManager m_UIManager;

    // Use this for initialization
    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_UIManager = FindObjectOfType<UIManager>();

        // Update UI
        if (m_UIManager)
        {
            m_UIManager.SetAmmoText(m_BulletAmmo, m_GrenadeAmmo);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_CanShoot)
        {
            m_ShotCooldown -= Time.deltaTime;
            if (m_ShotCooldown < 0.0f)
            {
                m_CanShoot = true;
            }
        }

        if (m_CanShoot)
        {
            if(Input.GetButtonDown("Fire1") && m_BulletAmmo > 0)
            {
                Fire();
                m_CanShoot = false;
            }
            else if (Input.GetButtonDown("Fire2") && m_GrenadeAmmo > 0)
            {
                FireGrenade();
                m_CanShoot = false;
            }
        }
    }

    void Fire()
    {
        if(m_BulletPrefab)
        {
            // Reduce the Ammo count
            --m_BulletAmmo;

            // Create the Projectile from the Bullet Prefab
            Instantiate(m_BulletPrefab, m_BulletSpawnPoint.position, transform.rotation * m_BulletPrefab.transform.rotation);

            // Play Particle Effects
            PlayGunVFX();

            // Play Sound effect
            if(m_AudioSource && m_BulletShot)
            {
                m_AudioSource.PlayOneShot(m_BulletShot);
            }

            // Update UI
            if(m_UIManager)
            {
                m_UIManager.SetAmmoText(m_BulletAmmo, m_GrenadeAmmo);
            }
        }
    }

    void FireGrenade()
    {
        if(m_ExplosiveBulletPrefab)
        {
            // Reduce the Ammo count
            --m_GrenadeAmmo;

            // Create the Projectile from the Explosive Bullet Prefab
            Instantiate(m_ExplosiveBulletPrefab, m_BulletSpawnPoint.position, transform.rotation * m_ExplosiveBulletPrefab.transform.rotation);

            // Play Particle Effects
            PlayGunVFX();

            // Play Sound effect
            if (m_AudioSource && m_GrenadeLaunched)
            {
                m_AudioSource.PlayOneShot(m_GrenadeLaunched);
            }

            // Update UI
            if(m_UIManager)
            {
                m_UIManager.SetAmmoText(m_BulletAmmo, m_GrenadeAmmo);
            }
        }
    }

    void PlayGunVFX()
    {
        if (m_Flare)
        {
            m_Flare.Play();
        }

        if (m_Sparks)
        {
            m_Sparks.Play();
        }

        if (m_Smoke)
        {
            m_Smoke.Play();
        }
    }

    public void AddAmmo(int bullets, int grenades)
    {
        m_BulletAmmo += bullets;
        m_GrenadeAmmo += grenades;

        // Update UI
        if (m_UIManager)
        {
            m_UIManager.SetAmmoText(m_BulletAmmo, m_GrenadeAmmo);
        }
    }
}
