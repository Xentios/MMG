using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPerspective : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;

    public float left = -0.1f;
    public float right = 0.1f;
    public float bottom = -0.1f;
    public float top = 0.1f;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
       

        Matrix4x4 m = Trimetric(
            left, right, bottom, top,
            cam.nearClipPlane, cam.farClipPlane);
        cam.projectionMatrix = m;
    }
    static Matrix4x4 Trimetric(
    float left, float right,
    float bottom, float top,
    float near, float far)
{
    float x = near;
    float y = near;
    float a = (right + left) / (right - left);
    float b = (top + bottom) / (top - bottom);
    float c = -(far + near) / (far - near);
    float d = -2.0f * far * near / (far - near);
    float e = 1.0f;

    Matrix4x4 m = new Matrix4x4();
    m[0, 0] = x; m[0, 1] = 0; m[0, 2] = a; m[0, 3] = 0;
    m[1, 0] = 0; m[1, 1] = y; m[1, 2] = b; m[1, 3] = 0;
    m[2, 0] = 0; m[2, 1] = 0; m[2, 2] = c; m[2, 3] = d;
    m[3, 0] = 0; m[3, 1] = 0; m[3, 2] = 0; m[3, 3] = e;

    return m;
}
}
