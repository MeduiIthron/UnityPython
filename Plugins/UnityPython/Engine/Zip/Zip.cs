using System;
using System.IO;
using System.Text;

using Ionic.Zip;

using UnityEngine;

namespace UnityZlib
{
    public class Zip
    {

        /// <summary>
        /// Decompress zip file to location
        /// </summary>
        /// <param name="zipfilePath">path to source zip file</param>
        /// <param name="location">path to output location</param>
        public static void UnpackZipFile(string zipfilePath, string location)
        {
            Directory.CreateDirectory(location);
            using (ZipFile zip = ZipFile.Read(zipfilePath))
            {
                try
                {
                    var z = zip.GetEnumerator();
                    while (z.MoveNext())
                    {
                        var t = z.Current;
                        if (t == null || t.IsDirectory)
                            continue;

                        var path = location + "/" + t.FileName.Replace("\\", "/");
                        var dir = path.Substring(0, path.LastIndexOf('/'));
                        Directory.CreateDirectory(dir);

                        using (FileStream s = File.Create(path))
                        {
                            t.Extract(s);
                        }
                    }
                }catch(Exception e)
                {
                    
                }
            }
        }
    }
}
