using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FingerprintRecognition.models;

namespace FingerprintRecognition.DAL
{
    public class DbJSONSerializer : IDbSerializable
    {
        public string SerializeMinutiaes(Fingerprints fingerprints)
        {
            return Serialize(fingerprints); 
        }

        public Fingerprints DeserializeMinutiaes(string json)
        {
            return Deserialize(json);
        }

        private string Serialize(Fingerprints fingerprints)
        {
            return JsonConvert.SerializeObject(fingerprints);
        }

        private Fingerprints Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Fingerprints>(json);
        }

        public string ReadFromJsonFile(string path)
        {
            string json = String.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        public void WriteToFile(string json, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(json);
            }
        }
    }
}