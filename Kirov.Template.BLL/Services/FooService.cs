using DAL.Template.Repositories;
using NAutowired.Core.Attributes;

namespace BLL.Template.Services
{
    [Service]
    public class FooService : Service
    {

        [Autowired]
        private readonly FooRepository fooRepository;

    }
}
