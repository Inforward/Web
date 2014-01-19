using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Blog;

namespace Bespoke.Services.Contracts
{
    public interface IBlogService
    {
        Post GetPost(string permalink);
        IEnumerable<Post> GetPostsByCategory(string slug);
        IEnumerable<Post> GetPostsByTag(string slug);
        IEnumerable<Post> GetPostsByAuthor(string slug);
        IEnumerable<Post> SearchPosts(string searchText);
        IEnumerable<Post> GetRecentPosts();
        IEnumerable<Post> GetPostsByArchive(int year, int month);
        IEnumerable<Category> GetCategories();
        IEnumerable<Tag> GetTags();
        IEnumerable<Archive> GetPostArchiveTree();
    }
}
