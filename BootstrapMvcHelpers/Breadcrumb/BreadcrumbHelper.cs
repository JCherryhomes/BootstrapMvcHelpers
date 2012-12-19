using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapMvcHelpers.Interfaces;

namespace BootstrapMvcHelpers
{
    internal class BreadcrumbHelper : IBootstrapComponent
    {
        /// <summary>
        /// The html helper
        /// </summary>
        private readonly HtmlHelper helper;

        /// <summary>
        /// The HTML attributes
        /// </summary>
        private readonly object htmlAttributes;

        /// <summary>
        /// The action links
        /// </summary>
        private readonly IEnumerable<MenuItem> actionLinks;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbHelper" /> class.
        /// </summary>
        /// <param name="helper">The html helper.</param>
        public BreadcrumbHelper(HtmlHelper helper, IEnumerable<MenuItem> actionLinks, object htmlAttributes)
        {
            this.helper = helper;
            this.actionLinks = actionLinks;
            this.htmlAttributes = htmlAttributes;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            var builder = GetUnorderedListBuilder();
            int linkCount = actionLinks.Count();

            for (int i=0; i<linkCount; i++)
            {
                var listBuilder = GetListItemBuilder(linkCount, i);
                var spanBuilder = GetSpanBuilder();

                listBuilder.InnerHtml += spanBuilder.ToString(TagRenderMode.Normal);
                builder.InnerHtml += listBuilder.ToString(TagRenderMode.Normal);
            }
            return builder.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Gets the unordered list tag builder.
        /// </summary>
        private TagBuilder GetUnorderedListBuilder()
        {
            var builder = new TagBuilder("ul");

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.AddCssClass("breadcrumb");
            return builder;
        }

        /// <summary>
        /// Gets the span tag builder.
        /// </summary>
        private static TagBuilder GetSpanBuilder()
        {
            var spanBuilder = new TagBuilder("span");
            spanBuilder.AddCssClass("divider");
            spanBuilder.SetInnerText(@"/");
            return spanBuilder;
        }

        /// <summary>
        /// Gets the list item tag builder.
        /// </summary>
        /// <param name="linkCount">The link count.</param>
        /// <param name="index">The index.</param>
        private TagBuilder GetListItemBuilder(int linkCount, int index)
        {
            var listBuilder = new TagBuilder("li");
            MenuItem item = actionLinks.ElementAt(index);

            // If this is the last item
            if (index == linkCount - 1)
            {
                listBuilder.AddCssClass("active");
                listBuilder.InnerHtml = item.Text;
            }
            else
            {
                listBuilder.InnerHtml = item.Render();
            }
            return listBuilder;
        }
    }
}
