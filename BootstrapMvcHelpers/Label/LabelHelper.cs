using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace BootstrapMvcHelpers
{
    internal class LabelHelper
    {
        private IStatusStrategy strategy;

        public LabelHelper(IStatusStrategy strategy)
        {
            this.strategy = strategy;
        }

        /// <summary>
        /// Renders a Twitter Bootstrap label component
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="status">The label status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Returns a label html string</returns>
        public string Label(string text, BootstrapStatus status, object htmlAttributes)
        {
            string cssClass = strategy.GetClassForStatus("label");

            var builder = new TagBuilder("span");
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass(cssClass);
            builder.SetInnerText(text);
            return builder.ToString(TagRenderMode.Normal);
        }
    }
}
