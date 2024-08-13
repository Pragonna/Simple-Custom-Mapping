<h4 align="center">Simple Mapping</h4> 
<h5>
  <ul> * TResult and TSource is a class - object </ul>
</h5>
<h6> 
1. First create instance from TResult - for return object value </br>
2. We need all properties in TResult and TSource
          
            public static TResult Map<TSource,TResult>(TSource source, out string information)
  
  
            var instanceResult = Activator.CreateInstance(typeof(TResult));
            var resultProperties = instanceResult.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();
</h6>
<h6>
3. Find all properties </br>
4. Compare property types by name </br>
5. Same prop.Name and prop.Type / Set value instance TResult for return

            foreach (var sourceProp in sourceProperties)
            {
                var resultProp = resultProperties.FirstOrDefault(p => p.Name == sourceProp.Name);

                if (resultProp != null)
                {
                    if (resultProp.PropertyType.Name == sourceProp.PropertyType.Name)
                    {
                        resultProp.SetValue(instanceResult, sourceProp.GetValue(source));
                        information += $"\nProperty name: {resultProp.Name} - is successfully mapped";
                    }
                    else
                    {
                        information += $"\n*Property name: {sourceProp.Name} - can not mapping. Because Source property type is {sourceProp.PropertyType.Name} and is not equal type of {resultProp.PropertyType.Name} * ";

                        throw new Exception(information.Split('*')[1]);
                    }
                }
                else
                    information += $"\nProperty name: {sourceProp.Name} - can not mapping to this  -{sourceProp.PropertyType.Name}-  type property. Because this property not found in your result type";
            }
            return (TResult)instanceResult;
        }
    }
</h6>
