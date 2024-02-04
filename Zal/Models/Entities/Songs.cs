using System.ComponentModel.DataAnnotations;

namespace Zal.Models.Entities
{
    public class Song
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Artysta jest wymagany.")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Nazwa utworu jest wymagana.")]
        public string SongName { get; set; }

        [Required(ErrorMessage = "Album jest wymagany.")]
        public string Album { get; set; }

        [Required(ErrorMessage = "Rok jest wymagany.")]
        public string Year { get; set; }

    }

}
