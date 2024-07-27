using HTOTools;
using HTOTools.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace DataMigration.TagHelpers
{
    public class CrudLinksTagHelper : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;

        private readonly IHttpContextAccessor _httpContext;


        public CrudLinksTagHelper(IHttpContextAccessor contextAccessor, IHtmlHelper htmlHelper) { 
            _httpContext = contextAccessor;
            _htmlHelper = htmlHelper;
        }


        public HTORowCtrlList RecordControls { get; set; }


        public ActionHistory LastAction { get; set; }


        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";    // Replaces <email> with <a> tag
            //output.AddClass("hto-cmd", HtmlEncoder.Default);

            var navpills = new TagBuilder("ul");

            //navpills.AddCssClass("nav");
            //navpills.AddCssClass("nav-pills");

            List<ActionHistory> templist = _httpContext.HttpContext.Session.GetObjectFromJson<List<ActionHistory>>("actionhistory");


            var validate = _httpContext.HttpContext.Session.GetString("TestThis");

            (_htmlHelper as IViewContextAware).Contextualize(ViewContext);

            if (LastAction != null && !string.IsNullOrEmpty(LastAction.ActionName))
            {

                IHtmlContent lastactionlink = _htmlHelper.ActionLink(
                    "Return to " + LastAction.RecordDescription,
                    LastAction.ActionName,
                    LastAction.ActionController,
                    LastAction.RouteValues,
                    new { @class = "hto-exit-ctrl" }
                    );

                var li = new TagBuilder("li");
                navpills.InnerHtml.AppendHtml(li.RenderStartTag());
                navpills.InnerHtml.AppendHtml(lastactionlink);
                navpills.InnerHtml.AppendHtml(li.RenderEndTag());


            }



            if (RecordControls?.Controls != null && RecordControls.Controls.Count > 0)
            {
                foreach (var rowctrl in RecordControls.Controls)
                {
                    var li = new TagBuilder("li");
                    var ctrler = !string.IsNullOrEmpty(rowctrl.ActionController) ? rowctrl.ActionController
                        : RecordControls.DefaultController;
                    if (rowctrl.RouteValues == null || rowctrl.RouteValues.Count == 0)
                    {
                        rowctrl.RouteValues = new Dictionary<string, string>();
                        if (RecordControls.RouteValues != null && RecordControls.RouteValues.Count > 0)
                        {
                            rowctrl.RouteValues = RecordControls.RouteValues;
                        }
                    }
                    if (RecordControls.ID > 0)
                    {
                        rowctrl.RouteValues.Add("ID", RecordControls.ID.ToString());
                    }
                    if (!string.IsNullOrEmpty(RecordControls.NameID))
                    {
                        rowctrl.RouteValues.Add("name", RecordControls.NameID);
                    }

                    IHtmlContent actionLink = _htmlHelper.ActionLink(
                        rowctrl.ActionText,
                        rowctrl.ActionName,
                        ctrler,
                        rowctrl.RouteValues
                        );


                    //var link = new TagBuilder("a");

                    //link.InnerHtml.Append(rowctrl.ActionText);
                    //link.Attributes.Add("href", "/Home");

                    //li.InnerHtml.AppendHtml(link.RenderStartTag());

                    //var linkHtml = link.RenderBody();
                    //if (linkHtml != null)
                    //{
                    //    li.InnerHtml.AppendHtml(linkHtml);
                    //}

                    //li.InnerHtml.AppendHtml(link.RenderEndTag());

                    //var liHtml = li.RenderBody();

                    navpills.InnerHtml.AppendHtml(li.RenderStartTag());

                    //if (liHtml != null) { 
                    //    navpills.InnerHtml.AppendHtml(liHtml);
                    //}

                    navpills.InnerHtml.AppendHtml(actionLink);


                    navpills.InnerHtml.AppendHtml(li.RenderEndTag());

                }

            }

            output.Content.AppendHtml(navpills);

        }


    }
}
