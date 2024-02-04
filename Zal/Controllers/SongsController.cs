using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Zal.Areas.Identity.Data;
using Zal.Models;
using Zal.Models.Entities;
using System.Linq;

namespace Zal.Controllers
{
    public class SongsController : Controller
    {
        private readonly DBContext dBContext;

        public SongsController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSongViewModel viewModel)
        {
            var song = new Song
            {
                Artist = viewModel.Artist,
                SongName = viewModel.SongName,
                Album = viewModel.Album,
                Year = viewModel.Year
            };

            await dBContext.Songs.AddAsync(song);

            await dBContext.SaveChangesAsync();

            return RedirectToAction("List", "Songs");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var song = await dBContext.Songs.FindAsync(id);

            return View(song);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Song songViewModel)
        {
            var song = await dBContext.Songs.FindAsync(songViewModel.Id);

            if (song is not null)
            {
                song.Artist = songViewModel.Artist;
                song.SongName = songViewModel.SongName;
                song.Album = songViewModel.Album;
                song.Year = songViewModel.Year;

                await dBContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Songs");
        }

        public async Task<IActionResult> Delete(Song songViewModel)
        {
            var song = await dBContext.Songs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == songViewModel.Id);

            if (song is not null)
            {
                dBContext.Songs.Remove(songViewModel);
                await dBContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Songs");
        }

        [HttpGet]
        public ActionResult List(string searchBy, string search, string sortBy)
        {
            ViewBag.SortArtistParm = String.IsNullOrEmpty(sortBy) ? "artist_des" : "";
            ViewBag.SortAlbumParm = sortBy == "Album" ? "album_des" : "album";
            ViewBag.SortSongNameParm = sortBy == "SongName" ? "sn_des" : "sn";
            ViewBag.SortYearParm = sortBy == "Year" ? "year_des" : "year";

            var songs = dBContext.Songs.AsQueryable();

            if (searchBy == "Artist")
            {
                songs = songs.Where(x => x.Artist.Contains(search) || search == null);
            }
            else if (searchBy == "SongName")
            {
                songs = songs.Where(x => x.SongName.Contains(search) || search == null);
            }
            else if (searchBy == "Album")
            {
                songs = songs.Where(x => x.Album.Contains(search) || search == null);
            }
            else
                songs = songs.Where(x => x.Year == search || search == null);

            switch (sortBy)
            {
                case "year_des":
                    songs = songs.OrderByDescending(s => s.Year);
                    break;
                case "year":
                    songs = songs.OrderBy(s => s.Year);
                    break;
                case "album_des":
                    songs = songs.OrderByDescending(s => s.Album);
                    break;
                case "album":
                    songs = songs.OrderBy(s => s.Album);
                    break;
                case "sn_des":
                    songs = songs.OrderByDescending(s => s.SongName);
                    break;
                case "sn":
                    songs = songs.OrderBy(s => s.SongName);
                    break;
                case "artist_des":
                    songs = songs.OrderByDescending(s => s.Artist);
                    break;
                default:
                    songs = songs.OrderBy(s => s.Artist);
                    break;
            }

            return View(songs);
        }
    }
}
