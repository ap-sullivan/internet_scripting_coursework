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
    public class InsertAlbum : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            ChinookDatabase db = new ChinookDatabase();

            string? albumTitle = Request.Form["tbxAlbum"];
            string? artistName = Request.Form["tbxArtist"];

            // check if artist exists first

            var artist = db.Artists.FirstOrDefault(a => a.Name == artistName);
            if (artist == null)
            {
                artist = new Artist { Name = artistName };
                db.Artists.Add(artist);
                db.SaveChanges();
            }

            // create the album
            Album newAlbum = new Album
            {
                Title = albumTitle,
                ArtistId = artist.ArtistId
            };
            db.Albums.Add(newAlbum);
            db.SaveChanges();






            return Redirect("~/Index");
        }
    }
}

