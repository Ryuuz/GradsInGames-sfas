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
    public KibbleAmmo[] m_Kibbles = new KibbleAmmo[4];

    // --------------------------------------------------------------

    // The kibble Spawn Point
    [SerializeField]
    private Transform m_KibbleSpawnPoint;

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
    private float m_ShotCooldown = 0.5f;

    // The type of kibble that is currently active
    private int m_CurrentKibble;

    // The AudioSource to play Sounds for this object
    private AudioSource m_AudioSource;
    private UIManager m_UIManager;

    // --------------------------------------------------------------

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_UIManager = FindObjectOfType<UIManager>();

        // Set the current kibble to the first one the player has any of
        for(int i = 0; i < m_Kibbles.Length; i++)
        {
            if(m_Kibbles[i].kibbleAmount > 0)
            {
                m_CurrentKibble = i;
                i = m_Kibbles.Length;
            }
        }

        // Update UI
        if(m_UIManager)
        {
            for(int i = 0; i < 4; i++)
            {
                m_UIManager.SetAmmoText(m_Kibbles[i].kibbleAmount, i);
            }

            m_UIManager.SetActiveAmmo(m_CurrentKibble);
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        // Update cooldown for shooting
        if(!m_CanShoot)
        {
            m_ShotCooldown -= Time.deltaTime;
            if(m_ShotCooldown < 0.0f)
            {
                m_CanShoot = true;
            }
        }
    }

    public void ShootKibble()
    {
        // Shoot if possible
        if(m_CanShoot)
        {
            if(m_Kibbles[m_CurrentKibble].kibbleAmount > 0)
            {
                Fire();
                m_CanShoot = false;
            }
        }
    }

    public void ChangeKibble(int kibbleNumber)
    {
        // Change active kibble type
        if( m_Kibbles[kibbleNumber].kibbleAmount > 0)
        {
            m_CurrentKibble = kibbleNumber;
            m_UIManager.SetActiveAmmo(m_CurrentKibble);
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
