using ClassMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddMapProfile(new ObjectModelMap()));

            Example ex = new Example() { PropertyDriver = "Driver", PropertyPassenger = "Passenger" };

           var test = Mapper.Map<Example, ExampleCopy>(ex);

            var test2 = Mapper.Map<Example, ExampleSimply>(ex);
        }
    }

    public class ObjectModelMap : Profile
    {
        public ObjectModelMap()
        {
            CreateMap<Example, ExampleCopy>()
                .ForMember(des=> des.PropertyMDrv, src => src.PropertyPassenger)
                .ForMember(des => des.PropertyFllr, src => src.PropertyDriver);
            CreateMap<Example, ExampleSimply>();
                

        }
    }
}
