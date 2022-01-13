using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public PlayerTouchControl playerTouch;
    private Vector2 startPosition;
    private float startTime;
    [SerializeField] private float minDistance = 0.2f;
    [SerializeField] private float maxTime = 1f;
    private Vector2 endPosition;
    private float endTime;
    private void Awake() {
        // PlayerTouchControl playerTouch = GetComponent<PlayerTouchControl>();
        // playerTouch = PlayerTouchControl.instance;
    }

    private void OnEnable() {
        playerTouch.OnStartTouch += SwipeStart;
        playerTouch.OnEndTouch += SwipeEnd; 
    }
    private void OnDisable() {
        playerTouch.OnStartTouch -= SwipeStart;
        playerTouch.OnEndTouch -= SwipeEnd;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DectecdSwipe();
    }


    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void DectecdSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime)<= maxTime){
            // Debug.DrawLine(startPosition, endPosition, Color.red,5f);
            Debug.Log(Vector3.Distance(startPosition, endPosition));
        }
        
    }
}
