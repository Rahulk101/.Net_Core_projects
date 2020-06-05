using Microsoft.AspNetCore.Razor.TagHelpers;
using WEbAppTagHelper.Models;
using Microsoft.AspNetCore.Mvc;

namespace WEbAppTagHelper.ViewComponents
{
    public class SpeakerCardViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(
            SpeakerModel speaker)
        {
            return View(speaker);
        }
    }
}