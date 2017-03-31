namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        [Key]
        public int Id { get; set; }

        [MinLength(8)]
        [MaxLength(256)]
        public string Content { get; set; }
        
        public DateTime PostedOn { get; set; }

        public int SenderId { get; set; }

        public virtual User Sender { get; set; }
        
        public int ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}
