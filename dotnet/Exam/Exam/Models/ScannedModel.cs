using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.Models
{
    public class ScannedModel
    {
        public int ScannedId;
        public int UrlId;
        public string ScannedUrl;
        public string ScannedData;

        public ScannedModel(int id, int urlId, string url, string data)
        {
            ScannedId = id;
            UrlId = urlId;
            ScannedUrl = url;
            ScannedData = data;
        }
    }
}
