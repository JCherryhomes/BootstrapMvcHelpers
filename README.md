BootstrapMvcHelpers
===================

This library provides html helpers for implementing the Twitter bootstrap components in an ASP.NET MVC application. This library is dependant upon the Twitter bootstrap stylesheet and javascript files already being included in the application. These files can be downloaded from the <a href='http://twitter.github.com/bootstrap/'>Twitter Bootstrap</a> site.

<h4>NavBar Example:</h4>
<pre>
@{
    var items = new List<MenuItem>
    {
        Html.MenuItem("Bootstrap MVC Helpers", "Index", "Home"),
        Html.MenuItem("Home Page", "Index", "Home"),
        Html.MenuItem("About Page", "About", "Home")
    };
}

@Html.NavBar(items, BrandPosition.Start, NavBarDisplay.Normal, NavBarPosition.Default)
</pre>

<h4>Other Components:</h4>
<pre>
@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/LayoutPage.cshtml";

    var items = new List<MenuItem>
    {
        Html.MenuItem("Home Page", "Index", "Home"),
        Html.MenuItem("About Page", "About", "Home")
    };
}
&lt;div class="span12"&gt;
    &lt;div class="well"&gt;
        @Html.Label("Success", BootstrapStatus.Success, new { id = "Label1" })
        @Html.Label("Default", BootstrapStatus.Default, new { id = "Label2" })
        @Html.Label("Warning", BootstrapStatus.Warning, new { id = "Label3" })
        @Html.Label("Important", BootstrapStatus.Important, new { id = "Label4" })
        @Html.Label("Info", BootstrapStatus.Info, new { id = "Label5" })
        @Html.Label("Info", BootstrapStatus.Inverse, new { id = "Label6" })
    &lt;/div&gt;

    &lt;div class="well"&gt;
        @Html.Badge("1", BootstrapStatus.Success, new { id = "Badge1" })
        @Html.Badge("2", BootstrapStatus.Default, new { id = "Badge2" })
        @Html.Badge("3", BootstrapStatus.Warning, new { id = "Badge3" })
        @Html.Badge("4", BootstrapStatus.Important, new { id = "Badge4" })
        @Html.Badge("5", BootstrapStatus.Info, new { id = "Badge5" })
        @Html.Badge("6", BootstrapStatus.Inverse, new { id = "Badge6" })
    &lt;/div&gt;

    &lt;div class="well"&gt;
        @Html.Breadcrumb(items)
        @Html.MenuDropDown("DropDown", items)
        @Html.Button("Test", "Index", "Home", ButtonStatus.Info, htmlAttributes: new { @class = "input-small" })
    &lt;/div&gt;
&lt;/div&gt;
</pre>

