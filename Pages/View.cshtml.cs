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


        public void OnGet()
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


        }
    }

}