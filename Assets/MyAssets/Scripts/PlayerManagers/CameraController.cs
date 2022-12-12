using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables inicialización cámara
    [SerializeField]
    private Transform _player, _playerCamera, _focusPoint;
    [SerializeField]
    private float _cameraHeight = 3;
    #endregion

    #region Variables zoom
    [SerializeField]
    private float _zoom = -15f;
    [SerializeField]
    private float _zoomSpeed = 3f;
    [SerializeField]
    private float _zoomMax = -6.6f, _zoomMin = -25f;
    #endregion

    #region Variables rotación
    [SerializeField]
    private float _camRotX, _camRotY;
    [SerializeField]
    //private float _mouseSensitivity = 2;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Comprobar asignación de objetos
        if (_player == null)
        {
            Debug.Log("Falta asignar el personaje al CameraController.cs");
        }
        if (_playerCamera == null)
        {
            Debug.Log("Falta asignar la cámara al CameraController.cs");
        }
        if (_focusPoint == null)
        {
            Debug.Log("Falta asignar el punto de foco al CameraController.cs");
        }
        #endregion

        #region Asignar parentesco
        _focusPoint.SetParent(_player);
        _playerCamera.SetParent(_focusPoint);
        #endregion

        #region Asignar posición y rotación
        _focusPoint.localPosition = new Vector3(0, _cameraHeight, 0);
        _focusPoint.localRotation = Quaternion.Euler(0, 0, 0);
        _focusPoint.localScale = new Vector3(1, 1, 1);
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        _playerCamera.LookAt(_player);
        _playerCamera.localScale = new Vector3(1, 1, 1);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Zoom
        _zoom += Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        _zoom = Mathf.Clamp(_zoom, _zoomMin, _zoomMax);
        _playerCamera.localPosition = new Vector3(0, 0, _zoom);
        #endregion

        #region Rotación
        if (Input.GetMouseButton(1))
        {
            _camRotX += Input.GetAxis("Mouse X");
            _camRotY += Input.GetAxis("Mouse Y");
            _camRotY = Mathf.Clamp(_camRotY, -20, 70);
            _focusPoint.localRotation = Quaternion.Euler(_camRotY, 0, 0);
            _player.localRotation = Quaternion.Euler(0, _camRotX, 0);
        }
        #endregion
        _playerCamera.LookAt(_player);
    }
}
