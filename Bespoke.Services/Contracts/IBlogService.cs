using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;
using Bespoke.Services.Messages.Blog;

namespace Bespoke.Services.Contracts
{
    public interface IBlogService
    {
        Post GetPost(string permalink);
        GetPostsByCategoryResponse GetPostsByCategory(string slug, int pageNumber = 1);
        GetPostsByTagResponse GetPostsByTag(string slug, int pageNumber = 1);
        GetPostsByAuthor GetPostsByAuthor(string slug, int pageNumber = 1);
        GetPostsResponse GetRecentPosts(int pageNumber = 1);
        IEnumerable<Archive> GetPostArchiveTree();
        GetPostsResponse GetPostsByArchive(int year, int month, int pageNumber = 1);
        GetPostsResponse SearchPosts(string searchText, int pageNumber = 1);
        IEnumerable<Category> GetCategories();
        IEnumerable<Tag> GetTags();
    }
}
