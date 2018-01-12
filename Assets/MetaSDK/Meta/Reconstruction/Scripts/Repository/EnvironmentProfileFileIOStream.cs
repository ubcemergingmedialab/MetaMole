using System.IO;
using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Uses a file to reads/writes the environment profiles data.
    /// </summary>
    public class EnvironmentProfileFileIOStream : IEnvironmentProfileIOStream
    {
        private readonly string _path;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentProfileFileIOStream"/> class.
        /// </summary>
        /// <param name="path">Path of the file used to read/write the environment profiles data.</param>
        public EnvironmentProfileFileIOStream(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            _path = path;
        }

        /// <summary>
        /// Reads the environment profiles data from a file.
        /// </summary>
        /// <returns>The environment profiles data</returns>
        public string Read()
        {
            Debug.Assert(!string.IsNullOrEmpty(_path));
            try
            {
                using (StreamReader reader = new StreamReader(_path))
                {
                    string fileContent = reader.ReadToEnd();
                    return fileContent;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Writes a file with environment profiles data.
        /// </summary>
        public void Write(string content)
        {
            Debug.Assert(!string.IsNullOrEmpty(_path));

            // Creates the directory if it doesn't exists.
            FileInfo fileInfo = new FileInfo(_path);
            if (fileInfo.Directory != null)
            {
                fileInfo.Directory.Create();
            }

            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.Write(content);
            }
        }
    }
}
