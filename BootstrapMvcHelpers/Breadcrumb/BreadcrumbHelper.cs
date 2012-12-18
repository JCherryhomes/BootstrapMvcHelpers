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
        /// The html helper
        /// </summary>
        private HtmlHelper helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbHelper" /> class.
        /// </summary>
        public BreadcrumbHelper()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbHelper" /> class.
        /// </summary>
        /// <param name="helper">The html helper.</param>
        public BreadcrumbHelper(HtmlHelper helper)
        {
            this.helper = helper;
        }

        /// <summary>
        /// Renders a Twitter Bootstrap breadcrumb.
        /// </summary>
        /// <param name="helper">The Html helper.</param>
        /// <param name="actionLinks">The action links.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>returns a breadcrumb html string</returns>
        /// <example>@Html.Breadcrumb(links, new { @class="span5 offset1" })</example>
        public string Breadcrumb(HtmlHelper helper, IEnumerable<MenuItem> actionLinks, object htmlAttributes)
        {
            var builder = new TagBuilder("ul");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass("breadcrumb");

            int linkCount = actionLinks.Count();

            for (int i=0; i<linkCount; i++)
            {
                var listBuilder = new TagBuilder("li");
                MenuItem item = actionLinks.ElementAt(i);

                if (i == linkCount - 1)
                {
                    listBuilder.AddCssClass("active");
                    listBuilder.InnerHtml = item.Text;
                }
                else
                {
                    listBuilder.InnerHtml = item.ToString();
                }

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
