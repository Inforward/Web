using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Infrastructure.Caching;
using Bespoke.Infrastructure.Configuration;
using Bespoke.Models.Blog;
using Bespoke.Services.Contracts;
using Bespoke.Services.Messages.Blog.Wordpress;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bespoke.Services
{
    public class BlogService : IBlogService
    {
        #region Private Members

        private readonly string _blogBaseUrl;
        private readonly ICacheStorage _cacheStorage;

        #endregion

        #region Constructor

        public BlogService(ICacheStorage cacheStorage)
        {
            _cacheStorage = cacheStorage;
            _blogBaseUrl = SettingsHelper.Get<string>("Blog.Wordpress.BaseUrl");

            if(string.IsNullOrEmpty(_blogBaseUrl))
                throw new ApplicationException("No value set for AppSetting Blog.Wordpress.BaseUrl.");

            if (!_blogBaseUrl.EndsWith("/"))
                _blogBaseUrl += "/";
        }

        #endregion

        #region Public Methods

        public Post GetPost(string permalink)
        {
            if (permalink.StartsWith("/"))
                permalink = permalink.Substring(1);

            var url = string.Concat(_blogBaseUrl, permalink, "?json=1");

            var response = _cacheStorage.Retrieve("BlogService." + permalink, () => RequestData<GetPostResponse>(url));

            return response.Post;
        }

        public IEnumerable<Post> GetPostsByCategory(string slug)
        {
            var url = string.Concat(_blogBaseUrl, "api/get_category_posts/?slug=", slug);

            var response = RequestData<GetPostsByCategory>(url);

            return response.Posts;
        }

        public IEnumerable<Post> GetPostsByTag(string slug)
        {
            var url = string.Concat(_blogBaseUrl, "api/get_tag_posts/?slug=", slug);

            var response = RequestData<GetPostsByTag>(url);

            return response.Posts;
        }

        public IEnumerable<Post> GetPostsByAuthor(string slug)
        {
            var url = string.Concat(_blogBaseUrl, "api/get_author_posts/?author_slug=", slug);

            var response = RequestData<GetPostsResponse>(url);

            return response.Posts;
        }

        public IEnumerable<Post> GetRecentPosts()
        {
            var url = string.Concat(_blogBaseUrl, "api/get_recent_posts");

            var response = _cacheStorage.Retrieve("BlogService.RecentPosts", () => RequestData<GetPostsResponse>(url));

            return response.Posts;
        }

        public IEnumerable<Archive> GetPostArchiveTree()
        {
            var url = string.Concat(_blogBaseUrl, "api/get_date_index");

            var response = _cacheStorage.Retrieve("BlogService.PostArchiveTree", () => RequestData<GetPostArchiveTreeResponse>(url));

            var list = new List<Archive>();

            foreach (var year in response.Tree)
            {
                list.AddRange(year.Value.Select(month => new Archive()
                    {
                        ArchiveDate = new DateTime(year.Key, month.Key, 1), 
                        PostCount = month.Value
                    }));
            }

            return list.OrderByDescending(a => a.ArchiveDate);
        }

        public IEnumerable<Post> GetPostsByArchive(int year, int month)
        {
            var url = string.Format("{0}{1}/{2}?json=1", _blogBaseUrl, year, month);

            var response = RequestData<GetPostsResponse>(url);

            return response.Posts;
        }

        public IEnumerable<Post> SearchPosts(string searchText)
        {
            var url = string.Concat(_blogBaseUrl, "api/get_search_results/?search=", searchText);

            var response = RequestData<GetPostsResponse>(url);

            return response.Posts;
        }

        public IEnumerable<Category> GetCategories()
        {
            var url = string.Concat(_blogBaseUrl, "api/get_category_index");

            var response = _cacheStorage.Retrieve("BlogService.Categories", () => RequestData<GetCategoriesResponse>(url));

            return response.Categories;
        }

        public IEnumerable<Tag> GetTags()
        {
            var url = string.Concat(_blogBaseUrl, "api/get_tag_index");

            var response = _cacheStorage.Retrieve("BlogService.Tags", () => RequestData<GetTagsResponse>(url));

            return response.Tags;
        }

        #endregion

        #region Private Methods

        private static T RequestData<T>(string url)
        {
            var data = default(T);
            var json = GetJson(url);

            if (!string.IsNullOrEmpty(json))
            {
                data = JsonConvert.DeserializeObject<T>(json);
            }

            return data;
        }

        private static string GetJson(string url)
        {
            var json = null as string;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (var webResponse = request.GetResponse())
            {
                using (var stream = webResponse.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            json = reader.ReadToEnd();
                        }
                    }
                }
            }

            return json;
        }

        #endregion
    }
}
