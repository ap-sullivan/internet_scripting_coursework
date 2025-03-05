using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            ChinookDatabase db = new ChinookDatabase();
            Album delAlbum = db.Albums!.Single(a => a.AlbumId == int.Parse(Request.Form["hdnAlbumID"]!));

            List<Tracks> delTrack = db.Tracks!.Where(t => t.AlbumId == delAlbum.AlbumId).ToList();

            db.Tracks!.RemoveRange(delTrack);
            db.Albums!.Remove(delAlbum);

            db.SaveChanges();

            return Redirect("/Delete");


        }

    }
}