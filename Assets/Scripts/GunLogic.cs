using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    // The Kibble Prefabs
    [SerializeField]
    GameObject m_StandardKibble;
    [SerializeField]
    GameObject m_StarKibble;
    [SerializeField]
    GameObject m_FishKibble;
    [SerializeField]
    GameObject m_TriangleKibble;

    // The Bullet Spawn Point
    [SerializeField]
    Transform m_KibbleSpawnPoint;

    // The Kibble Spawn Point
    [SerializeField]
    float m_ShotCooldown = 0.5f;

    bool m_CanShoot = true;
    GameObject m_CurrentKibble;

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
    int m_StandardKibbleAmmo = 100;
    int m_StarKibbleAmmo = 100;
    int m_FishKibbleAmmo = 100;
    int m_TriangleKibbleAmmo = 100;

    UIManager m_UIManager;

    // Use this for initialization
    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_UIManager = FindObjectOfType<UIManager>();

        // Update UI
        if (m_UIManager)
        {
            m_UIManager.SetAmmoText(m_StandardKibbleAmmo);
        }

        m_CurrentKibble = m_StandardKibble;
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
            if(Input.GetButtonDown("Fire1") && m_StandardKibbleAmmo > 0)
            {
                Fire();
                m_CanShoot = false;
            }
        }
    }

    void Fire()
    {
        if(m_StandardKibble)
        {
            // Reduce the Ammo count
            --m_StandardKibbleAmmo;

            // Create the Projectile from the Bullet Prefab
            Instantiate(m_CurrentKibble, m_KibbleSpawnPoint.position, transform.rotation * m_CurrentKibble.transform.rotation);

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
                m_UIManager.SetAmmoText(m_StandardKibbleAmmo);
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
