﻿using System;
using Events;
using UnityEngine;

namespace Characters
{
    public class Horde : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.13f;

        private Timer _timer      = new Timer(2f);
        private bool  _runStarted = false;

        [SerializeField] private PlayerController _player;


        #region Private fields

        [SerializeField] private Vector2 _direction = Vector2.right;

        #endregion

        #region Props

        public Transform Transform { get; private set; }

        #endregion

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == _player.gameObject.layer) Health.Kill();
        }

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            _player   = FindObjectOfType<PlayerController>();
        }

        private void Hui(Room r)
        {
            Transform.position = new Vector3(Transform.position.x, r.transform.position.y, Transform.position.z);
        }
        

        private void OnEnable()
        {
            _timer.ResetEvent += Run;
            Room.PlayerEnterInRoom += Hui;
            _timer.Set();
        }

        private void OnDisable()
        {
            Room.PlayerEnterInRoom -= Hui;
        }

        private void FixedUpdate()
        {
            if (_runStarted == false) return;

            transform.position += Vector3.right * _speed;
        }

        private void Run()
        {
            _runStarted = true;
        }
    }
}