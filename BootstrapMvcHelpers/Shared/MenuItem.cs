using System.Web.Mvc;

namespace BootstrapMvcHelpers
{
    /// <summary>
    /// The MenuDropDownItem class
    /// </summary>
    /// <remarks>This class is used to define the items in the MenuDropDown control</remarks>
    public class MenuItem
    {
        /// <summary>
        /// The html helper
        /// </summary>
        internal HtmlHelper helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="action">The action.</param>
        /// <param name="controller">The controller.</param>
        internal MenuItem(HtmlHelper helper, string text="", string action="", string controller="")
        {
            this.helper = helper;
            this.Text = text;
            this.Action = action;
            this.Controller = controller;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public string Controller { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public string Render()
        {
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.Attributes.Add("tabindex", "-1");

            if (string.IsNullOrWhiteSpace(Controller))
            {
                Controller = helper.ViewContext.Controller.GetType().Name;
            }

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            anchorBuilder.Attributes.Add("href", urlHelper.Action(Action, Controller));
            anchorBuilder.SetInnerText(Text);

            return anchorBuilder.ToString();
        }
    }
}
