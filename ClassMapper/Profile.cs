using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{
    public class Profile
    {
        public IMappingExpression<Ts, Td> CreateMap<Ts, Td>()
        {
            Type _tS = typeof(Ts);
            Type _tD = typeof(Td);

            if (!DataProcessor.lstDestinationClass.ContainsKey(_tD.Name))
            {

                //ConstructorInfo constructorInfoObj = myType.GetConstructor(types);
                /*
                var ctor = _tD
                    .GetConstructors(
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Instance
                    )
                    .First();

                var parameters = _tD
                    .GetConstructors()
                    .Single()
                    .GetParameters()
                    .Select(p => (object)null)
                    .ToArray();

                ParameterInfo[] _param = ctor.GetParameters();

                object[] paramArr = new object[0];

                int iCount = 0;
                foreach(var item in _param)
                {
                    Array.Resize(ref paramArr, iCount + 1);
                    paramArr[iCount] = item.RawDefaultValue;
                    iCount++;
                }
                  
                var instance = (Td)ctor.Invoke(parameters);
                DataProcessor.lstDestinationClass.Add(_tD.Name, instance);

                */
                DataProcessor.lstDestinationClass.Add(_tD.Name, MyCreateInstance(_tD));


                //DataProcessor.lstDestinationClass.Add(_tD.Name, Activator.CreateInstance(_tD, parameters));
            }

            //for (int i = DataProcessor.lstDestinationExpression.Count; i <= DataProcessor._counter; i++)
            //{
            //    DataProcessor.lstDestinationExpression.Add(i, null);
            //    DataProcessor.lstSourceExpression.Add(i, null); 
            //}

            DataProcessor.lstDestinationExpression.Add(DataProcessor.lstDestinationExpression.Count, null);
            DataProcessor.lstSourceExpression.Add(DataProcessor.lstSourceExpression.Count, null); 

            DataProcessor.lstDestination.Add(_tD.Name);
            DataProcessor.lstSource.Add(_tS.Name);

            return new MappingExpression<Ts, Td>();
        }

        public static object MyCreateInstance(Type type)
        {
            var parametrizedCtor = type
                .GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Length > 0);

            return parametrizedCtor != null
                ? parametrizedCtor.Invoke
                    (parametrizedCtor.GetParameters()
                        .Select(p =>
                            p.HasDefaultValue ? p.DefaultValue :
                            p.ParameterType.IsValueType && Nullable.GetUnderlyingType(p.ParameterType) == null
                                ? Activator.CreateInstance(p.ParameterType)
                                : null
                        ).ToArray()
                    )
                : Activator.CreateInstance(type);
        }
    }
}
