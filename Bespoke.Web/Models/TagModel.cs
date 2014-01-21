using System;

namespace Bespoke.Web.Models
{
    public class TagModel
    {
        public TagModel()
        {
            TagType = TagType.MetaName;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public TagType TagType { get; set; }

        public override string ToString()
        {
            string format;

            switch (TagType)
            {
                case TagType.MetaProperty:
                    format = "<meta property=\"{0}\" content=\"{1}\" />";
                    break;
                case TagType.MetaName:
                    format = "<meta name=\"{0}\" content=\"{1}\" />";
                    break;
                case TagType.Link:
                    format = "<link rel=\"{0}\" href=\"{1}\" />";
                    break;
                default:
                    throw new ApplicationException("Unsupported Tag Type: " + TagType);
            }

            return string.Format(format, Key, Value);
        }
    }

    public enum TagType
    {
        MetaName,
        MetaProperty,
        Link
    }
}