using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BootstrapMvcHelpers
{
    internal class BadgeHelper
    {
        private IStatusStrategy strategy;

        public BadgeHelper(IStatusStrategy strategy)
        {
            this.strategy = strategy;
        }

        /// <summary>
        /// Renders a Twitter Bootstrap badge component
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Returns a badge html string</returns>
        public string Badge(string text, BootstrapStatus status, object htmlAttributes)
        {
            string cssClass = strategy.GetClassForStatus("badge");

            var builder = new TagBuilder("span");
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass(cssClass);
            builder.SetInnerText(text);
            return builder.ToString(TagRenderMode.Normal);
        }
    }
}
