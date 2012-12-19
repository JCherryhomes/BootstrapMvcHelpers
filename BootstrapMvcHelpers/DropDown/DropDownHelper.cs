using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using BootstrapMvcHelpers.Interfaces;

namespace BootstrapMvcHelpers
{
    /// <summary>
    /// The DropDownHelper class
    /// </summary>
    public class DropDownHelper : IBootstrapComponent
    {
        /// <summary>
        /// The html helper
        /// </summary>
        private HtmlHelper helper;

        /// <summary>
        /// The name
        /// </summary>
        private string name;

        /// <summary>
        /// The select list
        /// </summary>
        private IEnumerable<MenuItem> selectList;

        /// <summary>
        /// The option label
        /// </summary>
        private string optionLabel;

        /// <summary>
        /// The HTML attributes
        /// </summary>
        private object htmlAttributes;

        /// <summary>
        /// Gets the string representation of the menu drop down control.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public DropDownHelper(HtmlHelper helper, string name, IEnumerable<MenuItem> selectList, string optionLabel, object htmlAttributes)
        {
            this.helper = helper;
            this.name = name;
            this.selectList = selectList;
            this.optionLabel = optionLabel;
            this.htmlAttributes = htmlAttributes;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            var divBuilder = GetDropDownDivBuilder(name, htmlAttributes);
            var triggerBuilder = GetDropDownTriggerBuilder(name, optionLabel);
            var caretBuilder = GetTriggerCaretBuilder();

            triggerBuilder.InnerHtml += caretBuilder.ToString();
            divBuilder.InnerHtml += triggerBuilder.ToString();

            var dropDownBuilder = GetUnorderedListBuilder();

            foreach (var item in selectList)
            {
                var listItemBuilder = GetListItemBuilder(item);
                dropDownBuilder.InnerHtml += Environment.NewLine + listItemBuilder.ToString(TagRenderMode.Normal);
            }

            divBuilder.InnerHtml += dropDownBuilder.ToString(TagRenderMode.Normal);
            return divBuilder.ToString(TagRenderMode.Normal);
        }

        /// <summary>
        /// Gets the unordered list builder.
        /// </summary>
        /// <returns></returns>
        private TagBuilder GetUnorderedListBuilder()
        {
            var dropDownBuilder = new TagBuilder("ul");
            dropDownBuilder.AddCssClass("dropdown-menu");
            dropDownBuilder.Attributes.Add("role", "menu");
            return dropDownBuilder;
        }

        /// <summary>
        /// Gets the list item builder.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private TagBuilder GetListItemBuilder(MenuItem item)
        {
            var listItemBuilder = new TagBuilder("li");

            listItemBuilder.InnerHtml = item.Render();
            return listItemBuilder;
        }

        /// <summary>
        /// Gets the drop down div builder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        private TagBuilder GetDropDownDivBuilder(string name, object htmlAttributes)
        {
            var divBuilder = new TagBuilder("div");
            divBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            divBuilder.AddCssClass("dropdown");
            divBuilder.Attributes.Add("id", name);
            divBuilder.Attributes.Add("name", name);
            return divBuilder;
        }

        /// <summary>
        /// Gets the trigger caret builder.
        /// </summary>
        /// <returns></returns>
        private TagBuilder GetTriggerCaretBuilder()
        {
            var caretBuilder = new TagBuilder("b");
            caretBuilder.AddCssClass("caret");
            return caretBuilder;
        }

        /// <summary>
        /// Gets the drop down trigger builder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <returns></returns>
        private TagBuilder GetDropDownTriggerBuilder(string name, string optionLabel)
        {
            var triggerBuilder = new TagBuilder("a");
            triggerBuilder.AddCssClass("dropdown-toggle btn btn-info");
            triggerBuilder.Attributes.Add("id", name + "Label");
            triggerBuilder.Attributes.Add("role", "button");
            triggerBuilder.Attributes.Add("data-toggle", "dropdown");
            triggerBuilder.Attributes.Add("data-target", "#");
            triggerBuilder.SetInnerText(optionLabel ?? "Select");
            return triggerBuilder;
        }
    }
}
