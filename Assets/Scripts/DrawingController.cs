using System.Collections.Generic;
using UnityEngine;

public class DrawingController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float linePointSpacing = 0.1f;
    public float launchForce = 5f; // Ajusta la fuerza del lanzamiento según sea necesario

    private List<Vector2> drawingPoints = new List<Vector2>();

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            drawingPoints.Add(mousePosition);
        }

        // Visualizar la trayectoria (puedes omitir esto si no quieres visualizarla)
        UpdateLineRenderer();

        // Verificar si se soltó el clic izquierdo
        if (Input.GetMouseButtonUp(0) && drawingPoints.Count > 1)
        {
            LaunchPaloma();
            drawingPoints.Clear(); // Limpiar la lista después de lanzar la paloma
        }
    }

    void UpdateLineRenderer()
    {
        // Visualizar la trayectoria utilizando LineRenderer
        lineRenderer.positionCount = drawingPoints.Count;

        for (int i = 0; i < drawingPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(drawingPoints[i].x, drawingPoints[i].y, 0));
        }
    }

    void LaunchPaloma()
    {
        // Obtener la dirección y la fuerza del lanzamiento
        Vector2 launchDirection = (drawingPoints[drawingPoints.Count - 1] - drawingPoints[0]).normalized;
        Vector2 launchForceVector = launchDirection * launchForce;

        // Aplicar la fuerza a la paloma (puedes ajustar esto según tu configuración)
        Rigidbody2D palomaRigidbody = GetComponent<Rigidbody2D>();
        palomaRigidbody.velocity = Vector2.zero; // Restablecer la velocidad para evitar acumulación
        palomaRigidbody.AddForce(launchForceVector, ForceMode2D.Impulse);
    }
}
