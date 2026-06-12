# 📋 Task Manager API

REST API для управления личными задачами. Написан на **ASP.NET Core 8** с JWT-аутентификацией и SQLite базой данных.

---

## 🚀 Технологии

| Технология | Назначение |
|---|---|
| ASP.NET Core 8 | Web-фреймворк |
| Entity Framework Core | ORM (работа с БД) |
| SQLite | База данных |
| JWT (JSON Web Tokens) | Аутентификация |
| BCrypt | Хэширование паролей |
| Swagger / OpenAPI | Документация API |

---

## 📦 Установка и запуск

### Предварительные требования
- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Запуск

```bash
# Клонировать репозиторий
git clone https://github.com/YOUR_USERNAME/TaskManagerAPI.git
cd TaskManagerAPI

# Восстановить зависимости и запустить
dotnet run
```

Swagger UI откроется на: **http://localhost:5000**

---

## 📡 Endpoints

### Auth

| Метод | URL | Описание |
|---|---|---|
| `POST` | `/api/auth/register` | Регистрация нового пользователя |
| `POST` | `/api/auth/login` | Вход (получить JWT токен) |

### Tasks *(требуют JWT токен)*

| Метод | URL | Описание |
|---|---|---|
| `GET` | `/api/tasks` | Получить все свои задачи |
| `GET` | `/api/tasks/{id}` | Получить задачу по ID |
| `POST` | `/api/tasks` | Создать задачу |
| `PUT` | `/api/tasks/{id}` | Обновить задачу |
| `DELETE` | `/api/tasks/{id}` | Удалить задачу |
| `GET` | `/api/tasks/categories` | Получить все категории |

### Фильтрация задач

```
GET /api/tasks?status=Pending&priority=High&category=Work
```

---

## 🔐 Аутентификация

1. Зарегистрируйтесь через `POST /api/auth/register`
2. Войдите через `POST /api/auth/login` — получите JWT токен
3. Добавьте токен в заголовок: `Authorization: Bearer <token>`

В Swagger UI нажмите кнопку **Authorize** и вставьте токен.

---

## 📝 Примеры запросов

### Регистрация
```json
POST /api/auth/register
{
  "username": "john",
  "email": "john@example.com",
  "password": "password123"
}
```

### Создание задачи
```json
POST /api/tasks
Authorization: Bearer <your-token>

{
  "title": "Изучить ASP.NET Core",
  "description": "Пройти туториал на Microsoft Docs",
  "priority": "High",
  "category": "Learning",
  "deadline": "2024-12-31T00:00:00"
}
```

### Обновление статуса
```json
PUT /api/tasks/1
Authorization: Bearer <your-token>

{
  "status": "Done"
}
```

---

## ⚙️ Конфигурация

В `appsettings.json` поменяй `SecretKey` перед деплоем:

```json
"JwtSettings": {
  "SecretKey": "your-super-secret-key-change-this-in-production-min32chars!!"
}
```
