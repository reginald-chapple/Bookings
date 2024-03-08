using Microsoft.EntityFrameworkCore;
using Bookings.Web.Domain;
using Bookings.Web.Models;

namespace Bookings.Web.Data.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ChatMessageResponseModel GetMessageById(long messageId)
        {
            var model = _context.ChatMessages
                .Where(m => m.Id == messageId)
                .Select(m => new ChatMessageResponseModel(m.Content, m.AuthorName, m.Created))
                .FirstOrDefault();
            return model!;
        }

        public async Task CreatePrivateChatAsync(string currentUserId, string otherUserId)
        {
            var privateChat =  await _context.Chats.FirstOrDefaultAsync(c => c.Name == $"{currentUserId}:{otherUserId}");
            var privateChatInverse = await _context.Chats.FirstOrDefaultAsync(c => c.Name == $"{otherUserId}:{currentUserId}");

            if (privateChat == null && privateChatInverse == null)
            {
                var chat = new Chat
                {
                    Name = $"{currentUserId}:{otherUserId}",
                    Type = ChatType.Private,
                };
                chat.Users.Add(new ChatUser { UserId = otherUserId });
                chat.Users.Add(new ChatUser { UserId = currentUserId });
                _context.Chats.Add(chat);
            }
        }

        public  async Task RemovePrivateChatAsync(string currentUserId, string otherUserId)
        {
            var privateChat =  await _context.Chats.FirstOrDefaultAsync(c => c.Name == $"{currentUserId}:{otherUserId}");
            var privateChatInverse = await _context.Chats.FirstOrDefaultAsync(c => c.Name == $"{otherUserId}:{currentUserId}");

            if (privateChat != null)
            {
                _context.Chats.Remove(privateChat);
            }
            else if (privateChatInverse != null)
            {
                 _context.Chats.Remove(privateChatInverse);
            }
        }

        public async Task CreateBusinessChatAsync(string name, string currentUserId, string otherUserId)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Private,
            };
            chat.Users.Add(new ChatUser { UserId = currentUserId });
            chat.Users.Add(new ChatUser { UserId = otherUserId });
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
               
        }

        public async Task<Chat> GetChatByNameAsync(string name)
        {
            var chat = await _context.Chats
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Name == name);

            return chat!;
        }

        public bool ChatExists(string name)
        {
            return _context.Chats.Any(e => e.Name == name);
        }
    }
}