using UnityEngine;
using UnityPython;

public class PythonModulesExample : MonoBehaviour
{
    private void Start()
    {
        var engine = PythonEnvironment.CreateEngine();
        var scope = engine.CreateScope();
        var source = "import random\nvalue = random.choice([1, 2, 3, 4, 5, 6, 7])";
        engine.IncludeNativeLibrary();
        engine.IncludeUnityLibraries();
        engine.Execute(source, scope);
        Debug.Log(scope.GetVariable<int>("value"));
    }
}
