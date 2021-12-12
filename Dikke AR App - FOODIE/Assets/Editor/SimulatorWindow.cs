
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SimulatorWindow : EditorWindow     // new window toevoegen in unity
{

    [MenuItem("Tools/Simulator")]       // het pad waar je new window te vinden is
    public static void Init()
    {
        EditorWindow.GetWindow<SimulatorWindow>().Show();       // init window
    }

    private float speed = 2.0f;
    private float xrot = 0.0f;
    private float yrot = 0.0f;

    public SimulatorWindow()
    {
        EditorApplication.playModeStateChanged += this.PlayModeChanged;         // callback toevoegen voor als je PlayMode changes
    }

    void PlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {       // laad de simulator scene als je playmode actief is --> is verhuist naar appmanager
            // EditorSceneManager.LoadSceneInPlayMode("Assets/Editor/Scenes/Simulator.unity", new LoadSceneParameters(LoadSceneMode.Additive));
        }
    }

    private void OnGUI()        // wordt opgeroepen iedere keer dat de muis iets doet in je window
    {
        if (Application.isPlaying)      // check of je de game runned
        {
            EditorGUILayout.BeginHorizontal();
            speed = EditorGUILayout.FloatField("Speed", speed);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            // roep de functies hier onder op
            if (GUILayout.RepeatButton("Forward"))
            {
                MoveForward(speed);
                Repaint();
            }
            if (GUILayout.RepeatButton("Backward"))
            {
                MoveBackward(speed);
                Repaint();
            }
            if (GUILayout.RepeatButton("Left"))
            {
                MoveLeft(speed);
                Repaint();
            }
            if (GUILayout.RepeatButton("Right"))
            {
                MoveRight(speed);
                Repaint();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            xrot = EditorGUILayout.Slider("Rotation Up and Down", xrot, -90.0f, 90.0f);
            yrot = EditorGUILayout.Slider("Rotation Left and Right", yrot, -180.0f, 180.0f);
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Reset"))
            {
                xrot = 0.0f;
                yrot = 0.0f;
            }

            SetRotation(xrot, yrot);
        }
        else
        {
            GUILayout.Label("Unity is not playing");
        }
    }

    // Laad de camera bewegen via een functie in een script dat aan AR camera hangt
    void MoveForward(float speed)
    {
        SimulatorController[] controllers = Object.FindObjectsOfType<SimulatorController>();
        foreach (var controller in controllers)
        {
            controller.MoveForward(speed);
        }
    }

    void MoveBackward(float speed)
    {
        SimulatorController[] controllers = Object.FindObjectsOfType<SimulatorController>();
        foreach (var controller in controllers)
        {
            controller.MoveBackward(speed);
        }
    }

    void MoveLeft(float speed)
    {
        SimulatorController[] controllers = Object.FindObjectsOfType<SimulatorController>();
        foreach (var controller in controllers)
        {
            controller.MoveLeft(speed);
        }
    }

    void MoveRight(float speed)
    {
        SimulatorController[] controllers = Object.FindObjectsOfType<SimulatorController>();
        foreach (var controller in controllers)
        {
            controller.MoveRight(speed);
        }
    }

    void SetRotation(float xrot, float yrot)
    {
        SimulatorController[] controllers = Object.FindObjectsOfType<SimulatorController>();
        foreach (var controller in controllers)
        {
            controller.SetRotation(xrot, yrot);
        }
    }

}
