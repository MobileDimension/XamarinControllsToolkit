using System;
using System.Threading.Tasks;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public class ClientRepository : IClientRepository
    {
        public ClientRepository()
        {
        }

        public Ticket<AuthorizationResponse> Auth(string login)
        {
            var response = new Response<AuthorizationResponse>();
            response.Data = new AuthorizationResponse() { IsAuthorized = login == "test123" };
            var ticket = new Ticket<AuthorizationResponse>();
            ticket.Response = response;
            ticket.DoJob();
            return ticket;
        }
    }
}
