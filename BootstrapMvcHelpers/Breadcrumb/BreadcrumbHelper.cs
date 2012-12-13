using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BootstrapMvcHelpers
{
    internal class BreadcrumbHelper
    {
        /// <summary>
        /// Renders a Twitter Bootstrap breadcrumb.
        /// </summary>
        /// <param name="helper">The Html helper.</param>
        /// <param name="actionLinks">The action links.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>returns a breadcrumb html string</returns>
        /// <example>@Html.Breadcrumb(links, new { @class="span5 offset1" })</example>
        public string Breadcrumb(HtmlHelper helper, IEnumerable<string> actionLinks, object htmlAttributes)
        {
            var builder = new TagBuilder("ul");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass("breadcrumb");

            int linkCount = actionLinks.Count();

            for (int i=0; i<linkCount; i++)
            {
                var listBuilder = new TagBuilder("li");

                if (i == linkCount - 1)
                {
                    listBuilder.AddCssClass("active");
                }

                listBuilder.InnerHtml = actionLinks.ElementAt(i);

                var spanBuilder = new TagBuilder("span");
                spanBuilder.AddCssClass("divider");
                spanBuilder.SetInnerText(@"/");

                listBuilder.InnerHtml += spanBuilder.ToString(TagRenderMode.Normal);
                builder.InnerHtml += listBuilder.ToString(TagRenderMode.Normal);
            }
            return builder.ToString(TagRenderMode.Normal);
        }
    }
}
