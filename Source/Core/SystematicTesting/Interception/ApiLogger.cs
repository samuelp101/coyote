﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Microsoft.Coyote.SystematicTesting.Interception
{
    /// <summary>
    /// Logs invocation of APIs during testing.
    /// </summary>
    /// <remarks>This type is intended for compiler use rather than use directly in code.</remarks>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ApiLogger
    {
        /// <summary>
        /// Info about the latest test executing.
        /// </summary>
        private static Info LatestTestInfo;

        /// <summary>
        /// Logs that the specified test started executing.
        /// </summary>
        public static void LogTestStarted(string name)
        {
            var info = new Info(name);
            info.Save();
            LatestTestInfo = info;
        }

        /// <summary>
        /// Logs that the specified API was invoked.
        /// </summary>
        public static void LogInvocation(string name)
        {
            LatestTestInfo.LogInvocation(name);
        }

        /// <summary>
        /// Information about API invocations that can be serialized to an XML file.
        /// </summary>
        public class Info
        {
            private static readonly object SyncObject = new object();

            /// <summary>
            /// The name of the test.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The location of the test.
            /// </summary>
            public string Location { get; set; }

            /// <summary>
            /// Map from APIs to their invocation frequency. This is a proxy
            /// used for XML serialization.
            /// </summary>
            [XmlElement("APIs")]
            public List<KeyValuePair<string, int>> APIsProxy
            {
                get
                {
                    return new List<KeyValuePair<string, int>>(this.APIs);
                }

                set
                {
                    this.APIs = new SortedDictionary<string, int>();
                    foreach (var pair in value)
                    {
                        this.APIs[pair.Key] = pair.Value;
                    }
                }
            }

            /// <summary>
            /// Map from APIs to their invocation frequency.
            /// </summary>
            [XmlIgnore]
            public IDictionary<string, int> APIs { get; set; }

            /// <summary>
            /// Path to the serialized file.
            /// </summary>
            private readonly string FilePath;

            /// <summary>
            /// Initializes a new instance of the <see cref="Info"/> class.
            /// </summary>
            public Info()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Info"/> class.
            /// </summary>
            internal Info(string name)
            {
                this.Name = name;
                this.Location = this.GetLocation();
                this.FilePath = this.GetFilePath(this.Location, name);
                this.APIs = new SortedDictionary<string, int>();
            }

            internal void LogInvocation(string name)
            {
                if (!this.APIs.ContainsKey(name))
                {
                    this.APIs.Add(name, 0);
                }

                this.APIs[name]++;
                this.Save();
            }

            /// <summary>
            /// Serializes to an XML file.
            /// </summary>
            internal void Save()
            {
                lock (SyncObject)
                {
                    using FileStream fs = new FileStream(this.FilePath, FileMode.OpenOrCreate, FileAccess.Write);
                    var serializer = new XmlSerializer(typeof(Info));
                    serializer.Serialize(fs, this);
                }
            }

            private string GetLocation() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            private string GetFilePath(string location, string name) => Path.Combine(location, $"{name}.api.xml");
        }
    }
}
