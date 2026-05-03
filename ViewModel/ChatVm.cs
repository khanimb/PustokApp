using System.Collections.Generic;

namespace PustokApp.ViewModel
{
    public class ChatVM
    {
        // Bazadan gələn köhnə mesajların siyahısı
        public List<ChatMessageVM> OldMessages { get; set; } = new List<ChatMessageVM>();

        // Yeni mesaj göndərmək üçün (opsional)
        public string CurrentUser { get; set; }
    }

    public class ChatMessageVM
    {
        public string User { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
