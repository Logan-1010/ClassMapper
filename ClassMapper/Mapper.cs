using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassMapper
{
    // A mapper class to compare two classes and shallow copy
    public static class Mapper
    {
        public static void Initialize(Action<IMapperConfigurationExpression> config)
        {
            DataProcessor.lstDestinationExpression = new Dictionary<int, LambdaExpression>();
            DataProcessor.lstSourceExpression = new Dictionary<int, LambdaExpression>();
            DataProcessor.lstDestinationClass = new Dictionary<string, object>();
            DataProcessor.lstDestination = new List<string>();
            DataProcessor.lstSource = new List<string>();

            DataProcessor._counter = 0;

            config?.Invoke(new MapperConfigurationExpression());

        }

        //Maps Source to Destination 
        public static Td Map<Ts, Td>(Ts srcClass)
        {
            PropertyInfo[] propInfosSrc, propInfosDes;

            //Get Ts & Td
            Type _tS = typeof(Ts);
            Type _tD = typeof(Td);

            Td _destinatonClass =new Func<Td>(() => { return (Td)DataProcessor.lstDestinationClass[_tD.Name]; })();
                        
            propInfosSrc = _tS.GetProperties(BindingFlags.Public | BindingFlags.Instance);            
            propInfosDes = _tD.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int iCount = 0;
            foreach(var item in DataProcessor.lstDestination)
            {
                if(item == _tD.Name && DataProcessor.lstSource[iCount] == _tS.Name)
                {
                    var _srcExp = DataProcessor.lstSourceExpression[iCount];
                    var _desExp = DataProcessor.lstDestinationExpression[iCount];

                    if(_srcExp == null && _desExp == null)
                    {
                        if(_tS.Name == _tD.Name)
                        {
                            //If the names match -> clone the class
                            CloneClass(ref srcClass, ref _destinatonClass);
                        }
                        else
                        {
                            //Set the property value
                            GetAllProperties<Ts, Td>(propInfosSrc, propInfosDes, ref srcClass, ref _destinatonClass);
                        }
                                                                   
                        
                        return _destinatonClass;
                    }

                    var _srcPropName = _srcExp.Body.ToString().Split('.');
                    var _desPropName = _desExp.Body.ToString().Split('.');
                    //Get Source Property
                    var sourceProp = propInfosSrc.Where(p => p.Name == _srcPropName[1]).Select(p => p).First();
                    //Get Destination Property
                    var desProp = propInfosDes.Where(p => p.Name == _desPropName[1]).Select(p => p).First();

                    desProp.SetValue(_destinatonClass, sourceProp.GetValue(srcClass));
                    


                }
                iCount++;
            }

           
            return _destinatonClass;

        }

        //Prefer Clone to Copy
        static void CloneClass<Ts, Td>(ref Ts srcClass, ref Td desClass)
        {
            desClass = (Td)(object)srcClass;

        }

        static void GetAllProperties<Ts, Td>(PropertyInfo[] srcProp, PropertyInfo[] desProp,ref Ts srcClass,ref Td desClass)
        {
            
            foreach (var item in desProp)
            {

                try
                {
                    var _sProp = srcProp.Where(p => p.Name == item.Name).Select(p => p).First();
                    var value = _sProp.GetValue(srcClass);
                                       
                    item.SetValue(desClass, value, null);

                }
                catch (Exception)
                {

                }
            }
            
        }

        //Deep copy
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }




    
  


}
