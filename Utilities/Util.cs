using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utilities
{
    public class Util
    {
        public static List<string> FlattenMatch(Match m)
        {
            List<string> parts = new List<string>();
            foreach (Group group in m.Groups)
            {
                foreach (Capture capture in group.Captures)
                {
                    var part = capture.Value.Trim();
                    if (part != string.Empty)
                    {
                        parts.Add(part);
                    }
                }
            }
            parts.RemoveAt(0);
            return parts;
        }
    }
}
