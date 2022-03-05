using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public float StartingHealth = 10;
    [Header("Invincible")]
    public bool Invincible = false;
    public Color InvincibleFlickerColor = Color.yellow;

    [Header("Invulnerable")]
    public float InvulnerableTime = 1;
    [Header("Damaged Feedback")]
    public Color DamageTakenFlickerColor = Color.red;

    private float _health = 0;
    private float _invulnerableTime = 0;
    private bool _currentlyInvulnerable = false;
    private Character character;


    public event UnityAction OutOfHealth;

    void Start()
    {
        character = GetComponent<Character>();
        _health = StartingHealth;
    }

    public void Take(float damage)
    {
        if (_health > 0 && !_currentlyInvulnerable && !Invincible)
        {
            _health -= damage;
            MakeInvulnerable();
            if (_health <= 0)
            {
                OutOfHealth?.Invoke();
            }
        }
    }

    public void MakeInvulnerable()
    {
        StartCoroutine(PerformInvulnerable());
    }

    private IEnumerator PerformInvulnerable()
    {
        _currentlyInvulnerable = true;
        _invulnerableTime = 0;
        Coroutine flicker = StartCoroutine(SpriteRendererUtil.Flicker(character.SpriteRenderer, DamageTakenFlickerColor, InvulnerableTime));
        while (_invulnerableTime < InvulnerableTime)
        {
            _invulnerableTime += Time.deltaTime;
            yield return null;
        }
        _currentlyInvulnerable = false;
        StopCoroutine(flicker);
    }

    public void MakeInvincible(float duration)
    {
        Invincible = true;
        if (duration > 0)
        {
            StartCoroutine(InvincibleCoroutine(duration));
        }
        else
        {
            //TODO change shader or soemthing.
        }
    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        _invulnerableTime = 0;
        Coroutine flicker = StartCoroutine(SpriteRendererUtil.Flicker(character.SpriteRenderer, InvincibleFlickerColor, duration));
        while (_invulnerableTime < duration)
        {
            _invulnerableTime += Time.deltaTime;
            yield return null;
        }
        Invincible = false;
        StopCoroutine(flicker);
    }

}
