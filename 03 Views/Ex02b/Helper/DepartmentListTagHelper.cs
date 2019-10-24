using Ex02b.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ex02b.Helper
{
    public class DepartmentListTagHelper : SelectTagHelper
    {
        public DepartmentListTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public string Selected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";

            this.Items = Dept.Departments.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id,
                Selected = d.Id.Equals(Selected, StringComparison.OrdinalIgnoreCase)
            });

            base.Process(context, output);
        }
    }
}
