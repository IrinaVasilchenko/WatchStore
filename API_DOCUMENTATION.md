# WatchesShop REST API — технічний опис

## 1. Призначення

Цей документ описує REST API, доданий поверх існуючого MVC-застосунку WatchesShop.
API дозволяє зовнішнім клієнтам (мобільним застосункам, іншим сервісам, CRM-системам)
працювати з каталогом годинників і замовленнями без використання HTML-інтерфейсу,
а також демонструє інтеграцію з зовнішньою системою через вихідний webhook.

## 2. Загальна інформація

| Параметр | Значення |
|---|---|
| Базовий URL (Docker) | `http://localhost:8080/api` |
| Формат даних | JSON |
| Аутентифікація | Відсутня (демо-версія). Для продакшену рекомендується API key або JWT — див. розділ 6. |
| Swagger UI | `http://localhost:8080/swagger` |
| OpenAPI-схема | `http://localhost:8080/swagger/v1/swagger.json` |

## 3. Ендпоінти — Watches API

Базовий шлях: `/api/watches`

| Метод | Шлях | Опис | Тіло запиту | Успішна відповідь |
|---|---|---|---|---|
| GET | `/api/watches` | Список усіх годинників | — | `200 OK`, масив Watch |
| GET | `/api/watches/{id}` | Годинник за ID | — | `200 OK`, Watch / `404 Not Found` |
| GET | `/api/watches/brands` | Довідник брендів | — | `200 OK`, масив Brand |
| POST | `/api/watches` | Створити годинник | Watch (JSON) | `201 Created` |
| PUT | `/api/watches/{id}` | Оновити годинник | Watch (JSON) | `204 No Content` / `404` |
| DELETE | `/api/watches/{id}` | Видалити годинник | — | `204 No Content` / `404` |

### Приклад: GET /api/watches/1

Запит:
```
GET /api/watches/1
```

Відповідь `200 OK`:
```json
{
  "watchId": 1,
  "model": "GA-24560R",
  "genderId": 2,
  "brandId": 17,
  "styleId": 3,
  "mechanismTypeId": 1,
  "assemblyFactory": "Netherlands",
  "caseId": 13,
  "price": 12340.00,
  "imagePath": "photo_watch/1.jpg"
}
```

### Приклад: POST /api/watches

```bash
curl -X POST http://localhost:8080/api/watches \
  -H "Content-Type: application/json" \
  -d '{
        "model": "TEST-001",
        "genderId": 1,
        "brandId": 1,
        "styleId": 1,
        "mechanismTypeId": 1,
        "assemblyFactory": "Ukraine",
        "caseId": 1,
        "price": 9999.00,
        "imagePath": "photo_watch/1.jpg"
      }'
```

Відповідь: `201 Created`, заголовок `Location` вказує на `GET /api/watches/{id}` нового запису.

## 4. Ендпоінти — Orders API

Базовий шлях: `/api/orders`

| Метод | Шлях | Опис | Тіло запиту | Успішна відповідь |
|---|---|---|---|---|
| GET | `/api/orders` | Список усіх замовлень | — | `200 OK`, масив OrderResponse |
| GET | `/api/orders/{id}` | Замовлення за ID | — | `200 OK` / `404` |
| POST | `/api/orders` | Створити замовлення | CreateOrderRequest | `201 Created` |
| PUT | `/api/orders/{id}/ship` | Позначити відправленим | — | `204 No Content` / `404` |

### Модель CreateOrderRequest

```json
{
  "name": "Іван Петренко",
  "address": "м. Київ, вул. Хрещатик, 1",
  "email": "ivan@example.com",
  "items": [
    { "watchId": 1, "quantity": 1 },
    { "watchId": 3, "quantity": 2 }
  ]
}
```

Валідація:
- `name`, `address`, `email` — обов'язкові; `email` перевіряється на формат.
- `items` — має містити щонайменше один елемент.
- Кожен `watchId` перевіряється на існування; якщо годинника немає — `400 Bad Request`.

### Модель OrderResponse (відповідь)

```json
{
  "orderId": 15,
  "name": "Іван Петренко",
  "address": "м. Київ, вул. Хрещатик, 1",
  "email": "ivan@example.com",
  "orderDate": "2026-09-18T20:15:00Z",
  "isShipped": false,
  "totalPrice": 20019.00,
  "items": [
    { "watchId": 1, "model": "GA-24560R", "quantity": 1, "price": 12340.00 },
    { "watchId": 3, "model": "RH456", "quantity": 2, "price": 7689.00 }
  ]
}
```

## 5. Інтеграція через webhook (order.created)

Коли нове замовлення успішно створюється (через API `POST /api/orders`), система
надсилає HTTP POST-запит на URL, налаштований у `appsettings.json`:

```json
"Webhook": {
  "OrderCreatedUrl": "https://webhook.site/ВАШ-УНІКАЛЬНИЙ-URL"
}
```

Payload, який надсилається зовнішній системі:

```json
{
  "eventType": "order.created",
  "orderId": 15,
  "customerName": "Іван Петренко",
  "email": "ivan@example.com",
  "address": "м. Київ, вул. Хрещатик, 1",
  "orderDate": "2026-09-18T20:15:00Z",
  "totalPrice": 20019.00,
  "items": [
    { "watchId": 1, "quantity": 1, "price": 12340.00 },
    { "watchId": 3, "quantity": 2, "price": 7689.00 }
  ]
}
```

**Обробка помилок:** якщо зовнішній сервіс недоступний або повертає помилку,
запис у нашій БД **не відкатується** — замовлення все одно вважається створеним.
Помилка інтеграції лише логується (`ILogger`). Це свідоме архітектурне рішення:
збій стороннього сервісу не повинен блокувати основний бізнес-процес.

**Як перевірити локально:**
1. Зайти на https://webhook.site — сервіс видасть унікальний тестовий URL.
2. Вставити цей URL у `appsettings.json` → `Webhook:OrderCreatedUrl`.
3. Створити замовлення через `POST /api/orders` (наприклад, через Swagger UI).
4. На сторінці webhook.site з'явиться вхідний запит з повним payload.

## 6. Рекомендації щодо продакшену (поза межами демо)

- Додати автентифікацію (API key в заголовку `X-Api-Key` або JWT Bearer token).
- Додати rate limiting для публічних ендпоінтів.
- Реалізувати чергу повторних спроб (retry) для webhook — наприклад, через
  Polly (`AddPolicyHandler` з `HttpClient`) або чергу повідомлень (RabbitMQ/Azure Service Bus)
  замість прямого синхронного виклику.
- Версіонування API (`/api/v1/...`) для зворотної сумісності при змінах контракту.
- Обмежити CORS конкретними доменами клієнтів замість повного дозволу.

## 7. Як запустити і перевірити

```bash
docker compose up --build
```

Після старту:
- Сайт: http://localhost:8080
- Swagger UI: http://localhost:8080/swagger
- Приклад запиту: `GET http://localhost:8080/api/watches`
