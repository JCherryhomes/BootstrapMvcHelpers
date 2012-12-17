using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BootstrapMvcHelpers.Strategies;

namespace BootstrapMvcHelpers
{
    /// <summary>
    /// The Bootstrap Html Helper class
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Renders a Twitter Bootstrap label component
        /// </summary>
        /// <param name="helper">The html helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="status">The label status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Returns a label html string</returns>
        public static MvcHtmlString Label(this HtmlHelper helper, string text, BootstrapStatus status, object htmlAttributes)
        {
            LabelHelper labelHelper = new LabelHelper(GetStatusStrategy(status));
            return new MvcHtmlString(labelHelper.Label(text, status, htmlAttributes));
        }

        /// <summary>
        /// Renders a Twitter Bootstrap badge component
        /// </summary>
        /// <param name="helper">The html helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="status">The status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>Returns a badge html string</returns>
        public static MvcHtmlString Badge(this HtmlHelper helper, string text, BootstrapStatus status, object htmlAttributes)
        {
            BadgeHelper badgeHelper = new BadgeHelper(GetStatusStrategy(status));
            return new MvcHtmlString(badgeHelper.Badge(text, status, htmlAttributes));
        }

        /// <summary>
        /// Renders a Twitter Bootstrap breadcrumb component.
        /// </summary>
        /// <param name="helper">The Html helper.</param>
        /// <param name="actionLinks">The action links.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>returns a breadcrumb html string</returns>
        /// <example>@Html.Breadcrumb(links, new { @class="span5 offset1" })</example>
        public static MvcHtmlString Breadcrumb(this HtmlHelper helper, IEnumerable<string> actionLinks, object htmlAttributes=null)
        {
            BreadcrumbHelper breadcrumbHelper = new BreadcrumbHelper();
            return new MvcHtmlString(breadcrumbHelper.Breadcrumb(helper, actionLinks, htmlAttributes));
        }

        public static MvcHtmlString MenuDropDown(this HtmlHelper helper, string name, IEnumerable<MenuDropDownItem> selectList, string optionLabel=null, object htmlAttributes=null)
        {
            DropDownHelper dropDownHelper = new DropDownHelper();
            return new MvcHtmlString(dropDownHelper.DropDown(helper, name, selectList, optionLabel, htmlAttributes));
        }

        public static MenuDropDownItem MenuDropDownItem(this HtmlHelper helper, string text, string action, string controller=null)
        {
            return new MenuDropDownItem { Action = action, Controller = controller, Text = text };
        }

        private static IStatusStrategy GetStatusStrategy(BootstrapStatus status)
        {
            IStatusStrategy strategy;
            switch (status)
            {
                case BootstrapStatus.Success:
                    strategy = new SuccessStatusStrategy();
                    break;
                case BootstrapStatus.Warning:
                    strategy = new WarningStatusStrategy();
                    break;
                case BootstrapStatus.Important:
                    strategy = new ImportantStatusStrategy();
                    break;
                case BootstrapStatus.Info:
                    strategy = new InfoStatusStrategy();
                    break;
                case BootstrapStatus.Inverse:
                    strategy = new InverseStatusStragety();
                    break;
                default:
                    strategy = new DefaultStatusStrategy();
                    break;
            }

            return strategy;
        }
    }
}
