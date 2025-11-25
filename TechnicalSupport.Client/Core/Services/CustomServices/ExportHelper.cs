namespace TechnicalSupport.Client.Core.Services.CustomServices;

public class ExportHelper
{
    public List<Dictionary<string, object>> ConvertToDictionary<T>(
                                            IEnumerable<T> source,
                                            Dictionary<string, string> columnTitles,
                                            List<string> nestedPropertyNames = null)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source), "Source cannot be null.");

        if (columnTitles == null)
            throw new ArgumentNullException(nameof(columnTitles), "Column titles cannot be null.");

        return source.Select(item =>
        {
            var dict = new Dictionary<string, object>();

            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // Get the direct property value
                object value = property.GetValue(item);

                // Check for direct mapping for primitive properties
                if (columnTitles.TryGetValue(property.Name, out var title))
                {
                    dict[title] = value ?? DBNull.Value;
                }

                // Handle only navigation properties (classes)
                if (value != null && IsNavigationProperty(property.PropertyType))
                {
                    // Iterate through properties of the nested type (navigation property)
                    foreach (var nestedProperty in property.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        // Use the nested property name directly
                        string nestedPropertyKey = property.Name + "." + nestedProperty.Name;

                        //TODO: Refactor This Part
                        // Only proceed if the nested property is in the specified list
                        if (nestedPropertyNames != null && nestedPropertyNames.Contains(nestedPropertyKey))
                        {
                            // Check if the key exists in columnTitles
                            if (columnTitles.TryGetValue(nestedPropertyKey, out var nestedTitle))
                            {
                                var nestedValue = nestedProperty.GetValue(value);

                                // Add the value to the dictionary, using DBNull.Value if null
                                dict[nestedTitle] = nestedValue ?? DBNull.Value;

                                // Log for debugging
                                Console.WriteLine($"Nested Property Found: {nestedPropertyKey}, Value: {nestedValue}");
                            }
                        }
                    }
                }
            }

            return dict;
        }).ToList();
    }

    // Check if the type is a navigation property (i.e., a class and not a primitive type or string)
    private static bool IsNavigationProperty(Type type)
    {
        return type.IsClass && type != typeof(string) && !type.IsGenericType; // Exclude strings and generic types
    }
}
