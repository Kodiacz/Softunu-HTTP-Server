namespace SoftuniHTTPServer.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {
        // Type because we dont know what is the passed type gonna be:
        private readonly Dictionary<Type, Type> dependancyContainer = new Dictionary<Type, Type>();
        public void Add<TSource, TDestination>()
        {
            this.dependancyContainer[typeof(TSource)] = typeof(TDestination);
        }

        /// <summary>
        /// Creates and instance of the passed interface or abstrac class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateInstance(Type type)
        {
            if (dependancyContainer.ContainsKey(type))
            {
                type = this.dependancyContainer[type];
            }

            var constructor = type.GetConstructors()
                .OrderBy(x => x.GetParameters().Count())
                .FirstOrDefault();

            var parametars = constructor.GetParameters();
            var parametarValues = new List<object>();
            foreach (var parameter in parametars)
            {
                var parameterValue = CreateInstance(parameter.ParameterType);
                parametarValues.Add(parameterValue);
            }

            var obj = constructor.Invoke(parametarValues.ToArray());
            return obj;
        }
    }
}
