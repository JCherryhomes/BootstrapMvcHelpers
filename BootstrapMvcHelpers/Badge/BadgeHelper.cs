using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapMvcHelpers.Interfaces;

namespace BootstrapMvcHelpers
{
    internal class BadgeHelper : IBootstrapComponent
    {
        private IStatusStrategy strategy;
        private HtmlHelper helper;
        private string text;
        private BootstrapStatus status;
        private object htmlAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeHelper" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="strategy">The strategy.</param>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        public BadgeHelper(HtmlHelper helper, IStatusStrategy strategy, string text, BootstrapStatus status, object htmlAttributes)
        {
            this.helper = helper;
            this.strategy = strategy;
            this.status = status;
            this.htmlAttributes = htmlAttributes;
            this.text = text;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        public string Render()
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
