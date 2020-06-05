using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WEbAppTagHelper.Models;

namespace WEbAppTagHelper.TagHelpers
{
    [HtmlTargetElement("speaker-track")]
    public class SpeakerTrackTagHelper : TagHelper
    {
        public string TrackName { get; set; }

        public override void Process(TagHelperContext context, 
            TagHelperOutput output)
        {
            string preContent = $@"
                <h2>{TrackName}</h2>
                <div class='row'>
            ";
            const string postContent = @"
                </div>
            ";
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "track");
            output.PreContent.SetHtmlContent(preContent);
            output.PostContent.SetHtmlContent(postContent);
        }
    }
}