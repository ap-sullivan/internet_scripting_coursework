using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChinookEntities;
using ChinookContext;

namespace project.Pages
{
    public class ViewAlbums : PageModel
    {
        public string? Heading { get; set; }
        public List<Album>? Albums { get; set; }
        public List<Artist>? Artists { get; set; }
        public List<AlbumArtist>? AlbumArtists { get; set; }

        // Retrieving album and artist data
        public void OnGet(string? tbxArtist, string? search)
        {
            Heading = "Albums";
            ChinookDatabase db = new ChinookDatabase();

            // Only get active albums
            Albums = db.Albums!.Where(a => a.IsActive).ToList();

            AlbumArtists = db.Albums
                .Where(a => a.IsActive) // Filter soft-deleted albums
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

            // Order by ascending
            Artists = db.Artists!
                .OrderBy(a => a.Name)
                .ToList();

            // Filter by artist
            if (!string.IsNullOrEmpty(tbxArtist))
            {
                AlbumArtists = AlbumArtists
                    .Where(a => a.Name.Contains(tbxArtist, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Search function
            if (!string.IsNullOrWhiteSpace(search))
            {
                AlbumArtists = AlbumArtists
                    .Where(a =>
                        a.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        a.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sort artist a-z or album a-z
            string sortAZ = Request.Query["sortAZ"];
            if (sortAZ == "artist")
                AlbumArtists = AlbumArtists.OrderBy(a => a.Name).ToList();
            else if (sortAZ == "album")
                AlbumArtists = AlbumArtists.OrderBy(a => a.Title).ToList();

            // Sort artist z-a or album z-a
            string sortZA = Request.Query["sortZA"];
            if (sortZA == "artist")
                AlbumArtists = AlbumArtists.OrderByDescending(a => a.Name).ToList();
            else if (sortZA == "album")
                AlbumArtists = AlbumArtists.OrderByDescending(a => a.Title).ToList();
        }

        // Adding new albums
        public IActionResult OnPost()
        {
            ChinookDatabase db = new ChinookDatabase();

            string? albumTitle = Request.Form["tbxAlbum"];
            string? artistName = Request.Form["tbxArtist"];

            // Check if artist exists
            var artist = db.Artists.FirstOrDefault(a => a.Name == artistName);
            if (artist == null)
            {
                artist = new Artist { Name = artistName };
                db.Artists.Add(artist);
                db.SaveChanges();
            }

            // Create the album
            Album newAlbum = new Album
            {
                Title = albumTitle,
                ArtistId = artist.ArtistId,
                IsActive = true // New albums are active by default
            };

            db.Albums.Add(newAlbum);
            db.SaveChanges();

            // Add tracks
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
