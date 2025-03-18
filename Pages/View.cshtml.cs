using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookEntities;
using ChinookContext;


namespace project.Pages
{

    public class ViewAlbums : PageModel
    {
        public String? Heading { get; set; }
        public List<Album>? Albums { get; set; }
        public List<Artist>? Artists { get; set; }
        public List<AlbumArtist>? AlbumArtists { get; set; }

        // retriving the album and artist data
        public void OnGet(string? tbxArtist)
        {
            Heading = "Albums";
            ChinookDatabase db = new ChinookDatabase();
            Albums = db.Albums.ToList();

            AlbumArtists = db.Albums
        .Join(db.Artists,
            album => album.ArtistId,
            artist => artist.ArtistId,
            (album, artist) => new AlbumArtist
            {
                AlbumId = album.AlbumId,
                Title = album.Title,
                Name = artist.Name
            })
        .ToList();


            Artists = db.Artists
            .OrderBy(a => a.Name)
            .ToList();


            if (!string.IsNullOrEmpty(tbxArtist))
            {
                AlbumArtists = AlbumArtists
                    .Where(a => a.Name == tbxArtist)
                    .ToList();
            }


        }

        // adding new albums
        public IActionResult OnPost()
        {

            ChinookDatabase db = new ChinookDatabase();

            string? albumTitle = Request.Form["tbxAlbum"];
            string? artistName = Request.Form["tbxArtist"];
           

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

            while (Request.Form.ContainsKey($"tbxTrack{trackNumber}"))
            {
                string? trackName = Request.Form[$"tbxTrack{trackNumber}"];



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