using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSettings : MonoBehaviour
{
    [Tooltip("Time of the day in seconds based on the game time")]
    [SerializeField] private float _timeInGameDay;

    private TimeSpan computerTime;

    [Tooltip("Time of the day in minutes based on system time")]
    [SerializeField] private float debugComputerTime;

    [Tooltip("Changes suns rotation and world time based on the system time")]
    public bool _useComputerTime;

    [Tooltip("Changes suns rotation and world time based on the game time")]
    public bool _useGameTime = true;

    // rotations of sun in degrees
    [SerializeField] private float _sunRotation;

    private bool _nightTime;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        _timeInGameDay = -120f;
    }

    // Update is called once per frame
    void Update()
    {
        computerTime = DateTime.Now.TimeOfDay;
        debugComputerTime = (float)computerTime.TotalMinutes;

        if (_useComputerTime)
        {
            _useGameTime = false;
        }
        if (_useGameTime)
        {
            _useComputerTime = false;
        }

        if(_useComputerTime && _sunRotation < 90f && _sunRotation > 270f)
        {
            _nightTime = true;
        }
        else
        {
            _nightTime = false;
        }

        if(_useGameTime && _sunRotation > 180f)
        {
            _nightTime = true;
        }
        else
        {
            _nightTime = false;
        }

        if (_useGameTime)
        {

            // adds time until a full day has occured
            if (_timeInGameDay < 1440f)
            {
                _timeInGameDay -= Time.deltaTime;
            }
            else
            {
                _timeInGameDay = 0f;
            }

            // convert the time into degrees
            _sunRotation = _timeInGameDay / 4f;
        }

        if (_useComputerTime)
        {
            _sunRotation = ((float)computerTime.TotalMinutes - 360) / -4f;
            _timeInGameDay = -120f;
        }

        this.gameObject.transform.localRotation = Quaternion.Euler(0f, _sunRotation, 0f);
    }
}
