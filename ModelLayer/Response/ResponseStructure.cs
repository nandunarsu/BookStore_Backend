using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

using System.Threading.Tasks;

namespace ModelLayer.Response
{
    public class ResponseStructure<T>
    {
        //private Book result;

        [JsonIgnore]
        public int StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ResponseStructure(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public ResponseStructure(bool success, string message, T? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public ResponseStructure(string message)
        {
            Message = message;
        }
    }
}
