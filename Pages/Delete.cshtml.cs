using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookContext;
using ChinookEntities;

namespace project.Pages
{
    public class Delete : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            using (ChinookDatabase db = new ChinookDatabase())
            {
                // Get the album ID from the form
                int albumId = int.Parse(Request.Form["hdnAlbumID"]!);

                // Get the album
                var album = db.Albums!.Single(a => a.AlbumId == albumId);

                // Soft delete: mark as inactive
                album.IsActive = false;

                // Save changes
                db.SaveChanges();
            }

            return Redirect("/View");
        }
    }
}
