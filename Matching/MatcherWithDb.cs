using System;
using System.Collections.Generic;
using FingerprintRecognition.models;
using FingerprintRecognition.DAL;

namespace FingerprintRecognition.Matching
{
    public class MatcherWithDb
    {
        private Fingerprints fingerprints;
        
        public MatcherWithDb(List<Minutiae> originalMinutiaes, string dbFileName)
        {
            DbJSONSerializer serializer = new DbJSONSerializer();
            fingerprints = serializer.DeserializeMinutiaes(serializer.ReadFromJsonFile("db_fingerprints.json"));
            List<UserFingerprint> userFingerprints = fingerprints.userFingerprints;
            
            foreach(UserFingerprint fp in userFingerprints)
            {
                if (CompareFingerprints(originalMinutiaes, fp.minutiaes))
                {
                    Console.WriteLine("Znaleziono odcisk w bazie");
                    break;
                }
            }
        }

        private bool CompareFingerprints(List<Minutiae> m1, List<Minutiae> m2)
        {
            return false;
        }
    }
}