using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;
using BootstrapMvcHelpers.Interfaces;

namespace BootstrapMvcHelpers
{
    internal class LabelHelper : IBootstrapComponent
    {
        private IStatusStrategy strategy;
        private object htmlAttributes;
        private string text;
        private BootstrapStatus status;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelHelper" /> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        public LabelHelper(IStatusStrategy strategy, string text, BootstrapStatus status, object htmlAttributes)
        {
            this.strategy = strategy;
            this.text = text;
            this.status = status;
            this.htmlAttributes = htmlAttributes;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public string Render()
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
