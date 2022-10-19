using ForumApp.Data;
using ForumApp.Data.Entities;
using ForumApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
    public class PostsController : Controller
    {
        private readonly ForumAppDbContext Data;

        public PostsController(ForumAppDbContext data)
        {
            this.Data = data;
        }

        public IActionResult All()
        {
            var posts =
                Data
                .Posts
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                })
                .ToList();

            return View(posts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostFormModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
            };

            this.Data.Posts.Add(post);
            this.Data.SaveChanges();

            return RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var post = this.Data.Posts.Find(id);

            return View(new PostFormModel
            {
                Title = post.Title,
                Content = post.Content,
            });
        }

        [HttpPost]
        public IActionResult Edit(PostFormModel model, int id)
        {
            var post = this.Data.Posts.Find(id);
            post.Title = model.Title;
            post.Content = model.Content;

            this.Data.SaveChanges();

            return RedirectToAction("All");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = this.Data.Posts.Find(id);

            this.Data.Posts.Remove(post);
            this.Data.SaveChanges();

            return RedirectToAction("All");
        }
    }
}
