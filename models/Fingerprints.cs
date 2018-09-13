using System;
using System.Collections.Generic;

namespace FingerprintRecognition.models
{
    [Serializable]
    public class Fingerprints
    {
        public List<UserFingerprint> userFingerprints;

        public Fingerprints(List<UserFingerprint> userFingerprints)
        {
            this.userFingerprints = userFingerprints;
        }
    }
}