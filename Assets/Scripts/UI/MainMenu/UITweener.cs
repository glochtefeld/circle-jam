using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIAnimationType
{
    Move,
    Scale,
    ScaleX,
    ScaleY,
    Fade
}

public class UITweener : MonoBehaviour
{

    #region Serialized fields
    public GameObject objectToAnimate;
    public UIAnimationType animationType;
    public LeanTweenType easeType;
    public float duration;
    public float delay;
    public bool loop;
    public bool pingPong;
    public bool startPositionOffset;
    public Vector3 from;
    public Vector3 to;
    public bool showOnEnable;
    public bool workOnDisable;
    #endregion

    private LTDescr _tweenObject;

    #region Monobehaviour
    public void OnEnable()
    {
        if (showOnEnable)
            Show();
        //LeanTween.pause(_tweenObject.uniqueId);
        //LeanTween.resume(_tweenObject.uniqueId);
    }

    public void OnDisable()
    {
        Disable();
    }
    #endregion

    public void Show()
    {
        objectToAnimate.SetActive(true);
        HandleTween();
    }

    private void HandleTween()
    {
        if (objectToAnimate == null)
            objectToAnimate = gameObject;

        switch (animationType)
        {
            case UIAnimationType.Fade:
                Fade();
                break;
            case UIAnimationType.Move:
                MoveAbsolute();
                break;
            case UIAnimationType.Scale:
                Scale();
                break;
            case UIAnimationType.ScaleX:
                ScaleX();
                break;
            case UIAnimationType.ScaleY:
                ScaleY();
                break;
            default:
                Debug.Log($"Unknown animation type");
                break;
        }

        _tweenObject.setDelay(delay);
        _tweenObject.setEase(easeType);

        if (loop)
            _tweenObject.loopCount = int.MaxValue;

        if (pingPong)
            _tweenObject.setLoopPingPong();
    }

    private void Fade()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        if (startPositionOffset)
            objectToAnimate.GetComponent<CanvasGroup>().alpha = from.x;
        _tweenObject = LeanTween.alphaCanvas(
            objectToAnimate.GetComponent<CanvasGroup>(),
            to.x,
            duration);
    }
    
    private void MoveAbsolute()
    {
        objectToAnimate.GetComponent<RectTransform>().anchoredPosition = from;
        _tweenObject = LeanTween.move(
            objectToAnimate.GetComponent<RectTransform>(),
            to,
            duration);
    }

    private void Scale()
    {
        if (startPositionOffset)
            objectToAnimate.GetComponent<RectTransform>().localScale = from;
        _tweenObject = LeanTween.scale(objectToAnimate, to, duration);
    }

    private void SwapDirection()
    {
        var temp = from;
        from = to;
        to = temp;
    }

    public void Disable()
    {
        //SwapDirection();
        //HandleTween();
        //LeanTween.pause(_tweenObject.uniqueId);
        //LeanTween.resume(_tweenObject.uniqueId);
        //_tweenObject.setOnComplete(() =>
        //{
        //    SwapDirection();
        //    gameObject.SetActive(false);

        //});
    }

    public void Disable(Action onCompleteAction)
    {
        SwapDirection();
        HandleTween();
        _tweenObject.setOnComplete(onCompleteAction);
    }

    private void ScaleX()
    {
        throw new NotImplementedException();
    }

    private void ScaleY()
    {
        throw new NotImplementedException();
    }

}