using CalcTime.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CalcTime.Utility
{
    public class DeserializeXml
    {
        private string root = Application.StartupPath;
        private XmlSerializer serializer;
        private FileStream fs;

        private DataContractJsonSerializer dcjs;

        // Deserialize XML
        public Timeline DeserializeObject()
        {
            string filename = Path.Combine(root, @"Data\Timeline.xml");
            serializer = new XmlSerializer(typeof(Timeline));

            fs = new FileStream(filename, FileMode.Open);

            Timeline timeline = (Timeline)serializer.Deserialize(fs);
            fs.Close();

            return timeline;
        }

        // Deserialize JSON
        public List<Format.JsonData> DeserializeJson()
        {
            string filename = Path.Combine(root, @"Data\DataFormat.txt");

            string contents = File.ReadAllText(filename);
            JObject o = JObject.Parse(contents);

            IList<JToken> results = o["Format"]["FormatList"].Children().ToList();

            List<Format.JsonData> jsonDatas = new List<Format.JsonData>();

            foreach (var result in results)
            {
                JObject o1 = JObject.Parse(result.ToString());
                Format.JsonData jsonData = JsonConvert.DeserializeObject<Format.JsonData>(result.ToString());
                jsonDatas.Add(jsonData);
            }

            return jsonDatas;
        }
    }
}
