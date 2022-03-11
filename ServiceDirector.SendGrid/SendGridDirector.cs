using SendGrid;
using SendGrid.Helpers.Mail;

namespace ServiceDirector.SendGrid
{
    public class SendGridDirector : IServiceDirector<SendGridClient, SendGridMessage, Response>
    {
        private readonly SendGridClient _client;

        public SendGridDirector(SendGridClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Response> ExecuteAsync(
            Func<CancellationToken, Task<SendGridMessage>> configure,
            CancellationToken ct = default)
        {
            var request = await configure(ct);
            var response = await _client.SendEmailAsync(request, ct);
            return response;
        }
    }
}