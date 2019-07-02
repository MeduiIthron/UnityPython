using UnityEngine;
using UnityPython;

public class PythonUnityHelloWorld : MonoBehaviour
{
    private void Start()
    {
        var engine = PythonEnvironment.CreateEngine();
        var scope = engine.CreateScope();
        var source = "import UnityEngine\nUnityEngine.Debug.Log('Unity Hello World')";
        engine.IncludeUnityLibraries();
        engine.Execute(source, scope);
    }
}
