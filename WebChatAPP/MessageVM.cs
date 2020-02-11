using System.ComponentModel.DataAnnotations;

namespace WebChatAPP
{
    public class MessageVM
    {
        [Required(AllowEmptyStrings = false)]
        public string Text { get; set; }
    }
}
