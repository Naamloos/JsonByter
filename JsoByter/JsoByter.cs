using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsoByter
{
    public class JsoByteFile
    {
        [JsonProperty("jsobyte")]
        public JsoByte JsoByte;

        [JsonProperty("file_info")]
        public FileInfo FileInfo;

        [JsonProperty("file_data")]
        public FileData FileData;

        public string GetJson()
        {
            return JObject.FromObject(this).ToString();
        }

        public byte[] GetBytes()
        {
            List<byte> bytes = new List<byte>();
            foreach (JToken j in FileData.Data)
            {
                if (j.Type == JTokenType.String)
                {
                    // token is string
                    foreach (byte b in FileData.Trends[int.Parse(j.ToString())])
                    {
                        bytes.Add(b);
                    }
                }
                else
                {
                    bytes.Add(byte.Parse(j.ToString()));
                }
            }
            return bytes.ToArray();
        }

        public static JsoByteFile FromJson(string json) => JObject.Parse(json).ToObject<JsoByteFile>();

        public static JsoByteFile Generate(string filename, string extension, byte[] bytes)
        {
            Dictionary<byte, int> PossibleTrends = new Dictionary<byte, int>();
            List<byte> Trends = new List<byte>();
            List<JToken> Data = new List<JToken>();
            foreach(byte b in bytes)
            {
                if (PossibleTrends.ContainsKey(b))
                {
                    PossibleTrends[b]++;
                }else
                {
                    PossibleTrends.Add(b, 1);
                }
            }
            foreach (byte b in bytes)
            {
                if (PossibleTrends[b] > 1)
                {
                    if (!Trends.Contains(b))
                    {
                        Trends.Add(b);
                        Data.Add(Trends.IndexOf(b).ToString());
                    }
                    else
                    {
                        Data.Add(Trends.IndexOf(b).ToString());
                    }
                }
                else
                {
                    Data.Add(b);
                }
            }

            List<List<byte>> Ttrends = new List<List<byte>>();
            foreach (byte b in Trends)
                Ttrends.Add(new List<byte>() { b });

            return new JsoByteFile()
            {
                JsoByte = new JsoByte()
                {
                    Version = ""
                },
                FileInfo = new FileInfo()
                {
                    FileName = filename,
                    FileExtension = extension,
                    MimeType = ""
                },
                FileData = new FileData()
                {
                    Data = Data,
                    Trends = Ttrends
                }
            };
        }
    }

    public class JsoByte
    {
        [JsonProperty("version")]
        public string Version;
    }

    public class FileInfo
    {
        [JsonProperty("file_name")]
        public string FileName;
        [JsonProperty("file_ext")]
        public string FileExtension;
        [JsonProperty("mime_type")]
        public string MimeType;
        // no support for extra u skrub
    }

    public class FileData
    {
        [JsonProperty("trends")]
        public List<List<byte>> Trends;
        [JsonProperty("data")]
        public List<JToken> Data;
    }


}
