using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TutorialStep : MonoBehaviour
{
    [Header("Contenido del Cartel")]
    public string titulo;
    [TextArea(2, 5)]
    public string descripcion;

    [Header("Configuración de Avance")]
    public TutorialAdvanceType advanceType = TutorialAdvanceType.TapAnywhere;

    [Header("Eventos Opcionales")]
    [Tooltip("Se ejecuta al iniciar este paso (ej: activar una flecha, iluminar un botón)")]
    public UnityEvent onStepStart;

    [Tooltip("Se ejecuta al terminar este paso (ej: ocultar la flecha)")]
    public UnityEvent onStepEnd;
}

public enum TutorialAdvanceType
{
    TapAnywhere,    // Avanza tocando cualquier parte de la pantalla
    CustomAction    // Espera a que el jugador toque un botón o haga una acción específica
}