using Microsoft.Scripting.Hosting;

using UnityEngine;
using UnityPython;

public class PythonBehaviourFPSExample : MonoBehaviour
{
    public TextAsset ScriptSource;

    public string FunctionInitialize = null;
    public string FunctionAwake = null;
    public string FunctionStart = null;
    public string FunctionUpdate = null;

    private ScriptEngine engine;
    private ScriptScope scope;

    private delegate void function(dynamic value = null);
    [HideInInspector] public CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        engine = PythonEnvironment.CreateEngine();
        scope = engine.CreateScope();
        engine.IncludeNativeLibrary();
        engine.IncludeUnityLibraries();
        var script = engine.CreateScriptSourceFromString(ScriptSource.text);
        var binary = script.Compile();
        binary.Execute(scope);
        scope.GetVariable<function>(FunctionInitialize !=null ? FunctionInitialize: "OnInitialize")?.Invoke(this);
        scope.GetVariable<function>(FunctionAwake !=null ? FunctionAwake : "OnAwake")?.Invoke(this);
    }

    private void Start()
    {
        scope.GetVariable<function>(FunctionStart !=null ? FunctionStart : "OnStart")?.Invoke(this);
    }

    private void Update()
    {
        scope.GetVariable<function>(FunctionUpdate != null ? FunctionUpdate : "OnUpdate")?.Invoke(this);
    }
}
