using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChinookContext;
using ChinookEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace project.Pages
{
    public class UpdateAlbum : PageModel
    {

        public Album? Album { get; set; }

        public Artist? Artist { get; set; }

        public List<Artist> Artists { get; set; }
        public List<Tracks>? Tracks { get; set; }

        public IActionResult OnGet(int albumId)
        {
            ChinookDatabase db = new ChinookDatabase();

            Album = db.Albums
           .FirstOrDefault(a => a.AlbumId == albumId);

            Artist = db.Artists.FirstOrDefault(a => a.ArtistId == Album.ArtistId);

            Artists = db.Artists.ToList();

            Tracks = db.Tracks
                .Where(t => t.AlbumId == albumId)
                .ToList();


            return Page();
        }

        public IActionResult OnPost()
        {

            int albumId = int.Parse(Request.Form["updtAlbumId"]!);
            int artistId = int.Parse(Request.Form["updtArtistId"]!);

            ChinookDatabase db = new ChinookDatabase();

            Album? updateAlbum = db.Albums?.FirstOrDefault(a => a.AlbumId == albumId);

            if (updateAlbum == null)
            {
               return NotFound();
            }

            // update album title
            updateAlbum.Title = Request.Form["updateTitle"];

            //update artist name

            Artist? updateArtist = db.Artists?.FirstOrDefault(a => a.ArtistId == artistId);

            if (updateArtist != null)
            {
             updateArtist.Name = Request.Form["updateName"];
            }

            // update the tracks

            List<Tracks> tracks = db.Tracks
                .Where(t => t.AlbumId == albumId)
                .ToList();

            foreach (var track in tracks) {

                string key = track.TrackId.ToString();
                if (Request.Form.ContainsKey(key))
                {
                    track.Name = Request.Form[key];
                }
            }
            db.SaveChanges();

      return RedirectToPage("/View");
        }


    }
}