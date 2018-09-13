using System;
using System.Collections.Generic;

namespace FingerprintRecognition.models
{
    [Serializable]
    public class UserFingerprint
    {
        public string name;
        public List<Minutiae> minutiaes;

        public UserFingerprint(string name, List<Minutiae> minutiaes)
        {
            this.name = name;
            this.minutiaes = minutiaes;
        }
    }
}