using System.Linq;
using System.Collections.Generic;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Models.Blog;

namespace Bespoke.Web.Models.Blog
{
    public class PostViewModel
    {
        public PostViewModel()
        {
            Tags = new List<TagViewModel>();
        }

        public Post Post { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public string ThumbnailImageUrl
        {
            get
            {
                var imageUrl = default(string);

                if (Post.ThumbnailImages != null && Post.ThumbnailImages.PostThumbnail != null && !string.IsNullOrEmpty(Post.ThumbnailImages.PostThumbnail.Url))
                {
                    imageUrl = Post.ThumbnailImages.PostThumbnail.Url;
                }

                if (string.IsNullOrEmpty(imageUrl) && !Post.Attachments.IsNullOrEmpty())
                {
                    var image = Post.Attachments.FirstOrDefault(a => a.Images != null && a.Images.Thumbnail != null);

                    if (image != null)
                    {
                        imageUrl = image.Url;
                    }
                }

                return imageUrl;
            }
        }
        public string FeaturedImageUrl
        {
            get
            {
                return Post.ThumbnailImageUrl;
            }
        }
        public string Url
        {
            get
            {
                return string.Format("/blog/{0:yyyy}/{0:MM}/{0:dd}/{1}", Post.CreateDate, Post.Slug);
            }
        }
        public string Excerpt
        {
            get
            {
                return Post.Excerpt.Replace(" [&hellip;]", string.Format("&hellip; <a href='{0}' class='read-more'>Read More &gt;</a>", Url));
            }
        }
    }
}