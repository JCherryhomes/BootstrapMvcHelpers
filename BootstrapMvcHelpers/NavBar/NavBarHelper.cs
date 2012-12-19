using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BootstrapMvcHelpers.Interfaces;
using System.Web.Routing;

namespace BootstrapMvcHelpers
{
    public class NavBarHelper : IBootstrapComponent
    {
        private readonly HtmlHelper helper;
        private readonly IEnumerable<MenuItem> menuList;
        private readonly BrandPosition position;
        private readonly object htmlAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarHelper" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="menuList">The menu list.</param>
        /// <param name="position">The position.</param>
        public NavBarHelper(HtmlHelper helper, IEnumerable<MenuItem> menuList, BrandPosition position, object htmlAttributes)
        {
            this.helper = helper;
            this.menuList = menuList;
            this.position = position;
            this.htmlAttributes = htmlAttributes;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public string Render()
        {

            var mainDivBuilder = GetDivBuilder();
            mainDivBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            mainDivBuilder.AddCssClass("navbar");

            var innerDivBuilder = GetInnerDivBuilder();
            mainDivBuilder.InnerHtml += innerDivBuilder.ToString();
            
            return mainDivBuilder.ToString();
        }

        /// <summary>
        /// Gets the inner div tag builder.
        /// </summary>
        /// <returns></returns>
        private TagBuilder GetInnerDivBuilder()
        {
            var innerDivBuilder = GetDivBuilder();
            innerDivBuilder.AddCssClass("navbar-inner");

            var containerDiv = GetDivBuilder();
            containerDiv.AddCssClass("container");

            containerDiv.InnerHtml += GetCollapseControlBuilder().ToString();

            if (position == BrandPosition.Start)
            {
                var brandBuilder = GetBrandBuilder();
                innerDivBuilder.InnerHtml += brandBuilder.ToString(TagRenderMode.Normal);
            }

            var navListBuilder = GetUnorderedListBuilder();
            containerDiv.InnerHtml += navListBuilder.ToString();

            if (position == BrandPosition.End)
            {
                var brandBuilder = GetBrandBuilder();
                innerDivBuilder.InnerHtml += brandBuilder.ToString(TagRenderMode.Normal);
            }

            innerDivBuilder.InnerHtml += containerDiv.ToString();
            return innerDivBuilder;
        }

        private TagBuilder GetCollapseControlBuilder()
        {
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.AddCssClass("btn btn-navbar");
            anchorBuilder.Attributes.Add("data-toggle", "collapse");
            anchorBuilder.Attributes.Add("data-target", ".nav-collapse");

            var spanBuilder = new TagBuilder("span");
            spanBuilder.AddCssClass("icon-bar");

            anchorBuilder.InnerHtml += spanBuilder.ToString();
            anchorBuilder.InnerHtml += spanBuilder.ToString();
            anchorBuilder.InnerHtml += spanBuilder.ToString();

            return anchorBuilder;
        }

        /// <summary>
        /// Gets the list item tag builder.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private TagBuilder GetListItemBuilder(MenuItem item)
        {
            var builder = new TagBuilder("li");
            var anchorBuilder = new TagBuilder("a");

            BuildAnchorFromMenuItem(item, anchorBuilder);
            builder.InnerHtml += anchorBuilder.ToString();

            return builder;
        }

        /// <summary>
        /// Gets the unordered list tag builder.
        /// </summary>
        /// <returns></returns>
        private TagBuilder GetUnorderedListBuilder()
        {
            var divBuilder = GetDivBuilder();
            divBuilder.AddCssClass("nav-collapse collapse");

            var builder = new TagBuilder("ul");
            builder.AddCssClass("nav");

            foreach (var item in menuList)
            {
                bool isBrandItem = 
                    (position == BrandPosition.Start && item == menuList.FirstOrDefault())
                    || (position == BrandPosition.End && item == menuList.LastOrDefault());

                if (!isBrandItem)
                {
                    var listBuilder = GetListItemBuilder(item);
                    builder.InnerHtml += listBuilder.ToString();
                }
            }

            divBuilder.InnerHtml += builder.ToString();
            return divBuilder;
        }

        /// <summary>
        /// Gets the brand tag builder.
        /// </summary>
        /// <returns></returns>
        private TagBuilder GetBrandBuilder()
        {
            MenuItem item = null;
            switch (position)
            {
                case BrandPosition.Start:
                    item = menuList.FirstOrDefault();
                    break;
                case BrandPosition.End:
                    item = menuList.LastOrDefault();
                    break;
                default:
                    break;
            }

            if (item != null)
            {
                var anchorBuilder = new TagBuilder("a");
                anchorBuilder.AddCssClass("brand");

                if (string.IsNullOrWhiteSpace(item.Controller))
                {
                    item.Controller = helper.ViewContext.Controller.GetType().Name;
                }

                BuildAnchorFromMenuItem(item, anchorBuilder);
                return anchorBuilder;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Builds the anchor from menu item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="anchorBuilder">The anchor builder.</param>
        private void BuildAnchorFromMenuItem(MenuItem item, TagBuilder anchorBuilder)
        {
            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            anchorBuilder.Attributes.Add("href", urlHelper.Action(item.Action, item.Controller));

            string action = helper.ViewContext.RouteData.Values["action"].ToString();
            string controller = helper.ViewContext.RouteData.Values["controller"].ToString();

            if (item.Controller == controller && item.Action == action)
            {
                anchorBuilder.AddCssClass("active");
            }

            anchorBuilder.SetInnerText(item.Text);
        }

        /// <summary>
        /// Gets the div tag builder.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns></returns>
        private TagBuilder GetDivBuilder()
        {
            var builder = new TagBuilder("div");
            return builder;
        }
    }
}
