using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slowable : MonoBehaviour
{
    Character character;
    bool isSlowed;
    float currentScale;

    Vector2 velocityJustAfterSlowed;


    void Start()
    {
        character = gameObject.FindComponent<Character>();
        CharacterSlowTimeSurround characterSlowTimeSurroundAbility = Characters.Player?.FindAbility<CharacterSlowTimeSurround>();
        if (characterSlowTimeSurroundAbility)
            characterSlowTimeSurroundAbility.TimeScaleChanged += OnTimeScaleChanged;

    }

    void Update()
    {
        if (isSlowed)
        {
            character.Controller.SetVelocity(velocityJustAfterSlowed * currentScale);
        }
    }

    public void Slow(float scale)
    {
        this.DOComplete();
        if (character != null && !isSlowed)
        {
            isSlowed = true;
            ChangeTimeScale(scale);
            character.Controller.SlowScale = scale;
            velocityJustAfterSlowed = character.Controller.Body2D.velocity;
        }
    }

    public void Normalize(float duration)
    {

        DOTween.To(x => currentScale = x, currentScale, 1, duration).SetId(this);
        Tweener tween = DOTween.To(x => character.Model.GetComponent<Animator>().speed = x, character.Model.GetComponent<Animator>().speed, 1, duration).SetId(this);
        DOTween.To(() => character.Controller.Body2D.velocity, x => character.Controller.Body2D.velocity = x, velocityJustAfterSlowed, 2).SetId(this);
        tween.onComplete
        += () =>
        {
            isSlowed = false;
            character.Controller.SlowScale = 1;
        };

    }

    private void OnTimeScaleChanged(float scale)
    {
        ChangeTimeScale(scale);
    }

    private void ChangeTimeScale(float scale)
    {
        currentScale = scale;
        character.Model.GetComponent<Animator>().speed = scale;
    }

    public bool IsSlowed { get => isSlowed; }
    public float CurrentScale { get => currentScale; set => currentScale = value; }
}
