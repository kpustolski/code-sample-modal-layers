using System.Collections.Generic;
using System;

namespace CodeSampleModalLayer
{
    // Made a class for copy related Key Value pairs to easily serialize to a JSON file.
    [Serializable]
    public class Copy
    {
        public string copyKey;
        public string copyValue;

        public override string ToString()
        {
            return $"copyKey: {copyKey} copyValue: {copyValue}";
        }
    }

    // This class helps store the deserialized JSON data.
    [Serializable]
    public class CopyData
    {
        public List<Copy> data = new List<Copy>();

        public override string ToString()
        {
            foreach (var d in data)
            {
                return $"{d.ToString()}";
            }
            return "";
        }
    }
}