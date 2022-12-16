using API.DTOs;
using Core;

namespace Application.Interfaces;

public interface IBikeShopRepository
{
    public void CreateDB();
    public List<Post> GetAllBikes();

    List<Post> GetAllPostsFromUser(User user);
    List<Post> GetAllPosts();
    void CreatePost(Post post);
    Post GetPost(int id);
    List<Post> getPostByCategory(int[] listId);
    void DeletePost(int id);
    void AddComment(CommentDTO dto);
    List<CommentDTO> GetAllCommentFromPost(int postId);
}