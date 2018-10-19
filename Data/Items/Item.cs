using OpenGameList.Data.Comments;
using OpenGameList.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OpenGameList.Data.Items
{
    public class Item
    {
        public Item() { }



        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        [Required]
        public int Type { get; set; }
        [Required]
        public int Flags { get; set; }

        [Required]
        public string UserId { get; set; }

       
        public int ViewCount { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]        public DateTime LastModifiedDate { get; set; }

        #region Related Properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser Author { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public string Notes { get; internal set; }
        #endregion Related Properties
    }


}
