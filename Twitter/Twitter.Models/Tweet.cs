namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        [Key]
        public int Id { get; set; }
        
        [MinLength(8)]
        [MaxLength(256)]
        public string Content { get; set; }
        
        public DateTime PostedOn { get; set; }
        
        public virtual User Author { get; set; }
    }
}
