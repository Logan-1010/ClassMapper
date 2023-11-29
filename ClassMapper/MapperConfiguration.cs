using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{
    public class MapperConfigurationExpression : IMapperConfigurationExpression
    {
        public MapperConfigurationExpression() { }

        public void AddMapProfile(Profile profile) { }

    }

    public class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>
    {

        public IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember)
        {
            int test = DataProcessor._counter;

            DataProcessor.lstDestinationExpression[DataProcessor._counter] = destinationMember;
            DataProcessor.lstSourceExpression[DataProcessor._counter] = sourceMember;

            if(DataProcessor.lstDestination.Count == DataProcessor._counter)
                    {
                DataProcessor.lstDestination.Add(typeof(TDestination).Name);
                DataProcessor.lstSource.Add(typeof(TSource).Name);
            }
           

            DataProcessor._counter += 1;

            return new MappingExpression<TSource, TDestination>();
        }


    }
}
