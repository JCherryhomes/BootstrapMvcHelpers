using System.Collections.Generic;
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
        /// Renders a Twitter Bootstrap NavBar component.
        /// </summary>
        /// <param name="helper">The HTML helper.</param>
        /// <param name="menuList">The menu list.</param>
        /// <param name="brandPosition">The brand position.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString NavBar(this HtmlHelper helper, IEnumerable<MenuItem> menuList, BrandPosition brandPosition=BrandPosition.None, NavBarDisplay displayType=NavBarDisplay.Normal, NavBarPosition navbarPosition=NavBarPosition.Default, object htmlAttributes=null)
        {
            var navBar = new NavBarHelper(helper, menuList, brandPosition, displayType, navbarPosition, htmlAttributes);
            return new MvcHtmlString(navBar.Render());
        }

        /// <summary>
        /// Renders a Twitter Bootstrap Button Component
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="status">The status.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString Button(this HtmlHelper helper, string text, string action, string controller="", ButtonStatus status = ButtonStatus.Default, object htmlAttributes = null)
        {
            ButtonHelper button = new ButtonHelper(helper, GetStatusStrategy(status), text, action, controller, status, htmlAttributes);
            return new MvcHtmlString(button.Render());
        }

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
            LabelHelper labelHelper = new LabelHelper(GetStatusStrategy(status), text, status, htmlAttributes);
            return new MvcHtmlString(labelHelper.Render());
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
            BadgeHelper badgeHelper = new BadgeHelper(helper, GetStatusStrategy(status), text, status, htmlAttributes);
            return new MvcHtmlString(badgeHelper.Render());
        }

        /// <summary>
        /// Renders a Twitter Bootstrap breadcrumb component.
        /// </summary>
        /// <param name="helper">The Html helper.</param>
        /// <param name="actionLinks">The action links.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>returns a breadcrumb html string</returns>
        /// <example>@Html.Breadcrumb(links, new { @class="span5 offset1" })</example>
        public static MvcHtmlString Breadcrumb(this HtmlHelper helper, IEnumerable<MenuItem> actionLinks, object htmlAttributes=null)
        {
            BreadcrumbHelper breadcrumbHelper = new BreadcrumbHelper(helper, actionLinks, htmlAttributes);
            return new MvcHtmlString(breadcrumbHelper.Render());
        }

        /// <summary>
        /// Renders the menu drop down component.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString MenuDropDown(this HtmlHelper helper, string name, IEnumerable<MenuItem> selectList, string optionLabel=null, object htmlAttributes=null)
        {
            DropDownHelper dropDownHelper = new DropDownHelper(helper, name, selectList, optionLabel, htmlAttributes);
            return new MvcHtmlString(dropDownHelper.Render());
        }

        /// <summary>
        /// The menu item.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        public static MenuItem MenuItem(this HtmlHelper helper, string text, string action, string controller=null)
        {
            return new MenuItem(helper, text, action, controller);
        }

        /// <summary>
        /// Gets the status strategy.
        /// </summary>
        /// <param name="status">The status.</param>
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

        /// <summary>
        /// Gets the button status strategy.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        private static IStatusStrategy GetStatusStrategy(ButtonStatus status)
        {
            IStatusStrategy strategy;
            switch (status)
            {
                case ButtonStatus.Success:
                    strategy = new SuccessStatusStrategy();
                    break;
                case ButtonStatus.Warning:
                    strategy = new WarningStatusStrategy();
                    break;
                case ButtonStatus.Danger:
                    strategy = new DangerStatusStrategy();
                    break;
                case ButtonStatus.Info:
                    strategy = new InfoStatusStrategy();
                    break;
                case ButtonStatus.Inverse:
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
