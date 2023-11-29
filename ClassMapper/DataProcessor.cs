using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{
    public static class DataProcessor
    {
        public static IDictionary<int, LambdaExpression> lstDestinationExpression;
        public static IDictionary<int, LambdaExpression> lstSourceExpression;

        public static IDictionary<string, object> lstDestinationClass;
        public static IList<string> lstDestination;
        public static IList<string> lstSource;

        public static int _counter;
    }

    public class DestinationClass
    {
        public object destinationClass { get; set; }

    }
}
