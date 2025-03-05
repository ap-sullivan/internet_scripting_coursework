using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookContext;
using ChinookEntities;

namespace project.Pages
{
    public class UpdateAlbum : PageModel
    {



        public Album? Album { get; set; }

        public Artist? Artist { get; set; }

        public List<Artist> Artists { get; set; }
        // public List<Tracks>? Tracks { get; set; }

        public IActionResult OnGet(int albumId)
        {
            ChinookDatabase db = new ChinookDatabase();

            Album = db.Albums
           .FirstOrDefault(a => a.AlbumId == albumId);

            Artist = db.Artists.FirstOrDefault(a => a.ArtistId == Album.ArtistId);

            Artists = db.Artists.ToList();

            return Page();
        }



    }
}