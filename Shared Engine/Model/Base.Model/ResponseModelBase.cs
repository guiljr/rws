using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Engine.Model
{
    public class ResponseModelBase<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ErrorResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class DetailedResponseModel<T> : ResponseModelBase<T> where T : class
    {
        public IEnumerable<ModelError> Errors { get; set; }
    }
}
