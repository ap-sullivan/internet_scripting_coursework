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
            // string? genre = Request.Form["tbxGenre"];

            // check if artist exists first

            var artist = db.Artists.FirstOrDefault(a => a.Name == artistName);

            // if null is returned thn create new artist
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


            int trackNumber = 1;
            Console.WriteLine("Form Keys Submitted:");
foreach (var key in Request.Form.Keys)
{
    Console.WriteLine(key);
}

            while (Request.Form.ContainsKey($"tbxTrack{trackNumber}"))
            {
                string? trackName = Request.Form[$"tbxTrack{trackNumber}"];

                    Console.WriteLine($"Processing track: {trackName}");  // Debugging output


                if (!string.IsNullOrWhiteSpace(trackName))
                {
                    Tracks newTrack = new Tracks
                    {
                        Name = trackName,
                        AlbumId = newAlbum.AlbumId,
                        MediaTypeId = 1,
                        UnitPrice = 0.99m, 
                        Milliseconds = 999
                    };
                    db.Tracks.Add(newTrack);
                }
                trackNumber++;
            }

            db.SaveChanges();



            return Redirect("~/Index");
        }
    }
}

