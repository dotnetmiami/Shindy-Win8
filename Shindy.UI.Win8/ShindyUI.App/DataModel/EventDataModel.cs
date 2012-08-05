using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShindyUI.App.DataModel
{
    using ShindyUI.App.Data;

    public class BaseDataItem : BaseDataModel
    {
        public BaseDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, BaseDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private BaseDataGroup _group;
        public BaseDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }
}
