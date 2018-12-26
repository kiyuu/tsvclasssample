using System.ComponentModel;

namespace ConsoleApp8.Utility
{
    public class ConvertUtility
    {
        public static bool TryParse<T>(string input, out T result)
        {
            result = default(T);
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    result = (T)converter.ConvertFromString(input);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
