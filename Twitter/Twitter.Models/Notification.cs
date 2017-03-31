namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(128)]
        public string Content { get; set; }
        
        public NotificationTypes Type { get; set; }

        public DateTime Date { get; set; }
        
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
