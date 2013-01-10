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
        private readonly BrandPosition brandPosition;
        private readonly NavBarDisplay displayType;
        private readonly NavBarPosition navbarPosition;
        private readonly object htmlAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavBarHelper" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="menuList">The menu list.</param>
        /// <param name="position">The position.</param>
        public NavBarHelper(HtmlHelper helper, IEnumerable<MenuItem> menuList, BrandPosition brandPosition, NavBarDisplay displayType, NavBarPosition navbarPosition, object htmlAttributes)
        {
            this.helper = helper;
            this.menuList = menuList;
            this.brandPosition = brandPosition;
            this.navbarPosition = navbarPosition;
            this.htmlAttributes = htmlAttributes;
            this.displayType = displayType;
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
            mainDivBuilder.AddCssClass(GetNavBarDisplayTypeCssClass(displayType));
            mainDivBuilder.AddCssClass(GetNavBarPositionCssClass(navbarPosition));

            var innerDivBuilder = GetInnerDivBuilder();
            mainDivBuilder.InnerHtml += innerDivBuilder.ToString();

            return mainDivBuilder.ToString();
        }

        /// <summary>
        /// Gets the nav bar display type CSS class.
        /// </summary>
        /// <param name="displayType">The display type.</param>
        /// <returns></returns>
        private string GetNavBarDisplayTypeCssClass(NavBarDisplay displayType)
        {
            switch (displayType)
            {
                case NavBarDisplay.Inverse:
                    return "navbar-inverse";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the nav bar position CSS class.
        /// </summary>
        /// <param name="navbarPosition">The navbar position.</param>
        /// <returns></returns>
        private string GetNavBarPositionCssClass(NavBarPosition navbarPosition)
        {
            switch (navbarPosition)
            {
                case NavBarPosition.Top:
                    return "navbar-fixed-top";
                case NavBarPosition.Bottom:
                    return "navbar-fixed-bottom";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the inner div tag builder.
        /// </summary>
        /// <returns></returns>
        private TagBuilder GetInnerDivBuilder()
        {
            var innerDivBuilder = GetDivBuilder();
            var brandBuilder = GetBrandBuilder();
            var containerDiv = GetDivBuilder();
            var navListBuilder = GetUnorderedListBuilder();

            innerDivBuilder.AddCssClass("navbar-inner");
            containerDiv.AddCssClass("container");

            containerDiv.InnerHtml += GetCollapseControlBuilder().ToString();
            containerDiv.InnerHtml += navListBuilder.ToString();

            AddBrandItemHtml(containerDiv, brandBuilder);

            innerDivBuilder.InnerHtml += containerDiv.ToString();
            return innerDivBuilder;
        }

        /// <summary>
        /// Adds the brand item HTML to the inner div builder.
        /// </summary>
        /// <param name="innerDivBuilder">The inner div builder.</param>
        /// <param name="brandBuilder">The brand builder.</param>
        private void AddBrandItemHtml(TagBuilder innerDivBuilder, TagBuilder brandBuilder)
        {
            if (brandPosition == BrandPosition.End)
            {
                innerDivBuilder.InnerHtml += string.Format("{0} ", brandBuilder);
            }
            else
            {
                innerDivBuilder.InnerHtml = string.Format(" {0}{1}", brandBuilder, innerDivBuilder.InnerHtml);
            }
        }

        /// <summary>
        /// Gets the collapse control builder.
        /// </summary>
        /// <returns></returns>
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
                    (brandPosition == BrandPosition.Start && item == menuList.FirstOrDefault())
                    || (brandPosition == BrandPosition.End && item == menuList.LastOrDefault());

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
            switch (brandPosition)
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

            if (item == null)
            {
                return null;
            }

            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.AddCssClass("brand");

            if (string.IsNullOrWhiteSpace(item.Controller))
            {
                item.Controller = helper.ViewContext.Controller.GetType().Name;
            }

            BuildAnchorFromMenuItem(item, anchorBuilder);
            return anchorBuilder;
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
