using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{

    public interface IMapperConfigurationExpression
    {

        void AddMapProfile(Profile profile);
    }

    public interface IMappingExpression<TSource, TDestination>
    {
        IMappingExpression<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> destinationMember, Expression<Func<TSource, TMember>> sourceMember);

    }

}
