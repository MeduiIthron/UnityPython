using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using UnityEngine;

using UnityZlib;

namespace UnityPython
{
    public static class PythonEnvironment
    {
        /// <summary>
        /// Python engine initialization
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ScriptEngine CreateEngine(IDictionary<string, object> options = null)
        {
            var engine = Python.CreateEngine(options);
            engine.IncludeNativeLibrary();

            var infoStream = new MemoryStream();
            var infoWriter = new PythonPrinter(Debug.Log, infoStream);
            engine.Runtime.IO.SetOutput(infoStream, infoWriter);

            var errorStream = new MemoryStream();
            var errorWriter = new PythonPrinter(Debug.LogError, errorStream);
            engine.Runtime.IO.SetErrorOutput(errorStream, errorWriter);

            return engine;
        }

        /// <summary>
        /// Including internal namespace to python engine
        /// </summary>
        /// <param name="identifier">Namespace Identifier</param>
        public static void IncludeNamespace(this ScriptEngine engine, string identifier)
        {
            foreach (var assembly in GetAssembliesInNamespace(identifier))
            {
                engine.Runtime.LoadAssembly(assembly);
            }
        }

        /// <summary>
        /// Including native python libraries path
        /// </summary>
        /// <param name="path">Path to python files</param>
        public static void IncludeLibraryPath(this ScriptEngine engine, string path)
        {
            var paths = engine.GetSearchPaths();
            paths.Add(path);
            engine.SetSearchPaths(paths);
        }

        /// <summary>
        /// Including standart unity libraries :: UnityEngine and UnityEditor
        /// </summary>
        public static void IncludeUnityLibraries(this ScriptEngine engine)
        {
            engine.IncludeNamespace("UnityEngine");
        #if UNITY_EDITOR
            engine.IncludeNamespace("UnityEditor");
        #endif
        }

        public static void IncludeNativeLibrary(this ScriptEngine engine)
        {
            var library = Resources.Load("PythonStandartLibrary") as TextAsset;
            var path = Application.persistentDataPath + "/scripts/python-stdlib";
            Directory.CreateDirectory(path);
            Directory.CreateDirectory(path + "/files/Lib");
            File.WriteAllBytes(path + "/python-raw.bin", library.bytes);
            Zip.UnpackZipFile(path + "/python-raw.bin", path + "/files");
            engine.IncludeLibraryPath(path + "/files/Lib");
        }

        /// <summary>
        /// Getting assembly list from namespace
        /// </summary>
        /// <param name="identifier">Namespace Identifier</param>
        /// <returns>Collection of Assemly</returns>
        private static IEnumerable<Assembly> GetAssembliesInNamespace(string identifier)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.Namespace != null && t.Namespace.StartsWith(identifier))
                .Select(t => t.Assembly)
                .Distinct();
        }
    }
}
