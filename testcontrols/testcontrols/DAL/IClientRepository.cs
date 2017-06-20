using System;
using testcontrols.DAL.ResponseModels;

namespace testcontrols.DAL
{
    public interface IClientRepository
    {
        Ticket<AuthorizationResponse> Auth(string login);
    }
}
