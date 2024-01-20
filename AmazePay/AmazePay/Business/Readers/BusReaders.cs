using AmazePay.Repository;
using System.Data;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace AmazePay.Business.Readers
{
    public class BusReaders
    {
        DataAccessLayerSQL DAL = new DataAccessLayerSQL();
        public string StrGetFileTypeValue = "GetFiletype";

        public string GetDalFileTypeValue()
        {
            DataSet ds = DAL.ExecuteProcedure(StrGetFileTypeValue);
            string jsonResult = ConvertDataSetToJson(ds);
            //Dictionary<string, string> dictionaryValue = GetDictionaryValue(ds);
            //string datas = ds;
            //return Convert.ToString(ds.Tables[0].Rows);//JsonConvert.SerializeObject(dataSet, Formatting.Indented);

            //Dictionary<string, string> keyValueData = new Dictionary<string, string>();

            //// Iterate through the rows of the dataset
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    // Assuming the first column contains keys and the second column contains values
            //    string key = row[0].ToString();
            //    string value = row[1].ToString();

            //    // Add key-value pair to the dictionary
            //    keyValueData[key] = value;
            //}

            return jsonResult;
        }
        public Dictionary<string, string> GetDictionaryValue(DataSet ds)
        {
            Dictionary<string, string> keyValueData = new Dictionary<string, string>();
            // Create and return your dictionary
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                // Assuming the first column contains keys and the second column contains values
                string key = row[0].ToString();
                string value = row[1].ToString();

                // Add key-value pair to the dictionary
                keyValueData[key] = value;
            }

            return keyValueData;
        }

        private string ConvertDataSetToJson(DataSet dataSet)
        {
            // Check for null or empty DataSet
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                return "{}"; // Return an empty JSON object
            }

            // Convert the DataSet to JSON using Newtonsoft.Json
            return JsonConvert.SerializeObject(dataSet, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
