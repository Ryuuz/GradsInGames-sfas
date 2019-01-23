using UnityEngine;

// Ammo type and amount
[System.Serializable]
public struct KibbleAmmo
{
    public GameObject kibblePrefab;
    public int kibbleAmount;
}

public class GunLogic : MonoBehaviour
{
    [SerializeField]
    public KibbleAmmo[] m_Kibbles = new KibbleAmmo[4];

    // --------------------------------------------------------------

    // The Bullet Spawn Point
    [SerializeField]
    private Transform m_KibbleSpawnPoint;

    // The Kibble Spawn Point
    [SerializeField]
    private float m_ShotCooldown = 0.5f;

    // VFX
    [SerializeField]
    private ParticleSystem m_Flare;

    [SerializeField]
    private ParticleSystem m_Smoke;

    [SerializeField]
    private ParticleSystem m_Sparks;

    // SFX
    [SerializeField]
    private AudioClip m_KibbleShot;

    private bool m_CanShoot = true;

    // The type of kibble that is currently active
    private int m_CurrentKibble;

    // The AudioSource to play Sounds for this object
    private AudioSource m_AudioSource;
    private UIManager m_UIManager;

    // --------------------------------------------------------------

    void Start ()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_UIManager = FindObjectOfType<UIManager>();
        m_CurrentKibble = 0;

        // Update UI
        if (m_UIManager)
        {
            for(int i = 0; i < 4; i++)
            {
                m_UIManager.SetAmmoText(m_Kibbles[i].kibbleAmount, i);
            }

            m_UIManager.SetActiveAmmo(m_CurrentKibble);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Change active kibble type
        if(Input.GetButtonDown("Ammo1") && m_Kibbles[0].kibbleAmount > 0)
        {
            m_CurrentKibble = 0;
            m_UIManager.SetActiveAmmo(m_CurrentKibble);
        }
        if (Input.GetButtonDown("Ammo2") && m_Kibbles[1].kibbleAmount > 0)
        {
            m_CurrentKibble = 1;
            m_UIManager.SetActiveAmmo(m_CurrentKibble);
        }
        if (Input.GetButtonDown("Ammo3") && m_Kibbles[2].kibbleAmount > 0)
        {
            m_CurrentKibble = 2;
            m_UIManager.SetActiveAmmo(m_CurrentKibble);
        }
        if (Input.GetButtonDown("Ammo4") && m_Kibbles[3].kibbleAmount > 0)
        {
            m_CurrentKibble = 3;
            m_UIManager.SetActiveAmmo(m_CurrentKibble);
        }

        // Update cooldown for shooting
        if (!m_CanShoot)
        {
            m_ShotCooldown -= Time.deltaTime;
            if (m_ShotCooldown < 0.0f)
            {
                m_CanShoot = true;
            }
        }

        // Shoot if possible
        if (m_CanShoot)
        {
            if(Input.GetButtonDown("Fire1") && m_Kibbles[m_CurrentKibble].kibbleAmount > 0)
            {
                Fire();
                m_CanShoot = false;
            }
        }
    }

    void Fire()
    {
        if(m_Kibbles[m_CurrentKibble].kibblePrefab)
        {
            // Reduce the Ammo count
            --m_Kibbles[m_CurrentKibble].kibbleAmount;

            // Create the Projectile from the Bullet Prefab
            Instantiate(m_Kibbles[m_CurrentKibble].kibblePrefab, m_KibbleSpawnPoint.position, transform.rotation * m_Kibbles[m_CurrentKibble].kibblePrefab.transform.rotation);

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
                m_UIManager.SetAmmoText(m_Kibbles[m_CurrentKibble].kibbleAmount, m_CurrentKibble);
            }
        }
    }

    // Play the particle effects for the gun
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

    // Returns the total amount of kibbles left
    public int GetAllAmmo()
    {
        int amount = 0;

        for(int i = 0; i < m_Kibbles.Length; i++)
        {
            amount += m_Kibbles[i].kibbleAmount;
        }

        return amount;
    }
}
