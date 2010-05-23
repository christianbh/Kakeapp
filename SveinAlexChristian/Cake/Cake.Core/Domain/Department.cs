using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cake.Core.Domain
{
    [Serializable]
    public class Department
    {
        public Guid Id { get; set; }

        [DisplayName("Kontaktperson")]
        [Required(ErrorMessage = "Her mangler noe")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Her mangler noe")]
        [DisplayName("Epost")]
        public string ContactEmail { get; set; }

        public int SortOrder { get; set; }
    }
}