using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace DataMigration.Views.Shared.TagHelpers
{
    public class BooleanyesnonullTagHelper : TagHelper
    {
        public bool? CurrentValue { get; set; }

        public string AspFor { get; set; }

        public string NullLabel { get; set; }

        public string InputClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            var inputclass = "";

            if (!string.IsNullOrEmpty(InputClass))
            {
                inputclass = InputClass;
            }

            output.TagName = "div";

            output.Attributes.SetAttribute("class", "hto-radio-group");

            string content = null;
            string checkedattr = "";
            string nulllabel = "";

            checkedattr = (CurrentValue == true) ? " checked" : "";

            nulllabel = (String.IsNullOrEmpty(NullLabel)) ? "Not Specified" : NullLabel;

            content = $@"<input type='radio' name='{AspFor}' id='{AspFor}' value='true' class='hto-form-radio {inputclass}' {checkedattr} /><span class='hto-form-radio'>Yes</span>";

            checkedattr = (CurrentValue == false) ? " checked" : "";

            output.Content.AppendHtml(content);

            content = $@"<input type='radio' name='{AspFor}' id='{AspFor}' value='false' class='hto-form-radio {inputclass}' {checkedattr} /><span class='hto-form-radio'>No</span>";

            output.Content.AppendHtml(content);

            checkedattr = (CurrentValue == null) ? " checked" : "";

            content = $@"<input type='radio' name='{AspFor}' id='{AspFor}' value='' class='hto-form-radio {inputclass}' {checkedattr} /><span class='hto-form-radio'>" + nulllabel + "</span>";

            output.Content.AppendHtml(content);


        }
    }
}
