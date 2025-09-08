using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace MvcMovieTrauner.Infrastructure
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {

        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            yield return "/Features/{1}/Views/{0}.cshtml";
            yield return "/Features/Shared/{0}.cshtml";
            foreach (var location in viewLocations)
            {
                yield return location;
            }
        }
    }
}
