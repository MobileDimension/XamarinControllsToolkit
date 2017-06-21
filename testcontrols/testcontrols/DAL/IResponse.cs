using System;
using System.Collections.Generic;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public interface IResponse
    {
        string ResponseCode { get; set; }    
    }

    public class Response<T> : IResponse where T: IResponseBody
    {
        public string ResponseCode { get; set; }
        public T Data { get; set; }
        public IEnumerable<IError> Errors { get; set; }
    }

    public interface IError
    {
        string Code { get; set; }
        string Message { get; set; }
    }

    public class Error : IError
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
