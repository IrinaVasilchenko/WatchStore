using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WatchesShop.Models;

namespace WatchesShop.Services
{
    public class OrderNotificationService : IOrderNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderNotificationService> _logger;

        public OrderNotificationService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<OrderNotificationService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task NotifyOrderCreatedAsync(Order order)
        {
            var webhookUrl = _configuration["Webhook:OrderCreatedUrl"];

            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                _logger.LogWarning("Webhook:OrderCreatedUrl не налаштований — сповіщення пропущено.");
                return;
            }

            // Формат payload, який типова CRM/зовнішня система очікує при вебхуку про нове замовлення
            var payload = new
            {
                eventType = "order.created",
                orderId = order.OrderId,
                customerName = order.Name,
                email = order.Email,
                address = order.Address,
                orderDate = order.OrderDate,
                totalPrice = order.OrderItems?.Sum(i => i.Price * i.Quantity) ?? 0,
                items = order.OrderItems?.Select(i => new
                {
                    watchId = i.WatchId,
                    quantity = i.Quantity,
                    price = i.Price
                })
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(webhookUrl, payload);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        "Webhook order.created повернув статус {StatusCode} для замовлення {OrderId}",
                        response.StatusCode, order.OrderId);
                }
                else
                {
                    _logger.LogInformation(
                        "Webhook order.created успішно надіслано для замовлення {OrderId}", order.OrderId);
                }
            }
            catch (Exception ex)
            {
                // Помилка зовнішньої інтеграції НЕ повинна ламати створення замовлення в нашій системі —
                // це ключовий принцип надійного обміну даними між системами.
                _logger.LogError(ex,
                    "Не вдалося надіслати webhook order.created для замовлення {OrderId}", order.OrderId);
            }
        }
    }
}
