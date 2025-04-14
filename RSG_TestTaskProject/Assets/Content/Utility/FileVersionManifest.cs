using System;
using System.Collections.Generic;

namespace Utility
{
    [Serializable]
    public class FileVersionManifest : Dictionary<string, FileVersionManifest.ManifestEntry>
    {
        [Serializable]
        public class ManifestEntry
        {
            public string Path;
            public int Version;
            public ManifestEntry(string name, int version)
            {
                Path = name;
                Version = version;
            }
        }
    }
}
