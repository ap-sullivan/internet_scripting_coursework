using System;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ChinookEntities 
{

public class Album
{
    public int AlbumId { get; set; }
    public string? Title { get; set; }
    public int ArtistId { get; set; }
    public bool IsActive { get; set; } = true; 

}

public class Artist 
{
    public int ArtistId { get; set; }
    public string? Name { get; set; }
}

public class AlbumArtist {
     public int AlbumId { get; set; }
    public string? Title { get; set; }
    public int ArtistId { get; set; }
    public string? Name { get; set; }

}

public class Tracks 
{
public int TrackId { get; set; }
public string? Name { get; set; }
public int AlbumId { get; set; }
public int MediaTypeId { get; set; }
// public int GenreId { get; set; }
public decimal UnitPrice { get; set; }
public int Milliseconds { get; set; }

}

public class Genres {
    public int GenreId { get; set; }
    public string? Name { get; set; }
}

public class MediaTypes {
    public int MediaTypeId { get; set; }
    public string? Name { get; set; }

}

public class Employees {
    public int EmployeeID { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Title { get; set; }
}



}
