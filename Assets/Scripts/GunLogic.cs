using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    // The Kibble Prefab
    [SerializeField]
    GameObject m_KibblePrefab;

    // The Bullet Spawn Point
    [SerializeField]
    Transform m_KibbleSpawnPoint;

    // The Kibble Spawn Point
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
    AudioClip m_KibbleShot;

    // The AudioSource to play Sounds for this object
    AudioSource m_AudioSource;

    [SerializeField]
    int m_KibbleAmmo = 100;

    UIManager m_UIManager;

    // Use this for initialization
    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_UIManager = FindObjectOfType<UIManager>();

        // Update UI
        if (m_UIManager)
        {
            m_UIManager.SetAmmoText(m_KibbleAmmo);
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
            if(Input.GetButtonDown("Fire1") && m_KibbleAmmo > 0)
            {
                Fire();
                m_CanShoot = false;
            }
        }
    }

    void Fire()
    {
        if(m_KibblePrefab)
        {
            // Reduce the Ammo count
            --m_KibbleAmmo;

            // Create the Projectile from the Bullet Prefab
            Instantiate(m_KibblePrefab, m_KibbleSpawnPoint.position, transform.rotation * m_KibblePrefab.transform.rotation);

            // Play Particle Effects
            PlayGunVFX();

            // Play Sound effect
            if(m_AudioSource && m_KibbleShot)
            {
                m_AudioSource.PlayOneShot(m_KibbleShot);
            }

            // Update UI
            if(m_UIManager)
            {
                m_UIManager.SetAmmoText(m_KibbleAmmo);
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
}
