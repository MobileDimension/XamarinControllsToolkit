using System;
using System.Collections.Generic;

namespace testcontrols.DAL.ResponseModels
{
    public class Response : IResponse    
    {
        public IEnumerable<IError> Errors { get; set; }
        public string ResponseCode { get; set; }
        IResponseBody Data { get; set; }
    }
}
