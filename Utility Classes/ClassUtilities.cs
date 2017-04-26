using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sukasa.UtilityClasses
{
    public static class ClassUtilities
    {
        public static IEnumerable<Type> FindClasses(Func<Type, bool> Predicate)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(Predicate);
        }

        private static IEnumerable<Type> Test()
        {
            return null;
        }


        public static IEnumerable<Type> FindClasses(Type Filter, bool IncludeRoot = false)
        {
            try
            {
                return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => Filter.IsAssignableFrom(x) && (IncludeRoot || (x != Filter)));
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                throw new Exception("Failed to iterate through:\r\n" + errorMessage);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
