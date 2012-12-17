using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;

namespace BootstrapMvcHelpers
{
    public class DropDownHelper
    {
        private HtmlHelper helper;

        public string DropDown(HtmlHelper helper, string name, IEnumerable<MenuDropDownItem> selectList, string optionLabel, object htmlAttributes)
        {
            this.helper = helper;
            var divBuilder = GetDropDownDivBuilder(name, htmlAttributes);
            var triggerBuilder = GetDropDownTriggerBuilder(name, optionLabel);
            var caretBuilder = GetTriggerCaretBuilder();

            triggerBuilder.InnerHtml += caretBuilder.ToString();
            divBuilder.InnerHtml += triggerBuilder.ToString();

            var dropDownBuilder = new TagBuilder("ul");
            dropDownBuilder.AddCssClass("dropdown-menu");
            dropDownBuilder.Attributes.Add("role", "menu");

            foreach (var item in selectList)
            {
                var listItemBuilder = GetListItemBuilder(item);
                dropDownBuilder.InnerHtml += Environment.NewLine + listItemBuilder.ToString(TagRenderMode.Normal);
            }

            divBuilder.InnerHtml += dropDownBuilder.ToString(TagRenderMode.Normal);
            return divBuilder.ToString(TagRenderMode.Normal);
        }

        private TagBuilder GetListItemBuilder(MenuDropDownItem item)
        {
            var listItemBuilder = new TagBuilder("li");
            var anchorBuilder = GetListItemAnchorBuilder(item);

            listItemBuilder.InnerHtml = anchorBuilder.ToString();
            return listItemBuilder;
        }

        private TagBuilder GetListItemAnchorBuilder(MenuDropDownItem item)
        {
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.Attributes.Add("tabindex", "-1");

            if (string.IsNullOrWhiteSpace(item.Controller))
            {
                item.Controller = helper.ViewContext.Controller.GetType().Name;
            }

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
            anchorBuilder.Attributes.Add("href", urlHelper.Action(item.Action, item.Controller));
            anchorBuilder.SetInnerText(item.Text);

            return anchorBuilder;
        }

        private TagBuilder GetDropDownDivBuilder(string name, object htmlAttributes)
        {
            var divBuilder = new TagBuilder("div");
            divBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            divBuilder.AddCssClass("dropdown");
            divBuilder.Attributes.Add("id", name);
            divBuilder.Attributes.Add("name", name);
            return divBuilder;
        }

        private TagBuilder GetTriggerCaretBuilder()
        {
            var caretBuilder = new TagBuilder("b");
            caretBuilder.AddCssClass("caret");
            return caretBuilder;
        }

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
