using System.Collections.Generic;
using FingerprintRecognition.models;

namespace FingerprintRecognition.DAL
{
    public interface IDbSerializable
    {
        string SerializeMinutiaes(Fingerprints fingerprints);
        Fingerprints DeserializeMinutiaes(string json);
    }
}



