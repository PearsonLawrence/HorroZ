using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public Image healthBar;
    public GameObject player;
    public Image medkit;
    public AudioSource heal;
    public int MedPack;
    // Use this for initialization
    public float healthValue = 0;
    public float startHealth;
    public GameObject fade;
    float keyPress;

    public void TakeDamage(float damageDealt)
    {
        healthValue -= damageDealt;

    }
    void Start()
    {
        startHealth = healthValue;
    }

    void Update()
    {
        if (healthValue > 0)
        {
            MedPack = player.GetComponent<FPScontroller>().MedPacks;

            if (MedPack > 0)
            {

                keyPress -= Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.F) && MedPack > 0 && keyPress <= 0 && healthValue < startHealth)
            {
                heal.Play();
                player.GetComponent<FPScontroller>().MedPacks -= 1;
                healthValue += 50;
                keyPress = 1;
                medkit.gameObject.SetActive(false);
            }
            healthBar.fillAmount = healthValue / 100;
        }
        if(healthValue <= 0)
        {
            fade.GetComponent<fading>().FadeOut = true;
        }
    }
}
