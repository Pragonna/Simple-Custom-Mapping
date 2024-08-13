using System.IO.IsolatedStorage;

namespace MAPPING
{
    public class ObjectMapper
    {
        public static TResult Map<TSource,TResult>(TSource source, out string information)
        {
            information = string.Empty;

            var instanceResult = Activator.CreateInstance(typeof(TResult));
            var resultProperties = instanceResult.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();

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
}
