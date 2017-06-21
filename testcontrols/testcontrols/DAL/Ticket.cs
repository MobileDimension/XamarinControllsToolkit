using System;
using System.Threading.Tasks;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public class Ticket<T> where T: IResponseBody 
    {
        public Response<T> Response;
        public event Action<Response<T>> OnSuccess;
        public event Action<Response<T>> OnFailure;

        public async Task DoJob()
        {
            var rnd = new Random();
            await Task.Delay(rnd.Next(200, 2000));
            OnSuccess?.Invoke(Response);
        }
    }
}
