using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public interface ILoginBusiness
    {
        object FindByLogin(User user);
    }
}
